using System.ComponentModel.DataAnnotations;

namespace BookingClassManagementApi.ViewModels
{
    public class ChangePasswordRequestVM
    {
        [Required]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string OldPassword { get; set; } = String.Empty;
        [Required]
        public string NewPassword { get; set; } = String.Empty;
    }
}
