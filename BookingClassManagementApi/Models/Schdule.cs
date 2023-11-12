using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace BookingClassManagementApi.Models
{
    public class Schdule
    {
        public int SchduleId { get; set; }
        public string SchduleName { get;}
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? StartTime { get; set; } // eg: 10AM
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }
    }
}
