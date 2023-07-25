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
        void CreateTag(string name);
        void DeleteTag(int id);
        Tag GetTag(int id);
        void UpdateTag(int id, string name);
    }
}
