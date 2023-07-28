using FMB.Core.Data.Models.BaseTypes;

namespace FMB.Core.Data.Models.Tags
{
    public class CreateTagRequest : IEntity<long>
    {
        public string Name { get; set; }
    }
}
