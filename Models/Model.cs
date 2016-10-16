using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace todo.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }
    }

    public class Todo
    {
        public int ID { get; set; }
        [Required]
        [MinLengthAttribute(10)]
        public string value { get; set; }
    }
}