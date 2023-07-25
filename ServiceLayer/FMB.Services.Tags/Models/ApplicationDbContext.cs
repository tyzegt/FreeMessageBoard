using FMB.Services.Tags.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Comments.Models
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: вынести в конфиги
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=TagsDB;User Id=postgres;Password=qwerty;"); 
        }
    }
}
