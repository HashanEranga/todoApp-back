using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using todoApp_back.Data;
using todoApp_back.Models;

namespace todoApp_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        public TodoController(TodoContext todoContext)
        {
            this._todoContext = todoContext;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetAllTodoItems()
        {
            return Ok(await _todoContext.TodoItems.ToListAsync());
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddTodoItem([FromBody] Todo todoObj)
        {
            try
            {
                await _todoContext.TodoItems.AddAsync(todoObj);
                _todoContext.SaveChanges();
                var newId = _todoContext.TodoItems.OrderBy(x=>x.Id).Select(x=>x.Id).LastOrDefault();
                return Ok(newId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("items")]
        public IActionResult EditTodoItem([FromBody] Todo todoObj)
        {
            try
            {
                _todoContext.TodoItems.Update(todoObj);
                _todoContext.SaveChanges();
                return Ok(todoObj);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("item")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            try
            {
                var removeItem = await _todoContext.TodoItems.FirstAsync(x => x.Id == id);
                _todoContext.TodoItems.Remove(removeItem);
                _todoContext.SaveChanges();
                return Ok(removeItem);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
