using SecurePrivacy.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurePrivacy.Services
{
   public interface IStudentService
    {
        Task<List<StudentModel>> GetAllStudents();
        Task<StudentModel> GetStudentById(int Id);
        Task<bool> SaveStudent(StudentModel studentModel);
        Task<bool> UpdateStudent(StudentModel studentModel);
        Task<bool> DeleteStudent(int Id);
    }
}
