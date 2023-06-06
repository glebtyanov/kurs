namespace kurs.Models
{
    public class BookingViewModel
    {
        public Booking Booking { get; set; }
        public List<Tour> Tours { get; set; }
        public List<Client> Clients {get; set;}
    }
}