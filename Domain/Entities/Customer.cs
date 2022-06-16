using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Customer
{
    public int CustomerId { get; set; }
    [MaxLength(100)]
    public string? CustomerName { get; set; }
    [MaxLength(100)]
    public string? CustomerAddress { get; set; }
    public List<SaleFact>? SalesFact { get; set; }
}