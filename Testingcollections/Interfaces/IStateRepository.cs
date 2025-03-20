using System.Diagnostics.Metrics;
using Testingcollections.Models;

namespace Testingcollections.Interfaces
{
    public interface IStateRepository
    {
        ICollection<State> GetStates(); //returns a list

        State GetState(int stateid);

        State GetStateBySeller(int sellerId);

        ICollection<Seller> GetSellerOfAState(int stateId);



        bool StateExists(int sellerId);

        bool CreateState(State state);

        bool UpdateState(State state);

        bool DeleteState(State state);


        bool Save();

    }

}