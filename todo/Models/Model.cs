using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace todo.Models {
    public class ModelContext : IdentityDbContext<ApplicationUser> {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Todo>()
                .Property(e => e.ID)
                .UseNpgsqlSerialColumn();
        }
    }
}