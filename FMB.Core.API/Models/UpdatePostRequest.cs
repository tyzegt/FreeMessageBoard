using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace FMB.Core.API.Models
{
    public class UpdatePostRequest
    {
        public int Id { get; set; }
        public string NewBody { get; set; }
        public string NewTitle { get; set; }
    }
}