using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurePrivacy.Model;
using SecurePrivacy.Services;

namespace SecurePrivacyBackEndTask.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;
        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }

        [Route("GetStudents")]
        [HttpGet]
        public async Task<ApiResponseResult> GetStudents()
        {
            ApiResponseResult apiResponse = new ApiResponseResult();
            try
            {
                apiResponse.data = await studentService.GetAllStudents();
                apiResponse.status = true;
                apiResponse.statusCode = 200;
                if (apiResponse.data != null)
                {
                    apiResponse.message = "Students Get Successfully";
                }
                else
                {
                    apiResponse.message = "No Records found";
                }
            }
            catch (Exception exc)
            {
                apiResponse.status = false;
                apiResponse.statusCode = 500;
                apiResponse.message = exc.Message;
            }
            return apiResponse;
        }

        [Route("AddStudent")]
        [HttpPost]
        public async Task<ApiResponseResult> AddStudent(StudentModel studentModel)
        {
            ApiResponseResult apiResponse = new ApiResponseResult();
            try
            {
                apiResponse.data = await studentService.SaveStudent(studentModel);
                apiResponse.status = true;
                apiResponse.statusCode = 200;
                apiResponse.message = "Student Save Successfully";

            }
            catch (Exception exc)
            {
                apiResponse.status = false;
                apiResponse.statusCode = 500;
                apiResponse.message = exc.Message;
            }
            return apiResponse;
        }

        [Route("UpdateStudent")]
        [HttpPost]
        public async Task<ApiResponseResult> UpdateStudent(StudentModel studentModel)
        {
            ApiResponseResult apiResponse = new ApiResponseResult();
            try
            {
                apiResponse.data = await studentService.UpdateStudent(studentModel);
                apiResponse.status = true;
                apiResponse.statusCode = 200;
                apiResponse.message = "Student Updated Successfully";

            }
            catch (Exception exc)
            {
                apiResponse.status = false;
                apiResponse.statusCode = 500;
                apiResponse.message = exc.Message;
            }
            return apiResponse;
        }


        [Route("DeleteStudent")]
        [HttpPost]
        public async Task<ApiResponseResult> DeleteStudent(int Id)
        {
            ApiResponseResult apiResponse = new ApiResponseResult();
            try
            {
                apiResponse.data = await studentService.DeleteStudent(Id);
                apiResponse.status = true;
                apiResponse.statusCode = 200;
                apiResponse.message = "Student Deleted Successfully";

            }
            catch (Exception exc)
            {
                apiResponse.status = false;
                apiResponse.statusCode = 500;
                apiResponse.message = exc.Message;
            }
            return apiResponse;
        }


        [Route("GetStudentById")]
        [HttpGet]
        public async Task<ApiResponseResult> GetStudentById(int Id)
        {
            ApiResponseResult apiResponse = new ApiResponseResult();
            try
            {
                apiResponse.data = await studentService.GetStudentById(Id);
                apiResponse.status = true;
                apiResponse.statusCode = 200;
                if(apiResponse.data != null)
                {
                    apiResponse.message = "Student Get Successfully";
                }
                else
                {
                    apiResponse.message = "No Records found";
                }
            }
            catch (Exception exc)
            {
                apiResponse.status = false;
                apiResponse.statusCode = 500;
                apiResponse.message = exc.Message;
            }
            return apiResponse;
        }
    }
}