namespace CornerStore.Models.DTOs;
public class OrderProductWithCategoryDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public ProductCategoryDTO Category { get; set; }
}
