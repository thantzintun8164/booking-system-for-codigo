using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookingClassManagementApi.Models
{
    public class UserPaymentCard
    {
        [Key]
        public int Id { get; set; }
        public string CardNumber { get; set; }     
        public string ExpireDate { get; set; } // only month and year
        public DateTime CreatedAt { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public List<User>? Users { get;}
    }
}
