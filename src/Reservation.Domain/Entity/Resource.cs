namespace Reservation.Domain.Entity
{
    public class Resource
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public decimal Cost { get; set; }
    }
}
