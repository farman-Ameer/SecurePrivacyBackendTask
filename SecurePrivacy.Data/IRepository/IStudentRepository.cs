using SecurePrivacy.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurePrivacy.Data.IRepository
{
   public interface IStudentRepository : IUnitOfWork<StudentModel>, IDisposable
    {
        
    }
}
