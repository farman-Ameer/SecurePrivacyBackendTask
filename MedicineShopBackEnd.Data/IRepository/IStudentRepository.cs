using MedicineShopBackEnd.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineShopBackEnd.Data.IRepository
{
   public interface IStudentRepository : IUnitOfWork<StudentModel>, IDisposable
    {
        
    }
}
