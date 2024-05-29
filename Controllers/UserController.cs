using CamperAirbnb.Database.CampingDatabase;
using CamperAirbnb.Database.UserDatabase;
using CamperAirbnb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CamperAirbnb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private IUserContext _context;
    public UserController(IUserContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Policy = "BasicAuthentication")]

    public IActionResult GetAll() 
    {
        return Ok(_context.GetAll());
    }

    [HttpPost]
    //[Authorize(Policy = "BasicAuthentication")]

    public IActionResult Add([FromBody] User user)
    {
        try
        {
            _context.AddUser(user);
            return Ok(user);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var user = _context.GetAll().FirstOrDefault(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        return Ok(user);

    }

    [HttpPut]
    [Authorize(Policy = "BasicAuthentication")]
    public IActionResult UpdateUser([FromBody] UpdateUser updatedUser)
    {
        var userId = User.FindFirstValue(ClaimTypes.Sid); // will give the user's userId
        var dbUser = _context.GetById(Convert.ToInt32(userId));
        dbUser.Name = updatedUser.Name;
        dbUser.Email = updatedUser.Email;
        dbUser.Password = updatedUser.Password ?? dbUser.Password;
        var updateInformation = _context.UpdateUser(dbUser);
        return Ok();
    }
}
