using System.ComponentModel.DataAnnotations;

namespace todo.ViewModels {
    public class AddTodo {
        [Required]
        public string value { get; set; }
    }
}