using System.ComponentModel.DataAnnotations;

namespace todo.Models {
    public class Todo {
        [Key]
        public int ID { get; set; }
        [Required]
        [MinLengthAttribute(10)]
        public string Value { get; set; }
        public virtual ApplicationUser user { get; set; }

        public override string ToString() =>
            $"<Todo id={ID} value=\"{Value}\" />";
    }
}