using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services;

public class CustomerServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CustomerServices(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CustomerDto>> GetCustomers()
    {
        var list = await (
            from p in _context.Customers
            select new CustomerDto()
            {
                CustomerId = p.CustomerId,
                CustomerName = p.CustomerName,
                CustomerAddress = p.CustomerAddress
            }
        ).ToListAsync();
        return list;
    }
    
    public async Task<int> InsertCustomer(CustomerDto customerDto)
    {
        var map = _mapper.Map<Customer>(customerDto);
        await _context.Customers.AddAsync(map);
        return await _context.SaveChangesAsync();
    }
    
    public async Task<int> UpdateCustomer(CustomerDto customerDto)
    {
        var finded = await _context.Customers.FindAsync(customerDto.CustomerId);
        finded.CustomerName = customerDto.CustomerName;
        finded.CustomerAddress = customerDto.CustomerAddress;
        return await _context.SaveChangesAsync();
    }
    
    public async Task<CustomerDto> GetCustomerById(int id)
    {
        var list = await (
            from p in _context.Customers
            where p.CustomerId == id
            select new  CustomerDto()
            {
                CustomerId = p.CustomerId,
                CustomerName = p.CustomerName,
                CustomerAddress = p.CustomerAddress
            }
        ).FirstOrDefaultAsync();
        
        return list;
    }
    
    public async Task<int> DeleteCustomer(int id)
    {
        var finded = await _context.Customers.FindAsync(id);
        _context.Customers.Remove(finded);
        return await _context.SaveChangesAsync();
    }
}