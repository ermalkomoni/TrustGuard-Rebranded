using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TrustGuard_API.Data;
using TrustGuard_API.Models;
using TrustGuard_API.Utility;

namespace TrustGuard_API.DbInitialiser
{
    public class DbInitialiser : IDbInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DbInitialiser(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public void Initialise()
        {
            // migrations if they are not applied
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {

                ApplicationUser user = new ApplicationUser
                {
                    UserName = "ermaladmin@trustguard.com",
                    Email = "ermaladmin@trustguard.com",
                    NormalizedEmail = "ermaladmin@trustguard.com".ToUpper(),
                    FullName = "Ermal Komoni",
                    PersonalId = "Ermal@123"
                };


                var result = _userManager.CreateAsync(user, "Ermal@123").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();

                    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                }
            }

            if (_context.InsuranceTypes.Count() <= 0)
            {
                var insuranceTypes = new List<InsuranceType>
                {
                    new InsuranceType
                    {
                        Name = "Casco",
                        Price = 8.99,
                        Category = "Car",
                        SpecialTag = "Best Seller",
                        Description = "With TrustGuard's CASCO insurance, you can safeguard your vehicle from potential financial losses due to damage, regardless of who is at fault. Protect yourself anywhere/anytime.",
                        Image = GetBase64Image("/Images/tpl.png")
                    },
                    new InsuranceType
                    {
                        Name = "House",
                        Price = 11.99,
                        Category = "Property",
                        Description = "With property insurance at TrustGuard you can protect your business space, as well as its contents, restoring the business to its proper condition without the need for investment.",
                        Image = GetBase64Image("/Images/house.png")
                    },
                    new InsuranceType
                    {
                        Name = "TPL",
                        Price = 14.99,
                        Category = "Car",
                        SpecialTag = "Special Price",
                        Description = "Compulsory auto liability insurance is a legal requirement for vehicle owners, shielding you from financial responsibility for any damages you cause to a third party with your vehicle.",
                        Image = GetBase64Image("/Images/tpl2.png")
                    },
                    new InsuranceType
                    {
                        Name = "Apartment",
                        Price = 11.99,
                        Category = "Property",
                        Description = "TrustGuard's personal property insurance ensures that your personal space remains protected and comfortable by covering the cost of replacing any lost or damaged items.",
                        Image = GetBase64Image("/Images/apartment.png")
                    },
                    new InsuranceType
                    {
                        Name = "Health",
                        Price = 8.99,
                        Category = "Car",
                        Description = "TrustGuard's health insurance offers comprehensive coverage for unexpected medical expenses, providing financial support for your health needs. Protect yourself anywhere/anytime.",
                        Image = GetBase64Image("/Images/health.png")
                    },
                    new InsuranceType
                    {
                        Name = "Accident",
                        Price = 11.99,
                        Category = "Individual",
                        Description = "Prepare for unexpected accidents by purchasing personal accident insurance, which ensures direct compensation in the event of injury resulting from an accident.",
                        Image = GetBase64Image("/Images/accident2.png")
                    }

                };
                    _context.InsuranceTypes.AddRange(insuranceTypes);
                    _context.SaveChanges();
                }
            }

            private string GetBase64Image(string imagePath)
            {
                // Get the path to the wwwroot folder
                string webRootPath = _webHostEnvironment.WebRootPath;

                // Combine the wwwroot path with the image path
                string absoluteImagePath = Path.Combine(webRootPath, imagePath.TrimStart('/'));

                // Convert the combined path to a URI
                Uri uri = new Uri(absoluteImagePath);

                // Get the absolute URI path
                string absoluteUriPath = uri.AbsoluteUri;

                // Read the image file and convert it to a base64 string
                byte[] imageBytes = System.IO.File.ReadAllBytes(absoluteUriPath);
                return Convert.ToBase64String(imageBytes);
        } 
    }
}
