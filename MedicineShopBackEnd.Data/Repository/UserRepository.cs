using MedicineShopBackEnd.Data.Helpers;
using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using MedicineShopBackEnd.Model.Entities;
using MedicineShopBackEnd.Model.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineShopBackEnd.Data.Repository
{
   public class UserRepository : UnitOfWork<User>, IUserRepository
    {
        private readonly MedicineShopDbContext _context;
        private IConfiguration _configuration;
        string jwtSecret = string.Empty;
        private IHttpContextAccessor _httpContextAccessor;
        //private readonly IUrlHelper _urlHelper;
        public UserRepository(MedicineShopDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(context, configuration, httpContextAccessor)
        {
            this._context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            jwtSecret = _configuration?.GetSection("ApplicationSettings")?["Secret"]?.ToString() ?? string.Empty;
            //_urlHelper = urlHelper;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<User> ValidateUser(string _email, string _pswd)
        {
            try
            {
              return await _context.Users.SingleOrDefaultAsync(x => x.userEmail == _email && x.userPassword == _pswd);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public User GetById(int id)
        {
            try
            {
                return _context.Users.FirstOrDefault(x => x.userId == id);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        //** 1 user can have multiple roles
        public async Task<List<UserRoleDTO>> GetUserRoleByUserId(int userId)
        {
            try
            {
                var data = await (from a in _context.Roles
                                  join b in _context.UserRoles
                                  on a.roleId equals b.roleId
                                  where b.userId == userId
                                  select new UserRoleDTO
                                  {
                                      userRole = a.roleId
                                  }).ToListAsync();
                return data;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public async Task<ApiResponseResult> SaveGuestInfo(GuestInfoDTO Guest, AddressDTO address)
        {
            User user = await getuser(Guest.email);
            ApiResponseResult apiresult = new ApiResponseResult();
            if (user == null)
            {
                user = new User();
                try
                {
                    user.userPassword = "***";
                    user.firstName = Guest.firstName;
                    user.lastName = Guest.lastName;
                    user.userEmail = Guest.email;
                    user.contact = Guest.phone;
                    user.roleID = (Int32)Constant.User.GUEST_USER;
                    user.joiningDate = DateTime.Now;
                    user.street = address.street;
                    user.state = address.state;
                    user.userZipCode = address.zipCode;
                    user.apartment = address.appartment;
                    user.city = address.city;
                    user.userAddress = address.street + "," + address.appartment + "," + address.city + "," + address.state + "," + address.zipCode;
                    user.isActive = true;
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    await SaveUserRole((Int32)Constant.User.GUEST_USER, user.userId);
                    apiresult.ErrorCode = (int)Constant.Error.NO_ERROR;
                    apiresult.data = user;
                    apiresult.message = "Save Successfully";
                }
                //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                //{
                //    apiresult.ErrorNo = (int)Constant.Error.ENTITY_EXCEPTION;
                //    apiresult.Message = dbEx.Message;
                //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                //    {
                //        foreach (var validationError in validationErrors.ValidationErrors)
                //        {
                //            string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                //            //raise a new exception inserting the current one as the InnerException

                //        }
                //    }

                //}
                catch (Exception exce)
                {
                    apiresult.ErrorCode = (int)Constant.Error.EXCEPTION;
                    apiresult.message = exce.Message;
                }
            }
            else
            {
                apiresult.ErrorCode = (int)Constant.Error.GUEST_ALREADY_EXIST;
                apiresult.message = "Guest Already Exist";
            }
            return apiresult;
        }
        public async Task<User> getuser(string _email)
        {
            try
            {
                return await _context.Users.Where(x => x.userEmail == _email).FirstOrDefaultAsync();
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
                var _user = await _context.Users.Where(x => x.userId == userId).FirstOrDefaultAsync();
                if(_user != null)
                {
                    _user.street = address.street;
                    _user.state = address.state;
                    _user.userZipCode = address.zipCode;
                    _user.apartment = address.appartment;
                    _user.city = address.city;
                    _user.userAddress = address.street + "," + address.appartment + "," + address.city + "," + address.state + "," + address.zipCode;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return false;
        }
        private async Task<int> SaveUserRole(int roleID, int userID)
        {
            UserRoleModel userRole = new UserRoleModel();
            try
            {
                userRole.roleId = roleID;
                userRole.userId = userID;
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
                return userRole.Id;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public async Task<ApiResponseResult> SaveUser(User _user)
        {
            User user = await getuser(_user.userEmail);
            ApiResponseResult apiresult = new ApiResponseResult();
            if (user == null)
            {
                user = new User();
                try
                {
                    user.userPassword = _user.userPassword;
                    user.firstName = _user.firstName;
                    user.lastName = _user.lastName;
                    user.userEmail = _user.userEmail;
                    user.contact = _user.userPhone;
                    user.roleID = _user.roleID;
                    user.joiningDate = DateTime.Now;
                    user.street = _user.street;
                    user.state = _user.state;
                    user.userZipCode = _user.userZipCode;
                    user.apartment = _user.apartment;
                    user.city = _user.city;
                    user.userAddress = _user.street + "," + _user.apartment + "," + _user.city + "," + _user.state + "," + _user.userZipCode;
                    user.isActive = true;
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    await SaveUserRole(_user.roleID, user.userId);
                    apiresult.ErrorCode = (int)Constant.Error.NO_ERROR;
                    apiresult.data = user;
                    apiresult.message = "User Registered Successfully";
                }
                catch (Exception exce)
                {
                    apiresult.ErrorCode = (int)Constant.Error.EXCEPTION;
                    apiresult.message = exce.Message;
                }
            }
            else
            {
                apiresult.ErrorCode = (int)Constant.Error.USER_ALREADY_EXIST;
                apiresult.message = "Email Already Exist";
            }
            return apiresult;
        }
        public PagedList<UserDTO> GetAllUsers(int? RoleId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            PagingParamsDTO pagingParams = new PagingParamsDTO();
            pagingParams.PageNumber = pageNumber;
            pagingParams.PageSize = pageSize;
            IQueryable<UserDTO> query;
            try
            {
                if(RoleId == 0)
                {
                    query = from a in _context.Users
                            join b in _context.UserRoles
                            on a.userId equals b.userId
                            join c in _context.Roles
                            on b.roleId equals c.roleId
                            where a.isActive == true
                            select new UserDTO
                            {
                                firstName = a.firstName,
                                lastName = a.lastName,
                                userEmail = a.userEmail,
                                userPhone = a.contact,
                                userAddress = a.userAddress,
                                userZipCode = a.userZipCode,
                                userId = a.userId,
                                userProfileImage = a.userProfileImage,
                                Role = new UserRoleDTO { userRole = c.roleId, userRoleName = c.roleName }
                            };
                }
                else
                {
                    query = from a in _context.Users
                            join b in _context.UserRoles
                            on a.userId equals b.userId
                            join c in _context.Roles
                            on b.roleId equals c.roleId
                            where c.roleId == RoleId
                            && a.isActive == true
                            select new UserDTO
                            {
                                firstName = a.firstName,
                                lastName = a.lastName,
                                userEmail = a.userEmail,
                                userPhone = a.contact,
                                userAddress = a.userAddress,
                                userZipCode = a.userZipCode,
                                userId = a.userId,
                                userProfileImage = a.userProfileImage,
                                Role = new UserRoleDTO { userRole = c.roleId, userRoleName = c.roleName }
                            };
                }
               


                  //var  query = _context.Users.Select( x => new UserDTO
                  //  {
                  //      firstName = x.firstName,
                  //      lastName = x.lastName,
                  //      userAddress = x.userAddress,
                  //      userEmail = x.userEmail,
                  //      userId = x.userId,
                  //      userPhone = x.userPhone,
                  //      userProfileImage = x.userProfileImage,
                  //      userZipCode = x.userZipCode,
                  //      //userRoles = GetRoles(x.userId, RoleId)
                  //});
                return new PagedList<UserDTO>(
                       query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        //** Not in use
        public List<UserRoleDTO> GetRoles(int userId, int ? RoleId = 0)
        {
            List<UserRoleDTO> userRoles = new List<UserRoleDTO>();
            try
            {
                if(RoleId == 0)
                {
                    userRoles = (from a in _context.Roles
                                join b in _context.UserRoles
                                on a.roleId equals b.roleId
                                where b.userId == userId
                                select new UserRoleDTO
                                {
                                   userRole = a.roleId
                                }).ToList();
                }
                else
                {
                    {
                        userRoles = (from a in _context.Roles
                                     join b in _context.UserRoles
                                     on a.roleId equals b.roleId
                                     where b.userId == userId
                                     && a.roleId == RoleId
                                     select new UserRoleDTO
                                     {
                                         userRole = a.roleId
                                     }).ToList();
                    }
                }
                return userRoles;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }



    }
}
