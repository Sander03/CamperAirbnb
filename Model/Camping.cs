namespace CamperAirbnb.Model;

public class Camping
{
    public Camping()
    {
        
    }

    public Camping(int id, string name, string description, double ppnight)
    {
        Id = id;
        Name = name;
        Description = description;
        PricePerNight = ppnight;
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double PricePerNight { get; set; }
    public string? Country { get; set; }
    public List<Booking> Bookings { get; set; } = new List<Booking>();
    public List<CampingSpecifications> Specifications { get; set; } = new List<CampingSpecifications>();
    public int OwnerId { get; set; }
}