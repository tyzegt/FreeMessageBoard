using FMB.Core.Data.Data;
using FMB.Core.Data.Models.Storys;
using Microsoft.EntityFrameworkCore;

namespace FMB.Core.API.Infrastructure.Services.StorysServices
{
    public class StoryService: IStoryService
    {
        List<Story> _stories;
        private readonly IServiceProvider _serviceProvider;

        public StoryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Load()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<IdentityContext>();
            _stories = await context.Storys.Select(x => new Story
            { 
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public IEnumerable<Story> Get(long id)
        {
            var result = _stories.Where(x => x.Id == id).ToList();
            result.AddRange(Get(new long?[] { id }));

            return result;
        }
        public IEnumerable<Story> Get(long?[] ids)
        {
            var result = _stories.Where(x => ids.Contains(x.ParentId)).ToList();

            ids = result.Select(x => (long?)x.Id).ToArray();

            if (_stories.Any(x => ids.Contains(x.ParentId)))
            {
                result.AddRange(Get(ids));
            }

            return result;
        }
    }
}
