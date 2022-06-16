namespace Domain;

public class SaleFact
{
    public int Id { get; set; }
    public Product? Product { get; set; }
    public int ProductId { get; set; }
    public Date? Date { get; set; }
    public int DateId { get; set; }
    public Store? Store { get; set; }
    public int StoreId { get; set; }
    public Customer? Customer { get; set; }
    public int CutomerId { get; set; }
    public decimal Amount { get; set; }
    public decimal Quantity { get; set; }
    public decimal Cost { get; set; }
}