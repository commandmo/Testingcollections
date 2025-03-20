namespace Testingcollections.Models
{
    public class Vertical
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AdvertVertical> AdvertVerticals { get; set; }

    }
}
