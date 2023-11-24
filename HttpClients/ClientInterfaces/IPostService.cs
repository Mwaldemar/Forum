using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(
        string? userName, 
        string? titleContains,
        string? bodyContains
    );

    Task<PostBasicDto> GetByIdAsync(int postId);
    Task UpdateAsync(GetPostDto dto);
}