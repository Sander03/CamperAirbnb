using CamperAirbnb.Controllers;
using CamperAirbnb.Model;
using LiteDB;
using System.Collections.Generic;

namespace CamperAirbnb.Database.UserDatabase;

public class UserContext : IUserContext
{
    private readonly string _connectionString = @"Filename=campingdatabase.db;connection=shared";

    public void Add(User user)
    {
        using var database = new LiteDatabase(_connectionString);
        var collection = database.GetCollection<User>("User");
        collection.Insert(user);
    }

    public void AddUser(User user)
    {
        using var database = new LiteDatabase(_connectionString);
        var collection = database.GetCollection<User>("User");
        collection.Insert(user);
    }

    public IEnumerable<User> GetAll()
    {
        using var database = new LiteDatabase(_connectionString);
        var collection = database.GetCollection<User>("User");
        return collection.FindAll();
    }

    public User GetById(int id)
    {
        using var database = new LiteDatabase(_connectionString);
        var collection = database.GetCollection<User>("User");
        return collection.FindById(id);
    }

    public User UpdateUser(User user)
    {
        using var database = new LiteDatabase(_connectionString);
        var collection = database.GetCollection<User>();
        collection.Update(user);
        return user;
        
    }
}
