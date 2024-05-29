using CamperAirbnb.Model;

namespace CamperAirbnb.Database.UserDatabase;

public interface IUserContext
{
    IEnumerable<User> GetAll();
    void AddUser(User user);
    User GetById(int id);
    User UpdateUser(User user);


}
