using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace todo.Models
{
    public class ModelContext : IdentityDbContext<ApplicationUser>
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Todo>()
                .Property(e => e.ID)
                .UseNpgsqlSerialColumn();
        }
    }

    public class Todo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [Required]
        [MinLengthAttribute(10)]
        public string value { get; set; }
        public virtual ApplicationUser user { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public List<Todo> Todos { get; set; }
    }
}