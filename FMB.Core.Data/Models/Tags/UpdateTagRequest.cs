using FMB.Core.Data.Models.BaseTypes;

namespace FMB.Core.Data.Models.Tags
{
    public class UpdateTagRequest : IEntity<long>
    {
        public string NewName { get; set; }
    }
}
