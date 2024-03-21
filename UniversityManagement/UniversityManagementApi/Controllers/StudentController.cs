using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementApi.Attributes;
using UniversityManagementApi.Models;
using UniversityManagementApi.Services.StudentService;

namespace UniversityManagementApi.Controllers
{

    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
 
    public class StudentController : ControllerBase
    {
        private readonly IStudentService<Students> _studentService;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentService<Students> studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger= logger;
            _logger.LogInformation("Nlog is integrated to Student Controller");
        }

       
        [HttpGet]
        [MapToApiVersion("1")]
        [RateLimitAttribute(timeWindowInSeconds = 60, maxRequests = 2)]
        public async Task<ActionResult<List<Students>>> GetAllStudents()
        {
            _logger.LogInformation("Requesting all student details");
            var students = await _studentService.GetAllStudent();
            return Ok(students);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1")]
        public async Task<ActionResult<Students>> GetSingleStudent(int id)
        {
            var result = await _studentService.GetSingleUser(id);
            if (result is null)
                return NotFound("Student not found.");

            return Ok(result);
        }

        [HttpGet("{id}")]

        [MapToApiVersion("1")]
        public async Task<ActionResult<Students>> GetStudentById(int id)
        {
            var result = await _studentService.GetSingleUser(id);
            if (result is null)
                return NotFound("Student not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Students>>> AddStudent(Students student)
        {
            var result = await _studentService.AddStudent(student);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Students>> UpdateStudent(int id, Students student)
        {
            var result = await _studentService.UpdateStudent(id, student);
            if (result is null)
                return NotFound("Student not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Students>>> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudent(id);
            if (result is null)
                return NotFound("Student not found.");

            return Ok(result);
        }
    }

}

