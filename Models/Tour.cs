using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace kurs.Models
{
    public class Tour
    {
        public int TourId { get; set; }
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Введите стоимость")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Введите дату начала")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Введите дату окончания")]
        public DateTime EndDate { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}