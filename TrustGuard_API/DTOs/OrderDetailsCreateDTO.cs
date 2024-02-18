using System.ComponentModel.DataAnnotations;

namespace TrustGuard_API.DTOs
{
    public class OrderDetailsCreateDTO
    {
        [Required]
        public int InsuranceTypeId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
