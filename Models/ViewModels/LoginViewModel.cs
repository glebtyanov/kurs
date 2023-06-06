using System.ComponentModel.DataAnnotations;

namespace kurs.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        public string Login {get; set;}
        [Required(ErrorMessage = "Введите пароль")]
        public string Password {get; set;}
    }
}