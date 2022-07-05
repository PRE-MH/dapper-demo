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
using Dapper;
using Microsoft.Data.Sqlite;
/**using (var connection = new SqliteConnection("Data Source=./data/demo1.db"))
{
    var sql =
    @"select * from Post p
    left join User u on u.Id = p.OwnerId
    Order by p.Id";
    var data = connection.Query<Post, User, Post>(sql, (post, user) => { post.Owner = user; return post;});
    foreach(var post in data){
        Console.WriteLine(post);
    }
}**/
new UserRepository().Select();
Console.WriteLine("-----------------------");
Console.WriteLine("Donner votre choix");
Console.WriteLine("1: Ajouter");
Console.WriteLine("2: Supprimer");
Console.WriteLine("3: Mettre à jour");
int x=0;
do{
x=Convert.ToInt32(Console.ReadLine());
}
while ( x<1 || x >3 );
User user=new User();
switch (x) {
    case 1:
        Console.WriteLine("Donner un nom");
        string name=Console.ReadLine();
        user.Name=name;        
        new UserRepository().Add(user);  
    break;
    case 2:
        Console.WriteLine("Donner l'id");
        int y =Convert.ToInt32(Console.ReadLine());
        user.Id=y;
        new UserRepository().Delete(y);  
    break;
    case 3:
        Console.WriteLine("Donner l'id");
        int y1 =Convert.ToInt32(Console.ReadLine());
        user.Id=y1;
        new UserRepository().Update(y1);
    break;
}
Console.WriteLine("--------------------------");
Console.WriteLine("Votre nouvelle Base de données :");
new UserRepository().Select();
public class Post
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
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public override string ToString()
    {
        return $"User Id: {Id} | Name: {Name} ";   
    }    
}

public class UserRepository
{   
    
    public User GetById(int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result=connection.Query<User>("SELECT Id FROM User WHERE Id=@id",new {id});
        if(result.Any()){
            return result.First();
        }else{
            return null;
        }
    }
    
    public User GetByName (int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result=connection.Query<User>("SELECT Name FROM User WHERE Id=@id",new {id});
        return result.First();
    }
    
    public IList<User> GetAll(User user)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result = connection.Query<User>("SELECT * FROM User WHERE Name=@name or Id=@id",user);
        return result.ToList();
    }
    
    public void Add (User user)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        if(GetById(user.Id)!=null)
        {
            Console.WriteLine("Utilisateur existe déja");
        }
        else
        { 
            connection.Execute("INSERT INTO User (Name) VALUES (@Name)",new{user.Name});    
        }        
    }

    public void Delete (int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        if(GetById(id)!=null){
            connection.Execute("DELETE FROM User WHERE Id=@id", new {id});
        }else{
            Console.WriteLine("Utilisateur n'existe pas déja");
        }
    }

    public void Update (int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        if(GetById(id)!=null){
            Console.WriteLine("Donner le nouveau nom");
        string name=Console.ReadLine();
        connection.Execute("UPDATE User SET Name=@name WHERE Id=@id", new {id, name});      
        }else{
            Console.WriteLine("Utilisateur n'existe pas");
        }  
    }
    public void Select (){
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result=connection.Query<User>("SELECT * FROM User ");
        foreach(var x in result){
            Console.WriteLine(x);
        }
    }
}