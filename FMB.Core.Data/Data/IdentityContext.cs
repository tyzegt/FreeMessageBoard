using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FMB.Core.Data.Data
{
    //TODO: предлагаю переименовать IdentityContext в какое то общее наименование - чтобы был один контекст для всего (идентификации и бд)
    public partial class IdentityContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
