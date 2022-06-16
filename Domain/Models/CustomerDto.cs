using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class CustomerDto
{
    public int CustomerId { get; set; }
    [MaxLength(100)]
    public string? CustomerName { get; set; }
    [MaxLength(100)]
    public string? CustomerAddress { get; set; }
}