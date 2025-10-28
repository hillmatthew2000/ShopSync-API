using ShopSync_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopSync_API.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) {}

         public DbSet<TaskItem> TaskItems { get; set; }
    }
}