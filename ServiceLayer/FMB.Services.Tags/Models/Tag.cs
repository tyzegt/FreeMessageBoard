using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Tags.Models
{
    public class Tag
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
