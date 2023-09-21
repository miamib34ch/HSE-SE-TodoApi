using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    public class TodoItemsController : ApiController
    {
        private TodoContext db = new TodoContext();

        // GET: api/TodoItems
        public IQueryable<TodoItem> GetTodoItems()
        {
            return db.TodoItems;
        }

        // GET: api/TodoItems/5
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult GetTodoItem(long id)
        {
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // PUT: api/TodoItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTodoItem(long id, TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            db.Entry(todoItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TodoItems
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult PostTodoItem(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TodoItems.Add(todoItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult DeleteTodoItem(long id)
        {
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            db.TodoItems.Remove(todoItem);
            db.SaveChanges();

            return Ok(todoItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TodoItemExists(long id)
        {
            return db.TodoItems.Count(e => e.Id == id) > 0;
        }
    }
}