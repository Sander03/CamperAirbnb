using CamperAirbnb.Model;

namespace CamperAirbnb.Database.CampingDatabase;

public interface ICampingContext
{
    IEnumerable<Camping> GetAll();
    Camping GetById(int id);
    void AddCamping(Camping camping);
    Camping UpdateCamping(Camping camping);

}
