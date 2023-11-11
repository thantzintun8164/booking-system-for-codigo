using BookingClassManagementApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingClassManagementApi.ViewModels
{
    public class PurchasePackageVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PackageId { get; set; }
    }
}
