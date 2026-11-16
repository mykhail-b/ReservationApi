namespace Reservation.Domain.Entity
{
    public class Booking
    {
        public Guid Id { get; set; }

        public Guid ResourceId { get; set; }
        public Guid ReserverId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
