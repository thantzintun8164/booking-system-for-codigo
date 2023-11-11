using AutoMapper;
using BookingClassManagementApi.Commons;
using BookingClassManagementApi.Data;
using BookingClassManagementApi.Interfaces;
using BookingClassManagementApi.Models;
using BookingClassManagementApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using MailKit.Net.Smtp;
using System.Security.Claims;
using System.Text;

namespace BookingClassManagementApi.Services
{
    public class UserService : IUser
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserService(ApplicationDbContext dbContext, IMapper _mapper, IConfiguration config)
        {
            _dbContext = dbContext;
            this._mapper = _mapper;
            _config = config;
        }
        public (UserVM?, string?) Authenticate(AuthenticateRequestVM authReqVM)
        {
            string password = CommonMethod.EncryptPassword(authReqVM.Password);
            User? user = _dbContext.Users.Where(x => x.Email == authReqVM.Email
                                                    && x.Password == password
                                                    && x.Activated == true
                                                    && x.EmailConfirmed == true
                                                    ).FirstOrDefault();

            // return null if user not found
            if (user is null) return (null, "user not found!");

            // authentication successful so generate jwt token
            string token = GenerateJwtToken(user);
            UserVM usrRes = new UserVM();
            usrRes = _mapper.Map<UserVM>(user!);
            usrRes.Token = token;
            return (usrRes,"success");
        }
        public UserVM? GetUserProfileInfo(int id)
        {
            User? user = _dbContext.Users.Where(x => x.Id == id
                                                    && x.Activated == true
                                                    && x.EmailConfirmed == true
                                                    ).FirstOrDefault();
            if(user is null) return null;
            UserVM res =  _mapper.Map<UserVM>(user!);
            res.Password = CommonMethod.DecryptPassword(res.Password);
            return res;
        }
        public (UserVM?, string) SaveUser(UserVM userVM)
        {
            try
            {
                if (userVM.Id == 0)
                {
                    var user = _dbContext.Users.Where(u => u.Email == userVM.Email && u.Activated == false).FirstOrDefault();
                    if (user != null)
                    {
                        return (null, "user already exist!");
                    }
                    else
                    {
                        userVM.Password = CommonMethod.EncryptPassword(userVM.Password);
                        userVM.CreatedAt = DateTime.Now;
                        var data = _mapper.Map<User>(userVM);
                        //denoted as this is verify email confirm process
                        data.Activated = true;
                        data.EmailConfirmed = true;
                        _dbContext.Users.Add(data);
                        SendVerifyEmail(data.Email);
                    }
                }
                else
                {
                    userVM.Password = CommonMethod.EncryptPassword(userVM.Password);
                    userVM.UpdatedAt = DateTime.Now;
                    var data = _mapper.Map<User>(userVM);
                    _dbContext.Users.Attach(data);
                    _dbContext.Entry(data).State = EntityState.Modified;
                }
                _dbContext.SaveChanges();

                //for return decrypt password again
                userVM.Password = CommonMethod.DecryptPassword(userVM.Password);
                return (userVM, "success");
            }
            catch (Exception ex)
            {
                return (userVM, ex.ToString());
            }
        }
        public (bool, string) ChangePassword(ChangePasswordRequestVM passChangeVM)
        {
            if (passChangeVM.Email == null || passChangeVM.OldPassword == null || passChangeVM.NewPassword == null)
                return (false, "fail");
            string password = CommonMethod.EncryptPassword(passChangeVM.OldPassword);
            User? user = _dbContext.Users.Where(x => x.Email == passChangeVM.Email
                                                    && x.Password == password
                                                    && x.Activated == true
                                                    && x.EmailConfirmed == true
                                                    ).FirstOrDefault();
            if(user == null) return (false, "user not found!");
            user.Password = CommonMethod.EncryptPassword(passChangeVM.NewPassword);
            _dbContext.Users.Attach(user);
            _dbContext.SaveChanges();
            return (true, "success");
        }
        public (bool, string) ResetPassword(string email)
        {
            
            User? user = _dbContext.Users.Where(x => x.Email == email
                                                    && x.Activated == true
                                                    && x.EmailConfirmed == true
                                                    ).FirstOrDefault();
            if (user == null) return (false, "user not found");
            var res = SendResetEmail(email);
            if (res) return (true, "reset link was sent to your email!");
            else return (false, "fail!");
        }
        #region generate jwt token
        private string GenerateJwtToken(User? user)
        {
            List<Claim> claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name,user!.Name),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_config.GetSection("JWTSetting").GetSection("securitykey").Value!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }
                ),
                Expires = DateTime.Now.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenHandler.WriteToken(token);
            return finaltoken;
        }
        #endregion
        #region SendEmailConfirmation
        public bool SendVerifyEmail(string email)
        {
            return true;
        }
        public bool SendResetEmail(string email)
        {
            return true;
        }
        #endregion

    }

}
