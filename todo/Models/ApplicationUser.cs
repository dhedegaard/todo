using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace todo.Models {
    public class ApplicationUser : IdentityUser {
        public List<Todo> Todos { get; set; }
    }
}