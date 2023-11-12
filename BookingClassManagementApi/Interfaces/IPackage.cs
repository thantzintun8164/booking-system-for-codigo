using BookingClassManagementApi.ViewModels;

namespace BookingClassManagementApi.Interfaces
{
    public interface IPackage
    {
        Task<(int, List<PackageVM>?)> GetPackageListByCountry(PackageRequestVM packReqVM);
        (bool,string) PurchasePackage(PurchasePackageRequestVM reqPurPackReq);
        Task<List<PurchasedPackageVM>?> GetPurchasedPackageList(int userid);
    }
}
