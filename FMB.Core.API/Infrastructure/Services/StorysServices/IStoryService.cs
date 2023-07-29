using FMB.Core.Data.Models.Storys;

namespace FMB.Core.API.Infrastructure.Services.StorysServices
{
    public interface IStoryService
    {
        IEnumerable<Story> Get(long?[] ids);

        IEnumerable<Story> Get(long id);

        Task Load();
    }
}
