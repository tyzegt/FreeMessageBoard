using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMB.Core.Data.Models.BaseTypes
{
    public class IEntity
    {
        [Display(Name = "Архивная запись")]
        public bool Archive { get; set; }
    }

    public class IEntity<TKey> : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public TKey Id { get; set; }
    }
}
