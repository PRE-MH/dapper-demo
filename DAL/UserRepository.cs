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

    public void Add(User user)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        if (GetById(user.Id) != null)
        {
            throw Exception;
        }
        else
        {
            connection.Execute("INSERT INTO User (Name) VALUES (@Name)", new { user.Name });
            Console.WriteLine("Ajout terminé avec succès");
        }
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        if (GetById(id) != null)
        {
            var result = connection.Query<User>("SELECT * FROM POST p , User u WHERE u.Id=p.OwnerId and p.OwnerId=@id", new { id });
            if (result.Any())
            {
                Console.WriteLine("Cet utilisateur a déja " + result.Count() + " poste(s).");
                Console.WriteLine("Voulez-vous le supprimer ?");
                string answer = "";
                do
                {
                    answer = Console.ReadLine();
                } while (answer.ToUpper() != "OUI" && answer.ToUpper() != "NON");
                if (answer.ToUpper() == "YES")
                {
                    connection.Execute("DELETE FROM User WHERE Id=@id", new { id });
                    Console.WriteLine("Suppression terminée avec succès");
                }
                else
                {
                    Console.WriteLine("Suppression annulée");
                }
            }
        }
        else
        {
            Console.WriteLine("Utilisateur n'existe pas déja");
        }
    }

    public void Update(int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        if (GetById(id) != null)
        {
            Console.WriteLine("Donner le nouveau nom");
            string name = Console.ReadLine();
            connection.Execute("UPDATE User SET Name=@name WHERE Id=@id", new { id, name });
            Console.WriteLine("Mise à jour terminée avec succès");
        }
        else
        {
            Console.WriteLine("Utilisateur n'existe pas");

        }
    }
    public void Select()
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<User>("SELECT * FROM User ");
        Console.WriteLine("Table User :");
        foreach (var x in result)
        {
            Console.WriteLine(x);
        }
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