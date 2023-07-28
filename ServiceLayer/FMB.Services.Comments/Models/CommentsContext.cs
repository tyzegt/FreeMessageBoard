using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


#nullable disable

namespace FMB.Services.Comments.Models
{
    public class CommentsContext : DbContext
    {
        public CommentsContext() // Не добавлять DI пока не покроем тестами
        {
            Database.EnsureCreated();
        }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Не убирать, пока не покроем всё тестами
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=CommentsDB;User Id=postgres;Password=qwerty;");
        }
    }
}