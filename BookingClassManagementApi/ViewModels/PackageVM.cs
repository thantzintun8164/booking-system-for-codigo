namespace BookingClassManagementApi.ViewModels
{
    public class PackageVM
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int CreditCost { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool? IsExpire { get; set; }
        public int ClassId { get; set; }
    }
}
