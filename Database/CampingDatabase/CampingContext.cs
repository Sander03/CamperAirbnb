using CamperAirbnb.Model;
using LiteDB;

namespace CamperAirbnb.Database.CampingDatabase;

public class CampingContext : ICampingContext
{
    private readonly string _connectionString = @"Filename=campingdatabase.db;connection=shared";

    public void AddCamping(Camping camping)
    {
        using var database = new LiteDatabase(_connectionString);
        database.GetCollection<Camping>("Camping").Insert(camping);
    }

    public IEnumerable<Camping> GetAll()
    {
        using var database = new LiteDatabase(_connectionString);
        return database.GetCollection<Camping>("Camping").FindAll();
    }

    public Camping GetById(int id)
    {
        using var database = new LiteDatabase(_connectionString);
        var collection = database.GetCollection<Camping>("Camping");
        return collection.FindById(id);
    }

    public Camping UpdateCamping(Camping camping)
    {
        using var database = new LiteDatabase(_connectionString);
        var collection = database.GetCollection<Camping>("Camping");
        collection.Update(camping);
        return camping;
    }
}
