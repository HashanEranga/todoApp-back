using Microsoft.EntityFrameworkCore;
using todoApp_back.Models;

namespace todoApp_back.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<Todo> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().HasData(
                new Todo { Id = 1, Description = "first task"},
                new Todo { Id = 2, Description = "second task"}
            );
            
        }
    }
}
