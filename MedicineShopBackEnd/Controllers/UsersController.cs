using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Services;
using static IdentityModel.OidcConstants;
using MedicineShopBackEnd.Model.Entities;
using MedicineShopBackEnd.Services.Helpers;

namespace MedicineShopBackEnd.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //public IActionResult Authenticate(AuthenticateRequest model)
        //{
        //    var response = _userService.Authenticate(model);

        //    if (response == null)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    return Ok(response);
        //}

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ApiResponseResult> Authenticate(AuthenticateRequest model)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                var response = await _userService.Authenticate(model);
                if (response == null)
                {
                    apiResponseResult.ErrorCode = (int)Constant.Error.INVALID_USER;
                    apiResponseResult.message = "Username or password is incorrect";
                }
                else
                {
                    apiResponseResult.data = response;
                    apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                    apiResponseResult.message = "Loged in";
                }
            }
            catch (Exception exc)
            {
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
                apiResponseResult.message = exc.Message;
            }
            return apiResponseResult;
        }


        [Route("RegisterUser")]
        [HttpPost]
        public async Task<ApiResponseResult> RegisterUser(User _user)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                return await _userService.RegisterUser(_user);
            }
            catch (Exception exc)
            {
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
                apiResponseResult.message = exc.Message;
            }
            return apiResponseResult;
        }

        //[Authorize]
        [Route("GetUsers")]
        [HttpGet]
        public async Task<ApiResponseResult> GetUsers(int? roleId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            return await _userService.GetAllUsers(roleId, pageNumber, pageSize);
        }

        //[Authorize]
        //[Route("GetUsers")]
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var users = _userService.GetAll();
        //    return Ok(users);
        //}
    }
}