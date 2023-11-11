namespace BookingClassManagementApi.ViewModels
{
    public class BookingVM
    {
        public int Id { get; set; }
        public string BookingName { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public int SchduleId { get; set; }
    }
}
