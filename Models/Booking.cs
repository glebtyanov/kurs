using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace kurs.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int TourId { get; set; }

        public int ClientId { get; set; }

        [Required(ErrorMessage = "Введите дату бронирования")]
        public DateTime BookingDate { get; set; }
        public Client Client { get; set; }
        public Tour Tour { get; set; }
    }
}