using System.ComponentModel.DataAnnotations.Schema;

namespace BookingClassManagementApi.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int CreditCost { get; set;}

        public DateTime ExpireDate { get; set; }
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }
    }
}
