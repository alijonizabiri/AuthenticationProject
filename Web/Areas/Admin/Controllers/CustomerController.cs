using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Areas.Admin.Controllers;
[Area("admin")]
[Authorize]

public class CustomerController : Controller
{
    private readonly CustomerServices _services;

    public CustomerController(CustomerServices services)
    {
        _services = services;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var list = await _services.GetCustomers();
        return View(list);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(new CustomerDto());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CustomerDto customerDto)
    {
        if (ModelState.IsValid)
        {
            await _services.InsertCustomer(customerDto);
            return RedirectToAction("Index");
        }

        return View(customerDto);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var find = await _services.GetCustomerById(id);
        return View(find);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(CustomerDto customerDto)
    {
        if (ModelState.IsValid)
        {
            await _services.UpdateCustomer(customerDto);
            return RedirectToAction("Index");
        }

        return View(customerDto);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _services.DeleteCustomer(id);
        return RedirectToAction("Index");
    }
}