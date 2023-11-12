using BookingClassManagementApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingClassManagementApi.ViewModels
{
    public class PurchasedPackageVM
    {
        public int PurchaseId { get; set; }
        public PackageVM? PackageVM { get; set; }
    }
}
