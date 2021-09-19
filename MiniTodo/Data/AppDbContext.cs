using Microsoft.EntityFrameworkCore;
using MiniTodo.Models;

namespace MiniTodo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoV2> TodosV2 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}