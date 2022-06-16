using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services;

namespace Web.Areas.Admin.Controllers;
[Area("admin")]
[Authorize]
public class CategoryController : Controller
{
    private readonly CategoryServices _services;

    public CategoryController(CategoryServices services)
    {
        _services = services;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var list = await _services.GetCategories();
        return View(list);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(new CategoryDto());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto categoryDto)
    {
        if (ModelState.IsValid)
        {
            await _services.InsertCategory(categoryDto);
            return RedirectToAction("Index");
        }

        return View(categoryDto);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var find = await _services.GetCategoryById(id);
        return View(find);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDto categoryDto)
    {
        if (ModelState.IsValid)
        {
            await _services.UpdateCategory(categoryDto);
            return RedirectToAction("Index");
        }

        return View(categoryDto);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _services.DeleteCategory(id);
        return RedirectToAction("Index");
    }
}