using Testingcollections.Data;
using Testingcollections.Interfaces;
using Testingcollections.Models;

namespace Testingcollections.Repository
{
    public class SellerRepository : ISellerRepository
    {

        private readonly DataContext _context;


        public SellerRepository(DataContext context)

        {
            _context = context;
        }

        public bool CreateSeller(Seller seller)
        {
            _context.Add(seller);

            return Save();
        }

        public bool DeleteSeller(Seller seller)
        {
            _context.Remove(seller);

            return Save();
        }

        public ICollection<Advert> GetAdvertBySeller(int sellerId)
        {
            return _context.SellerAdverts.Where(s => s.Seller.Id == sellerId).Select(a => a.Advert).ToList();
        }

        public Seller GetSeller(int sellerId)
        {
            return _context.Sellers.Where(s => s.Id == sellerId).FirstOrDefault();
        }

        public ICollection<Seller> GetSellerByState(int stateId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Seller> GetSellerOfAAdvert(int advertId)
        {
            return _context.SellerAdverts.Where(a => a.Advert.Id == advertId).Select(s => s.Seller).ToList();
        }

        public ICollection<Seller> GetSellers()
        {
            return _context.Sellers.OrderBy(s => s.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SellerExists(int sellerId)
        {
            return _context.Sellers.Any(s => s.Id == sellerId);
        }

        public bool UpdateSeller(Seller seller)
        {
            _context.Update(seller);

            return Save();
        }
    }
}
