using BookingClassManagementApi.Interfaces;
using BookingClassManagementApi.Models;
using BookingClassManagementApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookingClassManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PackageController : ControllerBase
    {
        IPackage _package;
        public PackageController(IPackage package)
        {
            _package = package;
        }

        [Route("GetPackageByCountry")]
        [HttpGet]
        public async Task<IActionResult> GetPackageByCountry(PackageRequestVM packReqVM)
        {
            if (packReqVM == null || packReqVM.CountryId == 0) 
                return BadRequest(new { Status = false, Message = "invalid request" });
            var res = await _package.GetPackageListByCountry(packReqVM);
            if (res.Item1 > 0 && res.Item2 != null )
            {
                int pageCount = (res.Item1 + packReqVM.PageSize - 1) / packReqVM.PageSize;
                return Ok(new
                {
                    Status = true,
                    Message = "success",
                    TotalCount = res.Item1,
                    CurrentPage = packReqVM.PageNumber,
                    PageCount = packReqVM.PageSize,
                    TotalPage = pageCount,
                    Data = res.Item2
                });
            }
            return Ok(new
            {
                Status = false,
                Message = "no data found!",
            });
        }

        [Route("GetPurchasedPackageList")]
        [HttpGet]
        public async Task<IActionResult> GetPurchasedPackageList(int userid)
        {
            if (userid == 0)
                return BadRequest(new { Status = false, Message = "invalid request" });
            var res = await _package.GetPurchasedPackageList(userid);
            if (res != null)
            {
                
                return Ok(new
                {
                    Status = true,
                    Message = "success",
                    Data = res
                });
            }
            return Ok(new
            {
                Status = false,
                Message = "no data found!",
            });
        }
        [Route("PurchasePackage")]
        [HttpPost]
        public IActionResult PurchasePackage(PurchasePackageRequestVM reqPurPack)
        {
            if (reqPurPack == null)
            {
                return BadRequest(new
                {
                    Status = false,
                    Message = "invalid request!"
                });
            }
            var res = _package.PurchasePackage(reqPurPack);
            if (res.Item1 == false)
            {
                return NotFound(new
                {
                    Status = false,
                    Message = res.Item2
                });
            }
            return Ok(new
            {
                Status = true,
                Message = res.Item2
            });
        }
    }
}
