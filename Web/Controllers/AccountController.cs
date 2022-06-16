using System.Security.Claims;
using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Account;


public class AccountController:Controller   
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly AccountService _accountService;

    public AccountController(SignInManager<IdentityUser> signInManager, AccountService accountService)
    {
        _signInManager = signInManager;
        _accountService = accountService;
    }
    
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new Login() { ReturnUrl = returnUrl });
    }
   
    
    [HttpPost]
    public async Task<IActionResult> Login(Login model)
    {
        if (ModelState.IsValid)
        {
            var user = await _accountService.Login(model);
            if (user == null) return View(model);
            await _signInManager.SignInAsync(user, false, null);
            
            if(!string.IsNullOrEmpty(model.ReturnUrl))
            { 
                return Redirect(model.ReturnUrl); 
            }
        }

        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View(new Register());
    }

    [HttpPost]
    public async Task<IActionResult> Register(Register model)
    {
        if (!ModelState.IsValid) return View(model);
        await _accountService.Register(model);
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }
    
}