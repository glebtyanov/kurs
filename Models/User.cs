
using System.ComponentModel.DataAnnotations;

namespace kurs.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}