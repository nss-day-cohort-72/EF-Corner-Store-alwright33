namespace CornerStore.Models.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public ProductCategoryDTO Category { get; set; }
}