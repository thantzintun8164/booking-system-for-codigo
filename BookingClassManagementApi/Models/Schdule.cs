using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace BookingClassManagementApi.Models
{
    public class Schdule
    {
        public int SchduleId { get; set; }
        public string SchduleName { get;}
        public DateTime FromDate { get; set; }
        public string FromTime { get; set; }
        public DateTime ToDate { get; set; }
        public string ToTime { get; set; }
        
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public virtual Class? Class { get; set; }
    }
}
