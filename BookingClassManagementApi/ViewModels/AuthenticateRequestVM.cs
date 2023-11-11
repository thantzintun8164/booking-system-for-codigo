using System.ComponentModel.DataAnnotations;

namespace BookingClassManagementApi.ViewModels
{
    public class AuthenticateRequestVM
    {
        [Required]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
