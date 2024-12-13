namespace CornerStore.Models.DTOs;
public class NewProductDTO
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public int CategoryId { get; set; }
}
