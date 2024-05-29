using CamperAirbnb.Database.CampingDatabase;
using CamperAirbnb.Database.UserDatabase;
using CamperAirbnb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
namespace CamperAirbnb.Controllers;

[EnableCors]
[Route("api/[controller]")]
[ApiController]
public class CampController : ControllerBase
{
    private ICampingContext _campingContext;
    private IUserContext _userContext;

    public CampController(ICampingContext campingContext, IUserContext userContext)
    {
        _campingContext = campingContext;
        _userContext = userContext;
    }


    [HttpGet]

    //url/api/camp/getall
    public IActionResult GetAll()
    {
        return Ok(_campingContext.GetAll().ToList());
    }

    [HttpGet("{id}")]

    public IActionResult GetCampingById(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest("Id can not be less then 0");

            Camping? foundCamping = _campingContext.GetById(id);

            if (foundCamping == null)
                return BadRequest($"No camping found with id {id}");

            return Ok(foundCamping);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Policy = "BasicAuthentication")]

    public IActionResult Add([FromBody] Camping campingSpot)
    {
        try
        {
            _campingContext.AddCamping(campingSpot);   
            return Ok(campingSpot);
        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/camping")]
    [Authorize(Policy = "BasicAuthentication")]
    public IActionResult CreateBooking([FromBody] CreateBooking createBooking, int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.Sid);
        var dbUser = _userContext.GetById(Convert.ToInt32(userId));
        var dbcamping = _campingContext.GetById(id);
        var booking = new Booking
        {
            User = dbUser,
            Start = createBooking.Start,
            End = createBooking.End,
        };
        dbcamping.Bookings.Add(booking);
        var updateInformation = _campingContext.UpdateCamping(dbcamping);
        return Ok(updateInformation);
    }
}
