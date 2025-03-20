using AutoMapper;
using System.Diagnostics.Metrics;
using Testingcollections.Data;
using Testingcollections.Interfaces;
using Testingcollections.Models;

namespace Testingcollections.Repository
{
    public class StateRepository : IStateRepository

    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StateRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateState(State state)
        {
            _context.Add(state);
            return Save();
        }

        public ICollection<Seller> GetAdvertByState(int stateId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Seller> GetSellerOfAState(int stateId)
        {
            return _context.Sellers.Where(s => s.State.Id == stateId).ToList();
        }

        

        public State GetState(int id)
        {
            return _context.States.Where(s => s.Id == id).FirstOrDefault();
        }

        public State GetStateBySeller(int sellerid)
        {
            return _context.Sellers.Where(s => s.Id == sellerid).Select(s => s.State).FirstOrDefault();
        }

        public ICollection<State> GetStates()
        {
            return _context.States.ToList();
        }



        public bool StateExists(int id)
        {
            return _context.States.Any(s => s.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateState(State state)
        {
            _context.Update(state);
            return Save();
        }

        public bool DeleteState(State state)
        {
            _context.Remove(state);
            return Save();
        }
    }
}
