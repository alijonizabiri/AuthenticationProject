namespace Domain;

public class Date
{
    public int DateId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public DateTime FullDate { get; set; }
    public int Quarter { get; set; }
    public List<SaleFact>? SalesFact { get; set; }
}