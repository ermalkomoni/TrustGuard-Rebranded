namespace TrustGuard_API.DTOs
{
    public class RegisterRequestDTO
    {
        public string PersonalId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
