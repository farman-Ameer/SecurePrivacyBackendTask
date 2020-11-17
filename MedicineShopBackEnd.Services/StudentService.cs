using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineShopBackEnd.Services
{
   public class StudentService : IStudentService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        private IStudentRepository _studentRepository;
        public StudentService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment, IStudentRepository studentRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _studentRepository = studentRepository;
        }

       public async Task<List<StudentModel>> GetAllStudents()
        {
            try
            {
                return await _studentRepository.GetAll();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }


        public async Task<bool> SaveStudent(StudentModel studentModel)
        {
            try
            {
                if (studentModel.name == null || studentModel.name == "")
                    throw new Exception("Student Name is required");

                await _studentRepository.Insert(studentModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return true;
        }


        public async Task<bool> UpdateStudent(StudentModel studentModel)
        {
            try
            {
                if (studentModel.Id == 0)
                    throw new Exception("Student id is required");

                var studentData = await _studentRepository.Get(x => x.Id == studentModel.Id);
                if(studentData != null)
                {
                    studentData.name = studentModel.name;
                    studentData.fatherName = studentModel.fatherName;
                    studentData.grade = studentModel.grade;
                    await _studentRepository.Update(studentData);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return true;
        }


        public async Task<bool> DeleteStudent(int Id)
        {
            try
            {
                if (Id == 0)
                    throw new Exception("Student id is required");

                var data = await _studentRepository.Get(x => x.Id == Id);
                if(data != null)
                    await _studentRepository.Delete(data);


            }
            catch (Exception exc)
            {
                throw exc;
            }
            return true;
        }

      public async Task<StudentModel> GetStudentById(int Id)
        {
            try
            {
                if (Id == 0)
                    throw new Exception("Student id is required");

                return await _studentRepository.Get(x => x.Id == Id);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
