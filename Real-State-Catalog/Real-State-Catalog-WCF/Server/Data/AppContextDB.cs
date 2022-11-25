using Microsoft.EntityFrameworkCore;
using Real_State_Catalog_WCF.Models;
using System.Data.Common;

namespace Real_State_Catalog_WCF.Data
{
    public class AppContextDb : DbContext
    {
        public AppContextDb(DbContextOptions<AppContextDb> options)
            : base(options)
        {

        }

        public DbSet<Accommodation> Accommodations { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<Booking> Booking { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Amenity> Amenity { get; set; }

        public DbSet<Bookmark> Bookmark { get; set; }


    }
    }
