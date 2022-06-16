using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services;

public class ProductServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ProductServices(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductCategoriesDto>> GetProducts()
    {
        var list = await (
            from p in _context.Products
            orderby p descending 
            join c in _context.Categories on p.CategoryId equals c.CategoryId 
            select new ProductCategoriesDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductQuantity = p.ProductQuantity,
                ProductPrice = p.ProductPrice,
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }
        ).ToListAsync();
        return list;
    }

    public async Task<int> InsertProduct(ProductDto productDto)
    {
        var map = _mapper.Map<Product>(productDto);
        await _context.Products.AddAsync(map);
        return await _context.SaveChangesAsync();
    }
    
    public async Task<int> UpdateProduct(ProductDto productDto)
    {
        var finded = await _context.Products.FindAsync(productDto.ProductId);
        finded.ProductId = productDto.ProductId;
        finded.ProductName = productDto.ProductName;
        finded.ProductQuantity = productDto.ProductQuantity;
        finded.ProductPrice = productDto.ProductPrice;
        finded.CategoryId = productDto.CategoryId;
        return await _context.SaveChangesAsync();
    }

    public async Task<ProductDto> GetProductById(int id)
    {
        var list = await (
            from p in _context.Products
            where p.ProductId == id
            select new  ProductDto()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductQuantity = p.ProductQuantity,
                ProductPrice = p.ProductPrice,
                CategoryId = p.CategoryId
            }
        ).FirstOrDefaultAsync();
        
        return list;
    }

    public async Task<int> DeleteProduct(int id)
    {
        var finded = await _context.Products.FindAsync(id);
        _context.Products.Remove(finded);
        return await _context.SaveChangesAsync();
    }
}