using FMB.Core.Data.Models.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FMB.Core.Data.Models.Storys
{

    //TODO Пример реализации таблицы по сущности
    [Table("Story", Schema = "Story")]
    public class Story: IEntity<long>
    {
        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Не указано наименование")]
        [StringLength(80, ErrorMessage = "Наименование не должно превышать 80 символов")]
        public string Name { get; set; }

        //Ссылка на родителя
        public int? ParentId { get; set; }
    }
}
