using FMB.Core.Data.Models.Storys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Core.Data.Data
{
    public partial class IdentityContext
    {
        public DbSet<Story> Storys { get; set; }
    }
}
