using Testingcollections.Models;

namespace Testingcollections.Interfaces
{
    public interface ISellerRepository
    {
        ICollection<Seller> GetSellers(); //returns a list

        Seller GetSeller(int sellerid);

        ICollection<Seller> GetSellerOfAAdvert(int advertId);

        ICollection<Seller> GetSellerByState(int stateId);

        ICollection<Advert> GetAdvertBySeller(int sellerId);


        bool SellerExists(int sellerId);

        bool CreateSeller(Seller seller);
        bool UpdateSeller(Seller seller);

        bool DeleteSeller(Seller seller);
        bool Save();

    }
}
