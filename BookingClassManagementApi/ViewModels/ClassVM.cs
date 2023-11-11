namespace BookingClassManagementApi.ViewModels
{
    public class ClassVM
    {
        public int? Id { get; set; }
        public string ClassName { get; set; }
        public string? Description { get; set; }
        public int? TotalHour { get; set; }
        public int CountryId { get; set; }
    }
}
