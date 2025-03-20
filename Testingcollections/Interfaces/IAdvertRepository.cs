using Microsoft.Extensions.Caching.Memory;
using Testingcollections.Models;

namespace Testingcollections.Interfaces
{
    public interface IAdvertRepository

    {
  


        Advert GetAdvert(int advertid);
        
        ICollection<Seller> GetSellerByAdvert(int advertid);

        //ICollection<Advert> GetAdverts(); //returns a list (before caching implemented)
        Task<ICollection<Advert>> GetAdvertsAsync(); //returns a list with caching

        bool AdvertExists(int advertid);

        bool CreateAdvert(Advert advert);

        bool UpdateAdvert(Advert advert);

        bool DeleteAdvert(Advert advert);


        bool Save();

    }

}