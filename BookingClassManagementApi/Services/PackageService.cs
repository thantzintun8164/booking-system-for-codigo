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
    public class PackageService : IPackage
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public PackageService(ApplicationDbContext dbContext, IMapper _mapper, IConfiguration config)
        {
            _dbContext = dbContext;
            this._mapper = _mapper;
            _config = config;
        }
        public async Task<(int,List<PackageVM>?)> GetPackageListByCountry(PackageRequestVM packReqVM)
        {
            var res = await _dbContext.Packages.Include(p => p.Class)
                      .Where(p => p.Class.CountryId == packReqVM.CountryId && p.ExpireDate <= DateTime.Now)
                      .AsNoTracking()
                      .ToListAsync();
            if(res.Count() == 0)
            {
                return (0,null!);
            }
            List<PackageVM> resPackageLst = new();
            foreach (var item in res)
            {
                PackageVM pvm = new PackageVM();
                pvm = _mapper.Map<PackageVM>(item);
                pvm.IsExpire = false;
                resPackageLst.Add(pvm);
            }
            int totalCount = resPackageLst.Count();
            resPackageLst = resPackageLst.Skip((packReqVM.PageNumber - 1) * packReqVM.PageSize)
                                       .Take(packReqVM.PageSize)
                                       .ToList();
            return (totalCount,resPackageLst);
        }

        public async Task<List<PurchasedPackageVM>?> GetPurchasedPackageList(int userid)
        {
            var res = await _dbContext.PurchasePackages
                      .Include(p=>p.Package)
                      .Where(p => p.UserId == userid)
                      .AsNoTracking()
                      .ToListAsync();
            if (res.Count() == 0)
            {
                return null;
            }
            List<PurchasedPackageVM> resPackageLst = new();
            foreach (var item in res)
            {
                PurchasedPackageVM pvm = new PurchasedPackageVM();
                pvm.PurchaseId = item.Id; 
                pvm.PackageVM = _mapper.Map<PackageVM>(item.Package);
                resPackageLst.Add(pvm);
            }
            return resPackageLst;
        }
        
        //The following are mock function for payment
        public (bool,string) PurchasePackage(PurchasePackageRequestVM reqPurPackReq)
        {
            var pack = _dbContext.Packages.Where(p => p.Id== reqPurPackReq.PackageId).FirstOrDefault();
            var user = _dbContext.Users.Where(u => u.Id == reqPurPackReq.UserId).FirstOrDefault();
            if (pack == null || user == null)
                return (false,"package not found!");
            var tmp_pur_pack = new PurchasePackage();
            tmp_pur_pack.PackageId = reqPurPackReq.PackageId;
            tmp_pur_pack.UserId = reqPurPackReq.UserId;
            _dbContext.PurchasePackages.Add(tmp_pur_pack);
            user!.OwnedCredit = pack.CreditCost;
            _dbContext.Users.Attach(user);
            _dbContext.SaveChanges();
            return (true, "success");
        }
        public bool AddPaymentCard(UserPaymentCard userPaymentCard)
        {
            return true;
        }
        public bool PaymentCharge(UserPaymentCard userPaymentCard)
        {
            return true;
        }

    }

}
