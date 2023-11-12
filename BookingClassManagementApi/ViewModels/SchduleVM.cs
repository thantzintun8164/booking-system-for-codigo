namespace BookingClassManagementApi.ViewModels
{
    public class SchduleVM
    {
        public int? SchduleId { get; set; }
        public string SchduleName { get; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string StartTime { get; set; }
        public int? ClassId { get; set; }
    }
}
