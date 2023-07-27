using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


#nullable disable

namespace FMB.Services.Comments
{
    public class CommentsContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public CommentsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: вынести в конфиги
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=CommentsDB;User Id=postgres;Password=qwerty;");
        }
    }
}