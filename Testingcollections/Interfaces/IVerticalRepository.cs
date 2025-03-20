using Testingcollections.Models;

namespace Testingcollections.Interfaces
{
    public interface IVerticalRepository
    {
        ICollection<Vertical> GetVerticals(); //returns a list

        Vertical GetVertical(int verticalid);

        ICollection<Advert> GetAdvertByVertical(int verticalId);



        bool VerticalExists(int verticalid);

        bool CreateVertical(Vertical vertical);

        bool UpdateVertical(Vertical vertical);

        bool DeleteVertical(Vertical vertical);


        bool Save();

    }

}
