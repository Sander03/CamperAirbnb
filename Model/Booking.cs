using Microsoft.VisualBasic;

namespace CamperAirbnb.Model;

public class Booking
{
    public Booking()
    {
        
    }

    public Booking(DateTime start, DateTime end, Camping camping)
    {
        Start = start;
        End = end;
        Camping = camping;
        TotalPrice = CalculateTotalPrice();
    }

    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Camping Camping { get; set; }
    public User User { get; set; }
    public double TotalPrice { get; private set; }

    private double CalculateTotalPrice()
    {
        int days = End.Day - Start.Day;
        return Camping.PricePerNight * days;
    }
}
