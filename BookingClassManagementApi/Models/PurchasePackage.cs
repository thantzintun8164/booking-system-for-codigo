using System.ComponentModel.DataAnnotations.Schema;

namespace BookingClassManagementApi.Models
{
    public class PurchasePackage
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Package")]
        public int PackageId { get; set; }
        public virtual int Package { get; set; }
    }
}
