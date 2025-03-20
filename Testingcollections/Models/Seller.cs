namespace Testingcollections.Models
{
    public class Seller
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public int PhoneNumber { get; set; }

        public State State { get; set; }

        public ICollection<SellerAdvert> SellerAdverts { get; set; }


    }
}
