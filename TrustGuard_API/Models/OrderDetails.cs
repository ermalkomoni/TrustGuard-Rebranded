using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrustGuard_API.Models;

public class OrderDetails
{
    [Key]
    public int OrderDetailId { get; set; }
    [Required]
    public int OrderHeaderId { get; set; }
    [Required]
    public int InsuranceTypeId { get; set; }
    [ForeignKey("InsuranceTypeId")]
    public InsuranceType InsuranceType { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public string ItemName { get; set; }
    [Required]
    public double Price { get; set; }
}