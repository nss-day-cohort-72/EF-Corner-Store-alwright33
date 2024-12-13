namespace CornerStore.Models.DTOs;

public class OrderByIdDTO
{
    public int Id { get; set; }
    public string Cashier { get; set; }
    public decimal Total { get; set; }
    public DateTime? PaidOnDate { get; set; }
    public List<OrderProductWithCategoryDTO> OrderProducts { get; set; } // Change this to match the type
}
