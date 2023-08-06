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
        public CommentsContext(DbContextOptions<CommentsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Comment> Comments { get; set; }
    }
}