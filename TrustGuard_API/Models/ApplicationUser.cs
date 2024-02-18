using Microsoft.AspNetCore.Identity;

namespace TrustGuard_API.Models;

public class ApplicationUser : IdentityUser
{
    public string PersonalId { get; set; }
    public string FullName { get; set; }
    public string? Address { get; set; }
    
}