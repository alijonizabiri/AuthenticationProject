using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services;

public class ProductServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductServices(DataContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
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
                CategoryName = c.CategoryName,
                ProductImage = p.ProductImage
            }
        ).ToListAsync();
        return list;
    }

    public async Task<int> InsertProduct(ProductDto product)
    {
        var fileName = Guid.NewGuid() + "_" + Path.GetFileName(product.ProductImage.FileName);
        var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await product.ProductImage.CopyToAsync(stream);
        }
        
        var map = _mapper.Map<Product>(product);
        map.ProductImage = fileName;
        await _context.Products.AddAsync(map);
        return await _context.SaveChangesAsync();
    }
    
    public async Task<int> UpdateProduct(ProductDto product)
    {
        var fileName = Guid.NewGuid() + "_" + Path.GetFileName(product.ProductImage.FileName);
        var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await product.ProductImage.CopyToAsync(stream);
        }
        
        var finded = await _context.Products.FindAsync(product.ProductId);
        finded.ProductId = product.ProductId;
        finded.ProductName = product.ProductName;
        finded.ProductQuantity = product.ProductQuantity;
        finded.ProductPrice = product.ProductPrice;
        finded.CategoryId = product.CategoryId;
        finded.ProductImage = fileName;
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