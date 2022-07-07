using Dapper;
using Microsoft.Data.Sqlite;
using DapperDemo.Models;
namespace DapperDemo.DAL;

public class PostRepository
{

    public int GetPostsCountByUserId(int userId)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<Post>("SELECT * FROM User u,Post p WHERE u.Id=p.OwnerId and p.OwnerId=@id", new { userId });
        return result.Count();
    }

    public IList<Post> GetPostsByUserId(int userId)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<Post>("SELECT * FROM User u,Post p WHERE u.Id=p.OwnerId and p.OwnerId=@id", new { userId });
        Console.WriteLine("Cet utilisateur a " + result.Count() + " poste(s):");
        return result.ToList();
    }

    public void Postes(int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<User>("SELECT * FROM User WHERE Id=@id", new { id });
        if (result.Any())
        {
            var result1 = connection.Query<Post>("SELECT * FROM User u,Post p WHERE u.Id=p.OwnerId and p.OwnerId=@id", new { id });
            if (result1.Any())
            {
                Console.WriteLine("Cet utilisateur a " + result1.Count() + " poste(s):");
                Console.WriteLine("");
                foreach (var x in result1)
                {
                    Console.WriteLine("Post " + x.Id + "= Title: " + x.Title + " | Content: " + x.Content);
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("Cet utilisateur n'a aucune poste");
            }
        }
        else
        {
            Console.WriteLine("Cet utilisateur n'existe pas");
        }
    }
    public void tous_postes()
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<Post>("SELECT * FROMPost");
        foreach(var x in result){
            Console.WriteLine(x);
        }
    }

    public void Add(string title, string content, int ownerid)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<User>("SELECT * FROM User WHERE Id=@id ", new { ownerid });
        if (result.Any())
        {
            connection.Execute("INSERT INTO Post (Title, Content, OwnerId) VALUES (@title, @content, @ownerid)", new { title, content, ownerid });
            Console.WriteLine("Ajout teriminé avec succès");
        }
        else
        {
            Console.WriteLine("Utilisateur n'existe pas");
        }
    }
    public void Delete(int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<Post>("SELECT * FROM Post WHERE Id=@id", new { id });
        if (result.Any())
        {
            connection.Execute("DELETE FROM Post WHERE Id=@id", new { id });
            Console.WriteLine("Suppression terminée avec succès");
        }
        else
        {
            Console.WriteLine("Cette poste n'existe pas déja");
        }

    }

    public void Update(int id, string title, string content, int ownerid)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<Post>("SELECT * FROM Post WHERE Id=@id", new { id });
        connection.Execute("UPADATE Post SET Title=@title, Content=@content, OwnerId=@ownerid", new { title, content, ownerid });
    }
    public int Verify(int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<Post>("SELECT * FROM Post WHERE Id=@id", new { id });
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