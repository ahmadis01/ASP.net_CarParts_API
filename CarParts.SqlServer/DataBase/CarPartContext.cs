using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CarParts.Models.Security;
using CarParts.Models.Main;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarParts.SqlServer.DataBase
{
    public class CarPartContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        #region - Constructor -
        public CarPartContext(DbContextOptions options) : base(options)
        {

        }
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Car>()
                .HasOne(x => x.Brand)
                .WithMany(x => x.Car)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Restrict);


        }
        #region - properties -
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarPart> CarParts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StorePart> StoreParts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        #endregion
    }


}
