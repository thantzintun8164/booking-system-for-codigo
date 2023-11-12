using System.ComponentModel.DataAnnotations;

namespace BookingClassManagementApi.ViewModels
{
    public class PurchasePackageRequestVM
    {
        [Required]
        public int PackageId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
    }
}
