namespace BookingClassManagementApi.ViewModels
{
    public class UserVM
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int OwnedCredit { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Token { get; set; }
    }
}
