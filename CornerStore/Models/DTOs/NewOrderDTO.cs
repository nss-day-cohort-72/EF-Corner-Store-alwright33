namespace CornerStore.Models.DTOs;
public class NewOrderDTO
{
    public int CashierID { get; set; }
    public DateTime? PaidOnDate { get; set; }
    public List<OrderProductDTO> OrderProducts { get; set; }
}
