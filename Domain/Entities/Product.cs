using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace Domain;

public class Product
{
    public int ProductId { get; set; }
    [MaxLength(100)]
    public string? ProductName { get; set; }
    [MaxLength(100)]
    public string? ProductQuantity { get; set; }
    public string? ProductPrice { get; set; }
    public string? ProductImage { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<SaleFact>? SalesFact { get; set; }
}