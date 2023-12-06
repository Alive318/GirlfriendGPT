using GirlfriendGPT_API.Helper;
using GirlfriendGPT_API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GirlfriendGPT_API.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        // Check if the model is valid
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Create a new User object
        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
            // Add any additional properties as needed
        };

        // Attempt to create the user with the provided password
        var result = await _userManager.CreateAsync(user, model.Password);

        // Check if user creation was successful
        if (result.Succeeded)
        {
            // You may choose to sign the user in here if automatic login is desired
            // For simplicity, we'll return a success message without signing in

            return Ok(new { Message = "Registration successful" });
        }

        // If user creation was not successful, return errors
        return BadRequest(new { Errors = result.Errors });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        // Check if the model is valid
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Attempt to sign in the user with the provided credentials
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        // Check if login was successful
        if (result.Succeeded)
        {
            // If login was successful, generate and return a JWT token
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = JWTHelper.GenerateJwtToken(user, _configuration);
            
            return Ok(new { Token = token, Message = "Login successful" });
        }

        // If login was not successful, return an error message
        return Unauthorized(new { Message = "Invalid login attempt" });
    }
}