using Microsoft.EntityFrameworkCore;

namespace Post.Microservice.Context
{
    public interface IApplicationContext
    {
        DbSet<Models.Post> Posts { get; set; }

        Task<int> SaveChanges();
    }
}