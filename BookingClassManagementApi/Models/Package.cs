using System.ComponentModel.DataAnnotations.Schema;

namespace BookingClassManagementApi.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CreditCost { get; set;}

        public DateTime ExpireDate { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country? Country { get; set; }
    }
}
