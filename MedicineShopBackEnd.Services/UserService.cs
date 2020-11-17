using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MedicineShopBackEnd.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using MedicineShopBackEnd.Data.IRepository;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using MedicineShopBackEnd.Services.Helpers;
using MedicineShopBackEnd.Model.DTO;

namespace MedicineShopBackEnd.Services
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<User> _users = new List<User>
        //{
        //    new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        //};

        private readonly AppSettings _appSettings;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        private IUserRepository _userRepository;
        public UserService(IOptions<AppSettings> appSettings, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            //var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            var user = await _userRepository.ValidateUser(model.username, model.Password);


            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        //public IEnumerable<User> GetAll()
        //{
        //    return _users;
        //}

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> IsUserHasRights()
        {
            try
            {
                var userRole = await GetUserRole();
                if (userRole.Any(x=>x.userRole == (int)Constant.User.ADMIN_USER))
                {
                    return true;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return false;
        }

        public async Task<List<UserRoleDTO>> GetUserRole()
        {
            try
            {
                var user = (User)_httpContextAccessor.HttpContext.Items["User"];
                return await _userRepository.GetUserRoleByUserId(user.userId);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task<bool> UpdateUserAddress(int userId, AddressDTO address)
        {
            try
            {
                await _userRepository.UpdateUserAddress(userId, address);
                return true;
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        public async Task<ApiResponseResult> SaveGuestInfo(GuestInfoDTO Guest, AddressDTO address)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult(); 
            try
            {
                apiResponseResult = await _userRepository.SaveGuestInfo(Guest, address);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return apiResponseResult;
        }

        public async Task<ApiResponseResult> RegisterUser(User user)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            try
            {
                apiResponseResult = await _userRepository.SaveUser(user);
            }
            catch (Exception exc)
            {
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
                apiResponseResult.message = exc.Message;
            }
            return apiResponseResult;
        }
        public async Task<ApiResponseResult> GetAllUsers(int? RoleId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            ApiResponseResult apiResponseResult = new ApiResponseResult();
            paginationDTO<UserDTO> paginatedorders = new paginationDTO<UserDTO>();
            try
            {
                var model = _userRepository.GetAllUsers(RoleId, pageNumber, pageSize);
                paginatedorders.Paging = model.GetHeader();
                paginatedorders.Items = await model.List;
                apiResponseResult.data = paginatedorders;
                apiResponseResult.message = "Users Get Successfully";
                apiResponseResult.ErrorCode = (int)Constant.Error.NO_ERROR;
                apiResponseResult.status = true;
            }
            catch (Exception exc)
            {
                apiResponseResult.message = exc.Message;
                apiResponseResult.ErrorCode = (int)Constant.Error.EXCEPTION;
                apiResponseResult.status = false;
            }
            return apiResponseResult;
        }


    }
}
