using CamperAirbnb.Enum;

namespace CamperAirbnb.Model;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Booking> Bookings { get; set; }
    public Role Role { get; set; }
    public User() { }
}
