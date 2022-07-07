namespace DapperDemo.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int OwnerId { get; set; }
    public override string ToString()
    {
        return $"Post {Id}= Title: {Title} | Content: {Content}  | OwnerId: {OwnerId}";
    }
}
