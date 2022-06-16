using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Store
{
    public int StoreId { get; set; }
    [MaxLength(100)]
    public string? StoreDescription { get; set; }
    [MaxLength(100)]
    public string? StoreAddress { get; set; }
    public List<SaleFact>? SalesFact { get; set; }
    
}