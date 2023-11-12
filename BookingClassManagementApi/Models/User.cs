using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookingClassManagementApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public bool EmailConfirmed { get; set; } = false;
        public int OwnedCredit { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Activated { get; set; } = false;
         
        //one to many
        [JsonIgnore]
        public List<Package>? Packages { get;}
        [JsonIgnore]
        public List<UserPaymentCard>? UserPaymentCards { get; }
    }
}
