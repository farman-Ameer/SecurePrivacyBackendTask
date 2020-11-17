using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using MedicineShopBackEnd.Model.Entities;
using MedicineShopBackEnd.Model.Pagination;

namespace MedicineShopBackEnd.Data.IRepository
{
   public interface IUserRepository : IUnitOfWork<User>, IDisposable
    {
        Task<User> ValidateUser(string _email, string _pswd);
        User GetById(int id);
        Task<List<UserRoleDTO>> GetUserRoleByUserId(int userId);
        Task<ApiResponseResult> SaveGuestInfo(GuestInfoDTO Guest, AddressDTO address);
        Task<bool> UpdateUserAddress(int userId, AddressDTO address);
        Task<ApiResponseResult> SaveUser(User user);
        PagedList<UserDTO> GetAllUsers(int? RoleId = 0, int? pageNumber = 1, int? pageSize = 10);
    }
}
