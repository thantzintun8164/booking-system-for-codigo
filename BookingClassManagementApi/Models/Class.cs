using System.ComponentModel.DataAnnotations.Schema;

namespace BookingClassManagementApi.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string? Description { get; set; }        
        public int? TotalHour { get; set; }

        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
