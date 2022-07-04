using Dapper;
using Microsoft.Data.Sqlite;
using (var connection = new SqliteConnection("Data Source=./data/demo.db"))
{
    connection.Open();
    

    connection.Execute("INSERT INTO Dog VALUES (16, 4, 'dog4', 16.9)");
}
/**using (var connection = new SqliteConnection("Data Source=./data/demo.db"))
{
    connection.Open();

    connection.Execute("DELETE FROM Dog WHERE Id=3");
}**/
using (var connection = new SqliteConnection("Data Source=./data/demo.db"))
{
    connection.Open();

    connection.Execute("UPDATE Dog SET Weight=100 WHERE Id=1");
}
using (var connection = new SqliteConnection("Data Source=./data/demo.db"))
{
    connection.Open();

    var sql = "SELECT * FROM Dog";
    var result = connection.Query<Dog1>(sql);

    foreach(var dog in result)
    {
        Console.WriteLine(dog);
    }
}


class Dog1
{
    public string Name {get; set;}

    public int Age {get; set;}

    public int Id {get; set;}

    public double Weight{get;set;}

    public override string ToString()
    {
        return $"DOG {Id}: Name {Name} | Age {Age} | Weight {Weight} ";
    }
}