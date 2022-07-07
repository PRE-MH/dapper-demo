using Dapper;
using Microsoft.Data.Sqlite;
using DapperDemo.Models;

namespace DapperDemo.DAL;

public class UserRepository
{
    public User GetById(int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<User>("SELECT Id FROM User WHERE Id=@id", new { id });
        if (result.Any())
        {
            return result.First();
        }
        else
        {
            return null;
        }
    }

    public User GetByName(int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<User>("SELECT Name FROM User WHERE Id=@id", new { id });
        return result.First();
    }

    public IList<User> GetAll(User user)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<User>("SELECT * FROM User WHERE Name=@name or Id=@id", user);
        return result.ToList();
    }

    public void Add(string name)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        connection.Execute("INSERT INTO User (Name) VALUES (@Name)", new { name});
        Console.WriteLine("Ajout terminé avec succès");
    }
    public void Delete(int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");        
        connection.Execute("DELETE FROM User WHERE Id=@id", new { id });
    }

    public void Update(int id,string name)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        connection.Execute("UPDATE User SET Name=@name WHERE Id=@id", new { id, name });
    }
    public IList<User> SelectAllUsers()
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<User>("SELECT * FROM User ");
        return result.ToList();
    }

    public int VerifyUserById(int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result=connection.Query<User>("SELECT * FROM User WHERE Id=@id", new {id});
        if (result.Any())
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    
}