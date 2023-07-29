using FMB.Core.Data.Models.BaseTypes;
using Microsoft.AspNetCore.Mvc;

namespace FMB.Core.API.Controllers.BaseController
{
    public class BaseControllers<T> : ControllerBase where T : IEntity<int>, new()
    {
        private readonly IServiceEditor<T> _serviceEditor;

        public BaseControllers(IServiceEditor<T> serviceEditor)
        {
            _serviceEditor = serviceEditor;
        }

        /// <summary>
        /// Получить таблицу списком
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult> Get()
        {
            var entity = await _serviceEditor.Get();
            if (entity == null)
            {
                return NoContent();
            }
            return Ok(entity);
        }

        /// <summary>
        /// Информация об записи
        /// </summary>
        /// <returns></returns>        
        [HttpGet("{id:long}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var entity = await _serviceEditor.Find(x => x.Id == id);
            if (entity == null) return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] T model)
        {
            await _serviceEditor.Post(model);

            return Ok();
        }

        /// <summary>
        /// Изменение записи
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] T value)
        {
            if (!await _serviceEditor.Exists(x => x.Id == id))
                return BadRequest("Запрашиваемый элемент не найден");


            await _serviceEditor.Put(value);

            return Ok();
        }
    }
}
