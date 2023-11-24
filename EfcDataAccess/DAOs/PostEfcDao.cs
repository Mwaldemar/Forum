using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.Owner).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchParameters.OwnerUsername))
        {
            query = query.Where(post =>
                post.OwnerUsername.ToLower().Equals(searchParameters.OwnerUsername.ToLower()));
        }
        
        List<Post> result = await query.ToListAsync();
        return result;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        Post? found = await context.Posts
            .Include(post => post.Owner)
            .SingleOrDefaultAsync(post => post.Id == id);
        return found;
    }
}