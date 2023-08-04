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
        Task AssignPostTag(long tagId, long postId);
        Task<Tag?> CreateTag(string name);
        Task<bool> DeleteTag(long id);
        Task<IEnumerable<Tag>> GetPostTags(long postId);
        Task<Tag?> GetTag(long id);
        Task<bool> UpdateTag(long id, string name);
    }
}
