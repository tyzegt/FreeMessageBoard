using FMB.Services.Tags.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMB.Services.Tags
{
    public interface ITagService
    {
        long CreateTag(string name);
        void DeleteTag(long id);
        Tag GetTag(long id);
        void UpdateTag(long id, string name);
    }
}
