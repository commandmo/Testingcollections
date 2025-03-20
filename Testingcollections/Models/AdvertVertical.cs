namespace Testingcollections.Models
{
    public class AdvertVertical
    {

        public int VerticalId { get; set; }
        public int AdvertId { get; set; }

        public Vertical Vertical { get; set; }

        public Advert Advert { get; set; }

    }
}
