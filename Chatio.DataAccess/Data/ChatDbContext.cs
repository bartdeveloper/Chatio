using Chatio.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatio.DataAccess.Data
{
    public class ChatDbContext : DbContext
    {

        public DbSet<ChatMessage> Messages { get; set; }

        public ChatDbContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=ChatDatabase.db");
        }

    }
}