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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<InsuranceType>().HasData(new InsuranceType
            {
                Id = 1,
                Name = "Casco Insurance",
                Description = "Accidental damage to your car from an accident, in the parking lot, theft or collision, accident with your fault is not protected by compulsory motor insurance (TPL). By purchasing CASCO insurance, you protect yourself from the financial system of damage to your vehicle, who is the defender/culpable for the damage.",
                Image = ("Images/tpl.png"),
                Price = 7.99,
                Category = "Car",
                SpecialTag = "Best Seller"
            }, new InsuranceType
            {
                Id = 2,
                Name = "House",
                Description = "With property insurance at TrustGuard you can protect your business space, as well as its contents, restoring the business to its proper condition without the need for investment.",
                Image = ("Images/house.svg"),
                Price = 8.99,
                Category = "Property",
                SpecialTag = ""
            }, new InsuranceType
            {
                Id = 3,
                Name = "TPL",
                Description = "Compulsory auto liability insurance is a legal obligation for all vehicle owners and protects you from liability for damages you may cause to a third party with your vehicle.",
                Image = ("Images/tpl2.png"),
                Price = 8.99,
                Category = "Car",
                SpecialTag = ""
            }, new InsuranceType
            {
                Id = 4,
                Name = "Apartment",
                Description = "Personal property insurance is the type of insurance that helps protect the space where you feel comfortable. TrustGuard personal property insurance helps cover the cost of replacing them.",
                Image = ("Images/apartment.png"),
                Price = 10.99,
                Category = "Property",
                SpecialTag = ""
            }, new InsuranceType
            {
                Id = 5,
                Name = "Health",
                Description = "With health insurance, PRISIG provides coverage for all or part of the medical expenses that may arise from the occurrence of unexpected health cases.",
                Image = ("Images/health.png"),
                Price = 12.99,
                Category = "Individual",
                SpecialTag = "Best Seller"
            }, new InsuranceType
            {
                Id = 6,
                Name = "Accident",
                Description = "Accidents happen when you least expect them! However, you can prepare for such events by purchasing personal accident insurance, through which you will be paid directly in the event of an accident that results in your injury.",
                Image = ("Images/accident2.png"),
                Price = 11.99,
                Category = "Individual",
                SpecialTag = "Sale"
            });
        }
    }
}
