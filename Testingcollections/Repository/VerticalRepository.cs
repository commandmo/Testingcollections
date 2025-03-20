using Testingcollections.Data;
using Testingcollections.Interfaces;
using Testingcollections.Models;

namespace Testingcollections.Repository
{
    public class VerticalRepository : IVerticalRepository
    {

        private readonly DataContext _context;

        public VerticalRepository(DataContext context)
        {
            _context = context; 
        }
        public bool CreateVertical(Vertical vertical)
        {
            _context.Add(vertical);
            return Save();
        }

        public bool DeleteVertical(Vertical vertical)
        {
            _context.Remove(vertical);
            return Save();
        }

        public ICollection<Advert> GetAdvertByVertical(int verticalId)
        {
            return _context.AdvertVerticals.Where(e => e.VerticalId == verticalId).Select(v => v.Advert).ToList();
        }

        public Vertical GetVertical(int id)
        {
            return _context.Verticals.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Vertical> GetVerticals()
        {
            return _context.Verticals.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateVertical(Vertical vertical)
        {
            _context.Update(vertical);
            return Save();
        }

        public bool VerticalExists(int id)
        {
            return _context.Verticals.Any(v => v.Id == id);
        }
    }
}
