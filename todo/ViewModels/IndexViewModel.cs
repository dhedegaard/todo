using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using todo.Models;

namespace todo.ViewModels {
    public class IndexViewModel {
        [Required]
        [Display(Name = "Add item")]
        public string NewTodo { get; set; }
        public IEnumerable<Todo> ExistingTodos { get; set; }
    }
}