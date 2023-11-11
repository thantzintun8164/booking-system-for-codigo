using BookingClassManagementApi.Models;
using BookingClassManagementApi.ViewModels;
using System.Data;

namespace BookingClassManagementApi.Interfaces
{
    public interface IUser 
    {
        (UserVM?, string) SaveUser(UserVM userVM);
        (UserVM?, string?) Authenticate(AuthenticateRequestVM authReqVM);
        UserVM? GetUserProfileInfo(int id);
        (bool,string) ChangePassword(ChangePasswordRequestVM passChangeVM);
        (bool, string) ResetPassword(string email);
    }
}
