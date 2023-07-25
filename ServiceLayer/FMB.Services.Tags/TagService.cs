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
        public void CreateTag(string name)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            using (var db = new ApplicationDbContext()) // TODO: DI
            {
                if (db.Tags.Any(x => x.Name == name)) throw new ArgumentException($"tag '{name}' already exists");
                db.Tags.Add(new Tag { Name = name });
                db.SaveChanges();
            }
        }

        public Tag GetTag(int id)
        {
            using (var db = new ApplicationDbContext()) // TODO: DI
            {
                var tag = db.Tags.FirstOrDefault(x => x.Id == id);
                if (tag != null) return tag;
                throw new KeyNotFoundException($"tag with id '{id}' not found");
            }
        }

        public void UpdateTag(int id, string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            using (var db = new ApplicationDbContext()) // TODO: DI
            {
                var tag = db.Tags.FirstOrDefault(x => x.Id == id);
                if (tag != null)
                {
                    tag.Name = name;
                    db.SaveChanges();
                }
                else
                {
                    throw new KeyNotFoundException($"tag with id '{id}' not found");
                }
            }

        }

        public void DeleteTag(int id) // consider DTO
        {
            using (var db = new ApplicationDbContext()) // TODO: DI
            {
                var tag = db.Tags.FirstOrDefault(x => x.Id == id);
                if (tag != null)
                {
                    db.Remove(tag);
                    db.SaveChanges();
                }
                else
                {
                    throw new KeyNotFoundException($"tag with id '{id}' not found");
                }
            }

        }
    }
}
