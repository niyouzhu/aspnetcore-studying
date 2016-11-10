using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class TodoRepository : ITodoRepository
    {
        private static readonly ConcurrentDictionary<string, TodoItem> Todos = new ConcurrentDictionary<string, TodoItem>();

        public TodoRepository()
        {
            Add(new TodoItem() { Name = "Item1" });
        }

        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            Todos[item.Key] = item;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return Todos.Values;
        }

        public TodoItem Find(string key)
        {
            TodoItem item;
            Todos.TryGetValue(key, out item);
            return item;
        }

        public TodoItem Remove(string key)
        {
            TodoItem item;
            Todos.TryRemove(key, out item);
            return item;
        }

        public void Update(TodoItem item)
        {
            Todos[item.Key] = item;

        }
    }
}
