using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace todo.Models
{
    public class ModelContext : IdentityDbContext<ApplicationUser>
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }
    }

    public class Todo
    {
        public int ID { get; set; }
        [Required]
        [MinLengthAttribute(10)]
        public string value { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public List<Todo> Todos { get; set; }
    }
}