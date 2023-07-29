using FMB.Services.Tags.Models;
using Microsoft.EntityFrameworkCore;

namespace FMB.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly TagsContext _tagsContext;

        public TagService(TagsContext tagsContext)
        {
            _tagsContext = tagsContext;
        }

        public async Task<Tag?> CreateTag(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            if (await _tagsContext.Tags.AnyAsync(x => x.Name == name))
                return null;
            
            var tag = new Tag { Name = name };
            
            _tagsContext.Tags.Add(tag);
            await _tagsContext.SaveChangesAsync();
            return tag;
        }

        public Task<Tag?> GetTag(long id)
            => _tagsContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> UpdateTag(long id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return await _tagsContext.Tags.Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, name)) > 0;

        }

        public async Task<bool> DeleteTag(long id) // consider DTO
            => await _tagsContext.Tags.Where(x => x.Id == id).ExecuteDeleteAsync() > 0;

        public async Task AssignTagToPost(long tagId, long postId)
        {
            if (_tagsContext.PostTags.Any(x => x.TagId == tagId && x.PostId == postId))
            {
                throw new Exception("tag already assigned");
            }
            _tagsContext.PostTags.Add(new PostTag { TagId = tagId, PostId = postId });
            await _tagsContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetPostTags(long postId)
        {
            var tags = _tagsContext.PostTags.Select(x => x.Tag).ToList();
            return null;
        }
    }
}
