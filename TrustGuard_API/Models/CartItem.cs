using System.ComponentModel.DataAnnotations.Schema;

namespace TrustGuard_API.Models;

public class CartItem
{
    public int Id { get; set; }
    public int InsuranceTypeId { get; set; }
    [ForeignKey("InsuranceTypeId")]
    public InsuranceType InsuranceType { get; set; } = new();
    public int Quantity { get; set; }
    public int ShoppingCartId { get; set; }
}