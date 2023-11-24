using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        ValidatePost(dto);
        
        User? user = await userDao.GetByUsernameAsync(dto.OwnerUsername);
        if (user == null)
        {
            throw new Exception($"User {dto.OwnerUsername} was not found.");
        }
        
        Post post = new Post(user.Username, dto.Title, dto.Body);
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Todo with id {id} not found");
        }

        return new PostBasicDto(post.Id, post.Owner.Username, post.Title, post.Body);
    }

    private void ValidatePost(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty");
        if (string.IsNullOrEmpty(dto.OwnerUsername)) throw new Exception("Username cannot be empty");
    }
}