using System.Text.Json.Serialization;

namespace Domain.DTOs;

public class PostBasicDto
{
    public int Id { get; }
    public string OwnerUsername { get; }
    public string Title { get; }
    public string Body { get; }
    
    public PostBasicDto(int id, string ownerUsername, string title, string body)
    {
        Id = id;
        OwnerUsername = ownerUsername;
        Title = title;
        Body = body;
    }
}