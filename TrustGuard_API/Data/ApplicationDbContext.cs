using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrustGuard_API.Models;

namespace TrustGuard_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<InsuranceType> InsuranceTypes { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<InsuranceType>().HasData(new InsuranceType
        //    {
        //        Id = 1,
        //        Name = "Casco",
        //        Price = 8.99,
        //        Category = "Car",
        //        SpecialTag = "Best Seller",
        //        Description = "With TrustGuard's CASCO insurance, you can safeguard your vehicle from potential financial losses due to damage, regardless of who is at fault. Protect yourself anywhere/anytime.",
        //        Image = GetBase64Image("Images/tpl.png")
        //    },
        //            new InsuranceType
        //            {
        //                Id = 2,
        //                Name = "House",
        //                Price = 11.99,
        //                Category = "Property",
        //                Description = "With property insurance at TrustGuard you can protect your business space, as well as its contents, restoring the business to its proper condition without the need for investment.",
        //                Image = GetBase64Image("Images/house.png")
        //            },
        //            new InsuranceType
        //            {
        //                Id = 3,
        //                Name = "TPL",
        //                Price = 14.99,
        //                Category = "Car",
        //                SpecialTag = "Special Price",
        //                Description = "Compulsory auto liability insurance is a legal requirement for vehicle owners, shielding you from financial responsibility for any damages you cause to a third party with your vehicle.",
        //                Image = GetBase64Image("Images/tpl2.png")
        //            },
        //            new InsuranceType
        //            {
        //                Id = 4,
        //                Name = "Apartment",
        //                Price = 11.99,
        //                Category = "Property",
        //                Description = "TrustGuard's personal property insurance ensures that your personal space remains protected and comfortable by covering the cost of replacing any lost or damaged items.",
        //                Image = GetBase64Image("Images/apartment.png")
        //            },
        //            new InsuranceType
        //            {
        //                Id = 5,
        //                Name = "Health",
        //                Price = 8.99,
        //                Category = "Car",
        //                Description = "TrustGuard's health insurance offers comprehensive coverage for unexpected medical expenses, providing financial support for your health needs. Protect yourself anywhere/anytime.",
        //                Image = GetBase64Image("Images/health.png")
        //            },
        //            new InsuranceType
        //            {
        //                Id = 6,
        //                Name = "Accident",
        //                Price = 11.99,
        //                Category = "Individual",
        //                Description = "Prepare for unexpected accidents by purchasing personal accident insurance, which ensures direct compensation in the event of injury resulting from an accident.",
        //                Image = GetBase64Image("Images/accident2.png")
        //            });

        //}
        //    private string GetBase64Image(string imagePath)
        //    {
        //        // Read the image file and convert it to a base64 string
        //        byte[] imageBytes = File.ReadAllBytes(imagePath);
        //        return Convert.ToBase64String(imageBytes);
        //    }
    }
}
