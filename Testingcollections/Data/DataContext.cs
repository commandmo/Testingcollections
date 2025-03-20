using Microsoft.EntityFrameworkCore;
using Testingcollections.Models;

namespace Testingcollections.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Vertical> Verticals { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<SellerAdvert> SellerAdverts { get; set; }

        public DbSet<AdvertVertical> AdvertVerticals { get; set; }


        //below code customises the data table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SellerAdvert>()
                .HasKey(sa => new { sa.SellerId, sa.AdvertId });
            modelBuilder.Entity<SellerAdvert>()
                .HasOne(s => s.Seller)
                .WithMany(sa => sa.SellerAdverts)
                .HasForeignKey(s => s.SellerId);
            modelBuilder.Entity<SellerAdvert>()
                .HasOne(a => a.Advert)
                .WithMany(sa => sa.SellerAdverts)
                .HasForeignKey(a => a.AdvertId);


            modelBuilder.Entity<AdvertVertical>()
                .HasKey(av => new { av.VerticalId, av.AdvertId });
            modelBuilder.Entity<AdvertVertical>()
                .HasOne(v => v.Vertical)
                .WithMany(av => av.AdvertVerticals)
                .HasForeignKey(v => v.VerticalId);
            modelBuilder.Entity<AdvertVertical>()
                .HasOne(a => a.Advert)
                .WithMany(av => av.AdvertVerticals)
                .HasForeignKey(a => a.AdvertId);

        }
    }
}





