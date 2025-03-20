namespace Testingcollections.Models
{
    public class State
    {

        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<Seller> Sellers { get; set; }



    }
}
