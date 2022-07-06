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
new UserRepository().Select();
string answer="";
do{
    Console.WriteLine("-----------------------");
    Console.WriteLine("Donner votre choix");
    Console.WriteLine("1: Ajouter");
    Console.WriteLine("2: Supprimer");
    Console.WriteLine("3: Mettre à jour");
    Console.WriteLine("4: Afficher les postes d'un utilisateur");
    Console.WriteLine("5: Afficher tous les postes");
    int x=0;
    do{
    x=Convert.ToInt32(Console.ReadLine());
    }
    while ( x<1 || x >5 );
    User user=new User();
    switch (x) {
        case 1:
            Console.WriteLine("Donner un nom");
            string name=Console.ReadLine();
            user.Name=name;        
            new UserRepository().Add(user);  
            Console.WriteLine("--------------------------");
            Console.WriteLine("Votre nouvelle Base de données :");
            new UserRepository().Select();
        break;
        case 2:
            Console.WriteLine("Donner l'id");
            int y =Convert.ToInt32(Console.ReadLine());
            user.Id=y;
            new UserRepository().Delete(y);  
            Console.WriteLine("--------------------------");
            Console.WriteLine("Votre nouvelle Base de données :");
            new UserRepository().Select();
        break;
        case 3:
            Console.WriteLine("Donner l'id");
            int y1 =Convert.ToInt32(Console.ReadLine());
            user.Id=y1;
            new UserRepository().Update(y1);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Votre nouvelle Base de données :");
            new UserRepository().Select();
        break;
        case 4:
            Console.WriteLine("Donner l'id");
            int y2 =Convert.ToInt32(Console.ReadLine());
            user.Id=y2;
            new UserRepository().Postes(y2);
        break;
        case 5:
            new UserRepository().tous_postes();
        break;
    }
    do{
        Console.WriteLine("Essayer autre chose ?");
        answer=Console.ReadLine();
    }while(answer.ToUpper()!="OUI" && answer.ToUpper()!="NON");
}while(answer.ToUpper()=="OUI");
Console.WriteLine("BYE BYE");

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
            Console.WriteLine("Ajout terminé avec succès");
        }        
    }

    public void Delete (int id)
    {
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        if(GetById(id)!=null){
            var result=connection.Query<User>("SELECT * FROM POST p , User u WHERE u.Id=p.OwnerId and p.OwnerId=@id",new{id});
            if(result.Any()){
                Console.WriteLine("Cet utilisateur a déja " +result.Count()+" poste(s).");
                Console.WriteLine("Voulez-vous le supprimer ?");
                string answer="";
                do{
                    answer=Console.ReadLine();
                }while(answer.ToUpper()!="OUI" && answer.ToUpper()!="NON");
                if(answer.ToUpper()=="YES"){
                    connection.Execute("DELETE FROM User WHERE Id=@id", new {id});
                    Console.WriteLine("Suppression terminée avec succès");
                }else{
                    Console.WriteLine("Suppression annulée");
                }
            }
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
            Console.WriteLine("Mise à jour terminée avec succès");
        }else{
            Console.WriteLine("Utilisateur n'existe pas");
            
        }  
    }
    public void Select (){
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result=connection.Query<User>("SELECT * FROM User ");
        Console.WriteLine("Table User :");
        foreach(var x in result){
            Console.WriteLine(x);
        }
    }
    public void Postes(int id){
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var result=connection.Query<User>("SELECT * FROM User WHERE Id=@id",new{id});
        if(result.Any()){
            var result1=connection.Query<Post>("SELECT * FROM User u,Post p WHERE u.Id=p.OwnerId and p.OwnerId=@id",new{id});
            if(result1.Any()){
                Console.WriteLine("Cet utilisateur a "+result1.Count()+" poste(s):");
                Console.WriteLine("");
                foreach(var x in result1){
                    Console.WriteLine("Post "+x.Id+"= Title: "+x.Title+" | Content: "+x.Content);
                    Console.WriteLine("");
                }
            }else{
                Console.WriteLine("Cet utilisateur n'a aucune poste");
            }
        }else{
            Console.WriteLine("Cet utilisateur n'existe pas");
        }
    }
    public void tous_postes(){
        var connection = new SqliteConnection("DataSource=./data/demo1.db");
        var sql =
        @"select * from Post p
        left join User u on u.Id = p.OwnerId
        Order by p.Id";
        var data = connection.Query<Post, User, Post>(sql, (post, user) => { post.Owner = user; return post;});
        foreach(var post in data)
        {
            Console.WriteLine(post);
        }
    }
}