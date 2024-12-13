namespace CornerStore.Models.DTOs;

public class CashiersOrdersDTO
{
    public int Id { get; set; }
    public decimal Total { get; set; }
    public DateTime? PaidOnDate { get; set; }
    public List<OrderProductDTO> OrderProducts { get; set; }
}