using Testingcollections.Data;
using Testingcollections.Models;


namespace Testingcollections
{
    public class Seed
    {

        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.SellerAdverts.Any())
            {
                var sellerAdverts = new List<SellerAdvert>()
                {
                    new SellerAdvert()
                    {

                        Advert = new Advert()
                        {
                            Make = "Holden",
                            Model = "Commodore",
                            Year = 1997,
                            Colour = "Black",
                            AdvertVerticals = new List<AdvertVertical>()
                            {
                                new AdvertVertical { Vertical = new Vertical() { Name = "carsales.com.au"}}
                            },

                        },


                        Seller = new Seller()
                        {
                            FirstName= "Muhammad",
                            LastName = "Masri",
                            EmailAddress = "elmazri@live.com",
                            PhoneNumber = 0492865599,

                            State = new State ()
                            {
                                Name = "Victoria"
                            }

                        }

                    },




                    new SellerAdvert()
                    {

                        Advert = new Advert()
                        {
                            Make = "Seadoo",
                            Model = "Jetski",
                            Year = 2008,
                            Colour = "Red",
                            AdvertVerticals = new List<AdvertVertical>()
                            {
                                new AdvertVertical { Vertical = new Vertical() { Name = "boatsales.com.au"}}
                            },

                        },

                        Seller = new Seller()
                        {
                            FirstName= "Jonathon",
                            LastName = "Right",
                            EmailAddress = "jon.r@gmail.com",
                            PhoneNumber = 0430586585,

                            State = new State ()
                            {
                                Name = "New South Wales"
                            }



                        }

                    },

                    new SellerAdvert()
                    {

                        Advert = new Advert()
                        {
                            Make = "Toyota",
                            Model = "Camry",
                            Year = 2003,
                            Colour = "Gold",
                            AdvertVerticals = new List<AdvertVertical>()
                            {
                                new AdvertVertical { Vertical = new Vertical() { Name = "carsales.com.au"}}
                            },

                        },


                        Seller = new Seller()
                        {
                            FirstName= "Tony",
                            LastName = "Seth",
                            EmailAddress = "seth@live.com",
                            PhoneNumber = 0358458658,

                            State = new State ()
                            {
                                Name = "Queensland"
                            }

                        }

                    },

                };

                dataContext.SellerAdverts.AddRange(sellerAdverts);
                dataContext.SaveChanges();

            }
        }
    }
}
