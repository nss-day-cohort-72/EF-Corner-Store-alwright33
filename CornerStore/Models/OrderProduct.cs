using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models;

public class OrderProduct
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int Quantity { get; set; }

    public Order Order { get; set; }  // Reference to Order
    public Product Product { get; set; }
}