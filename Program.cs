using Dapper;
using Microsoft.Data.Sqlite;

using (var connection = new SqliteConnection("Data Source=./data/demo.db"))
{
    connection.Open();

    var sql = "SELECT * FROM Dog";
    var result = connection.Query<Dog>(sql);

    foreach(var dog in result)
    {
        Console.WriteLine(dog);
    }
}


class Dog
{
    public string Name {get; set;}

    public int Age {get; set;}

    public int Id {get; set;}

    public override string ToString()
    {
        return $"DOG {Id}: {Name} {Age}";
    }
}
