using System.ComponentModel.DataAnnotations;

namespace BookingClassManagementApi.ViewModels
{
    public class PackageRequestVM
    {
        [Required]
        public int CountryId { get; set; }
        //Pagination
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int PageSize { get; set; }
    }
}
