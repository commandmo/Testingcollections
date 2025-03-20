namespace Testingcollections.Models
{
    public class Advert
    {

        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Colour { get; set; }


        public ICollection<SellerAdvert> SellerAdverts { get; set; }
        public ICollection<AdvertVertical> AdvertVerticals { get; set; }




    }
}

