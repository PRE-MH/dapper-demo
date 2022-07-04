using Dapper;
using Microsoft.Data.Sqlite;
using (var connection = new SqliteConnection("Data Source=./data/demo1.db"))
{
    var sql =
    @"select * from Post p
    left join User u on u.Id = p.OwnerId
    Order by p.Id";
    
    var data = connection.Query<Post, User, Post>(sql, (post, user) => { post.Owner = user; return post;});
    
    foreach(var post in data){
    
        Console.WriteLine(post);
    
    }
}
/**using (var connection = new SqliteConnection("Data Source=./data/demo.db"))
{
    connection.Open();
    int age=16;
    string name="dog1";
    double weight=15.3;
    connection.Execute("INSERT INTO Dog (Age, Name, Weight) VALUES (@age ,@name ,@weight )", new { age, name, weight});
}**/
/**using (var connection = new SqliteConnection("Data Source=./data/demo.db"))
{
    connection.Open();
    connection.Execute("DELETE FROM Dog WHERE Id=3");
}**/
/**using (var connection = new SqliteConnection("Data Source=./data/demo.db"))
{
    connection.Open();
    connection.Execute("UPDATE Dog SET Weight=100 WHERE Id=1");
}**/
/**using (var connection = new SqliteConnection("Data Source=./data/demo.db"))
{
    connection.Open();
    var sql = "SELECT * FROM Dog";
    var result = connection.Query<Dog>(sql);
    foreach(var dog in result)
    {
        Console.WriteLine(dog);
    }
}*/
class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public User Owner { get; set; }
    public int OwnerId { get; set ;}
    
    public override string ToString()
    {
    
        return $"Post {Id}= Title: {Title} | Owner: {Owner.Name} | Content: {Content} | OwnerId: {OwnerId} ";
    
    }
}
class User
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString(){
        return $"Name: {Name} | Id: {Id} ";
    }
}
