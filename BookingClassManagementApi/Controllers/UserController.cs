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
    //[Authorize]
    public class UserController : ControllerBase
    {
        IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        [Route("UserLogin")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult UserLogin([FromBody] AuthenticateRequestVM authReqVM)
        {
            if (authReqVM.Email == null || !authReqVM.Email.Contains('@') || authReqVM.Password == String.Empty) //for empty string
            {
                return BadRequest(new
                {
                    Status = false,
                    Message = "email & password are not valid!"
                });
            }
            var res = _user.Authenticate(authReqVM);
            if(res.Item1 == null)
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
                Message = res.Item2,
                Data = JsonSerializer.Serialize(authReqVM)
            });
        }
        [Route("UserRegister")]
        [HttpPost]
        public IActionResult UserRegister([FromBody] UserVM userVM)
        {
            if (userVM == null) 
            {
                return BadRequest(new
                {
                    Status = false,
                    Message = "invalid request!"
                });
            }
            var res = _user.SaveUser(userVM);
            if (res.Item1 == null)
            {
                return Ok(new
                {
                    Status = false,
                    Message = res.Item2
                });
            }
            return Ok(new
            {
                Status = true,
                Message = res.Item2,
                Data = JsonSerializer.Serialize(res.Item1)
            });
        }
        [Route("GetUserProfile/{id}")]
        [HttpGet]
        public IActionResult GetUserProfile(int id)
        {
            if (id == 0 || id < 0) return BadRequest(new { Status = false, Message = "invalid request" });
            var res = _user.GetUserProfileInfo(id);
            if (res == null)
            {
                return NotFound(new
                {
                    Status = false,
                    Message = "user information not found!"
                });
            }
            return Ok(new
            {
                Status = false,
                Message = "success",
                Data= JsonSerializer.Serialize(res)
            });
        }
        [Route("ChangeUserPassword")]
        [HttpPost]
        public IActionResult ChangeUserPassword(ChangePasswordRequestVM reqPass)
        {
            if (reqPass == null)
            {
                return BadRequest(new
                {
                    Status = false,
                    Message = "invalid request!"
                });
            }
            var res = _user.ChangePassword(reqPass);
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
        [Route("ResetUserPassword/{email}")]
        [HttpGet]
        public IActionResult ChangeUserPassword(string email)
        {
            if (email == string.Empty)
            {
                return BadRequest(new
                {
                    Status = false,
                    Message = "invalid request!"
                });
            }
            var res = _user.ResetPassword(email);
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
