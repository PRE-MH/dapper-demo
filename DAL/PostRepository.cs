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
        return result.ToList();
    }
    public IList<Post> SelectAllPosts()
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<Post>("SELECT * FROM Post");
        return result.ToList();
    }
    public void Add(string title, string content, int ownerid)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        connection.Execute("INSERT INTO Post (Title, Content, OwnerId) VALUES (@title, @content, @ownerid)", new { title, content, ownerid });
    }
    public void Delete(int PostId)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");        
        connection.Execute("DELETE FROM Post WHERE Id=@id", new { PostId});
    }

    public void Update(int id, string title, string content, int ownerid)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        connection.Execute("UPADATE Post SET Title=@title, Content=@content, OwnerId=@ownerid WHERE Id=@id", new {id, title, content, ownerid });
    }
    public int VerifyPostById(int PostId){
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<Post>("SELECT * FROM Post WHERE Id=@id", new { PostId});
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