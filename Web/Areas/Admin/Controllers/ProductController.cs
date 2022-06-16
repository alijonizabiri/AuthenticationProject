using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Areas.Admin.Controllers;
[Area("admin")]
[Authorize]
public class ProductController : Controller
{
    private readonly ProductServices _services;
    private readonly CategoryServices _categoryServices;

    public ProductController(ProductServices services, CategoryServices categoryServices)
    {
        _services = services;
        _categoryServices = categoryServices;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var list = await _services.GetProducts();
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _categoryServices.GetCategories();
        return View(new ProductDto());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ProductDto productDto)
    {
        ViewBag.Categories = await _categoryServices.GetCategories();
        if (ModelState.IsValid)
        {
            await _services.InsertProduct(productDto);
            return RedirectToAction("Index");
        }

        return View(productDto);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.Categories = await _categoryServices.GetCategories();
        var find = await _services.GetProductById(id);
        return View(find);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(ProductDto productDto)
    {
        ViewBag.Categories = await _categoryServices.GetCategories();
        if (ModelState.IsValid)
        {
            await _services.UpdateProduct(productDto);
            return RedirectToAction("Index");
        }

        return View(productDto);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _services.DeleteProduct(id);
        return RedirectToAction("Index");
    }
}