using System.ComponentModel.DataAnnotations;

namespace kurs.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Введите номер телефона")]
        public string PhoneNumber { get; set; }
        public List<Booking> Bookings {get; set;}
    }
}