using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class ProductDto
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductQuantity { get; set; }
    public string? ProductPrice { get; set; }
    public int CategoryId { get; set; }
}

public class ProductCategoriesDto
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductQuantity { get; set; }
    public string? ProductPrice { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }

}

