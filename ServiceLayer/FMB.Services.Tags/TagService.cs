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

        public void CreateTag(string name)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            if (_tagsContext.Tags.Any(x => x.Name == name)) throw new ArgumentException($"tag '{name}' already exists");
            _tagsContext.Tags.Add(new Tag { Name = name });
            _tagsContext.SaveChanges();
        }

        public Tag GetTag(int id)
        {
            var tag = _tagsContext.Tags.FirstOrDefault(x => x.Id == id);
            if (tag != null) return tag;
            throw new KeyNotFoundException($"tag with id '{id}' not found");
        }

        public void UpdateTag(int id, string name)
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

        public void DeleteTag(int id) // consider DTO
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
