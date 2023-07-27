using FMB.Services.Comments.Models;
using FMB.Services.Tags.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FMB.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly TagsContext _tagsContext;

        public TagService(TagsContext tagsContext)
        {
            _tagsContext = tagsContext;
        }

        public long CreateTag(string name)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            if (_tagsContext.Tags.Any(x => x.Name == name)) throw new ArgumentException($"tag '{name}' already exists");
            var tag = new Tag { Name = name };
            _tagsContext.Tags.Add(tag);
            _tagsContext.SaveChanges();
            return tag.Id;
        }

        public Tag GetTag(long id)
        {
            var tag = _tagsContext.Tags.FirstOrDefault(x => x.Id == id);
            if (tag != null) return tag;
            throw new KeyNotFoundException($"tag with id '{id}' not found");
        }

        public void UpdateTag(long id, string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            var tag = _tagsContext.Tags.FirstOrDefault(x => x.Id == id);
            if (tag != null)
            {
                tag.Name = name;
                _tagsContext.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"tag with id '{id}' not found");
            }

        }

        public void DeleteTag(long id) // consider DTO
        {
            var tag = _tagsContext.Tags.FirstOrDefault(x => x.Id == id);
            if (tag != null)
            {
                _tagsContext.Remove(tag);
                _tagsContext.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"tag with id '{id}' not found");
            }
        }
    }
}
