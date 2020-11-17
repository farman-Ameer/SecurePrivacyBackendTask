using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MedicineShopBackEnd.Data.Repository
{
   public class StudentRepository : UnitOfWork<StudentModel>, IStudentRepository
    {
        private readonly MedicineShopDbContext _context;
        private IConfiguration _configuration;
        string jwtSecret = string.Empty;
        private IHttpContextAccessor _httpContextAccessor;
        //private readonly IUrlHelper _urlHelper;
        public StudentRepository(MedicineShopDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(context, configuration, httpContextAccessor)
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
       
       
    }
}
