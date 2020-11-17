using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using MedicineShopBackEnd.Model.Entities;
namespace MedicineShopBackEnd.Services
{
   public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        //IEnumerable<User> GetAll();
        User GetById(int id);
        Task<List<UserRoleDTO>> GetUserRole();
        Task<bool> IsUserHasRights();
        Task<ApiResponseResult> SaveGuestInfo(GuestInfoDTO Guest, AddressDTO address);
        Task<bool> UpdateUserAddress(int userId, AddressDTO address);
        Task<ApiResponseResult> RegisterUser(User user);
        Task<ApiResponseResult> GetAllUsers(int? RoleId = 0, int? pageNumber = 1, int? pageSize = 10);
    }
}
