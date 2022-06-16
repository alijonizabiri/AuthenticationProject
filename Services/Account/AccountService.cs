using System.Security.Claims;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Services.Account;

public class AccountService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityUser> Login(Login login)
    {
        var user = await _userManager.FindByNameAsync(login.UserName);
        if (user == null) return null;

        var validationPassword = new PasswordValidator<IdentityUser>();
        var result = await validationPassword.ValidateAsync(_userManager, user, login.Password);

        if (result.Succeeded == false) return null;
        
        //fill claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        await _userManager.AddClaimsAsync(user, claims);

        return user;
    }

    public async Task<bool> Register(Register register)
    {
        var user = new IdentityUser()
        {
            Email = register.Email,
            UserName = register.UserName
        };
        var result = await _userManager.CreateAsync(user);

        return result.Succeeded;
    }
}