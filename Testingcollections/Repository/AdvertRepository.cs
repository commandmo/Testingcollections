using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using Testingcollections.Data;
using Testingcollections.Interfaces;
using Testingcollections.Models;

namespace Testingcollections.Repository
{
    public class AdvertRepository : IAdvertRepository
    {

        private readonly DataContext _context;
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;
  

        public AdvertRepository(DataContext context, IMemoryCache cache, ILogger<AdvertRepository> logger)

        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public bool AdvertExists(int id)
        {
            return _context.Adverts.Any(a => a.Id == id);
        }

        public bool CreateAdvert(Advert advert)
        {
            _context.Add(advert);
            return Save();
        }

        public bool DeleteAdvert(Advert advert)
        {
            _context.Remove(advert);
            return Save();
        }

        public Advert GetAdvert(int advertid)
        {
            return _context.Adverts.Where(c => c.Id == advertid).FirstOrDefault();
        }

        public async Task<ICollection<Advert>> GetAdvertsAsync() //get list of adverts using in-memory cache
        {
            string cacheKey = "AllAdverts";
            if (_cache.TryGetValue(cacheKey, out ICollection<Advert>?cachedAdverts))
            {
                _logger.LogInformation("Cache hit for key: {CacheKey}", cacheKey);
                return cachedAdverts!;
            }

            _logger.LogInformation("Cache miss for key: {CacheKey}", cacheKey);

            var adverts = await _context.Adverts.ToListAsync();

            if (adverts.Any())
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                _cache.Set(cacheKey, adverts, cacheOptions);
                _logger.LogInformation("Data cached for key: {CacheKey}", cacheKey);
            }    
            return adverts;
        }

        ICollection<Seller> IAdvertRepository.GetSellerByAdvert(int advertId)
        {
            return _context.SellerAdverts.Where(a => a.Advert.Id == advertId).Select(s => s.Seller).ToList(); //SellerAdverts from datacontext
        }

        public bool Save()
        {
            var saved  = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAdvert(Advert advert)
        {
            _context.Update(advert);
            return Save();
        }


    }
}
