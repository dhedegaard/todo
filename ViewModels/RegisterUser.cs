using System.ComponentModel.DataAnnotations;

namespace todo.ViewModels
{
    public class RegisterUser
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password again")]
        public string Password2 { get; set; }
    }
}