using Microsoft.Data.Sqlite;

using (var connection = new SqliteConnection("Data Source=./data/demo.db"))
{
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText =
    @"
        SELECT *
        FROM dog
    ";
    using (var reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            var value = reader.GetString(0);
            Console.WriteLine($"Hello, {value}!");
        }
    }
}
