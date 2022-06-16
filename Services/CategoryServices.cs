using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services;

public class CategoryServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CategoryServices(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> GetCategories()
    {
        var list = await (
            from p in _context.Categories
            select new CategoryDto()
            {
                CategoryId = p.CategoryId,
                CategoryName = p.CategoryName
            }
        ).ToListAsync();
        return list;
    }
    
    public async Task<int> InsertCategory(CategoryDto categoryDto)
    {
        var map = _mapper.Map<Category>(categoryDto);
        await _context.Categories.AddAsync(map);
        return await _context.SaveChangesAsync();
    }
    
    public async Task<int> UpdateCategory(CategoryDto categoryDto)
    {
        var finded = await _context.Categories.FindAsync(categoryDto.CategoryId);
        finded.CategoryName = categoryDto.CategoryName;
        return await _context.SaveChangesAsync();
    }
    
    public async Task<CategoryDto> GetCategoryById(int id)
    {
        var list = await (
            from p in _context.Categories
            where p.CategoryId == id
            select new  CategoryDto()
            {
                CategoryId = p.CategoryId,
                CategoryName = p.CategoryName
            }
        ).FirstOrDefaultAsync();
        
        return list;
    }
    
    public async Task<int> DeleteCategory(int id)
    {
        var finded = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(finded);
        return await _context.SaveChangesAsync();
    }
}