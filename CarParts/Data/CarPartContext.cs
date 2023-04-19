
using CarParts.Models.Main;
using CarParts.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CarParts.Data
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

            //Seeding a  'Administrator' role to AspNetRoles table
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> {Id = 1, Name = "Admin", NormalizedName = "ADMIN".ToUpper() },
                new IdentityRole<int> { Id = 2, Name = "Accountant", NormalizedName = "ACCOUNTANT".ToUpper() },
                new IdentityRole<int> { Id = 3, Name = "DataEntry", NormalizedName = "DATAENTRY".ToUpper() }
                );


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser<int>>();
            var user = new User
            {
                Id = 1, // primary key
                UserName = "Admin",
                NormalizedUserName = "ADMIN".ToUpper()
            };
            user.PasswordHash = hasher.HashPassword(user, "Admin");
            //Seeding the User to AspNetUsers table
            builder.Entity<User>().HasData(user);


            //Seeding the relation between our user and role to AspNetUserRoles table
            builder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int>
            {
                RoleId = 1, 
                UserId = 1
            });
            var client = new Client
            {
                Id = 1,
                Name = "زبون مفرق"
            };
            builder.Entity<Client>().HasData(client);
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
