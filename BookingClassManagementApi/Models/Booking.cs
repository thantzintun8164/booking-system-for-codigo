using System.ComponentModel.DataAnnotations.Schema;

namespace BookingClassManagementApi.Models
{
    public class Booking
    {
        public int Id { get; set; }     
        public string BookingName { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } // status for conditions for CANCEL,BOOKED,REFUND and WAITING 

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        [ForeignKey("Schdule")]
        public int SchduleId { get; set; }
        public Schdule? Schdule { get; set; }
        
    }
}
