namespace Testingcollections.Models
{
    public class SellerAdvert
    {

        public int SellerId { get; set; }
        public int AdvertId { get; set; }

        public Seller Seller { get; set; }

        public Advert Advert { get; set; }

    }
}
