using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementApi.Controllers;
using UniversityManagementApi.Models;
using UniversityManagementApi.Services.StudentService;

namespace UniversityApi.NUnitTest.Controller
{
    public class StudentControllerTest
    {
        private Mock<IStudentService<Students>> _studentService;
        private StudentController studentController;
        private Mock<ILogger<StudentController>> _logger;
        private List<Students> students;
        
      

        [SetUp]
        public void Setup()
        {
            _studentService = new Mock<IStudentService<Students>>();
            _logger = new Mock<ILogger<StudentController>>();
            studentController = new StudentController(_studentService.Object, _logger.Object);
            students = new List<Students>();
            students.Add(new Students() { StudentId=1,FirstName="Jenish",LastName="Raiyani"});
            students.Add(new Students() { StudentId=2,FirstName="Khush",LastName="Makadiya"});
            students.Add(new Students() { StudentId=3,FirstName="Akash",LastName="Rana"});
        
        }

        [Test]
        public async Task StudentController_GetStudents_ReturnOk()
        {
            //Arrange
            _studentService.Setup(serivce => serivce.GetAllStudent());

            // Act
            var controllerResult = await studentController.GetAllStudents();
        
            // Assert
            Assert.IsInstanceOf<ActionResult<List<Students>>>(controllerResult);
            var apiResponse = controllerResult.Result as OkObjectResult;
            Assert.NotNull(controllerResult);
            Assert.AreEqual(200, apiResponse.StatusCode);
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public async Task StudentController_GetStudentById_ReturnOk(int studentId)
        {
            //Arrange
            var student = students.Find(x => x.StudentId == studentId);
            _studentService.Setup(serivce => serivce.GetSingleUser(studentId)).ReturnsAsync(student);

            // Act
            var controllerResult = await studentController.GetStudentById(studentId);

            // Assert
            Assert.IsInstanceOf<ActionResult<Students>>(controllerResult);
            var apiResponse = controllerResult.Result as OkObjectResult;
            Assert.NotNull(controllerResult);
            Assert.AreEqual(200, apiResponse.StatusCode);

        }

        [Test]
        public async Task StudentController_AddStudent_ReturnsOk()
        {
            // Arrange
            var newStudent = new Students { StudentId = 1, FirstName = "Jenish", LastName="Raiyani"};  

            _studentService.Setup(service => service.AddStudent(newStudent)).ReturnsAsync(students);  

            // Act
            var controllerResult = await studentController.AddStudent(newStudent);   

            // Assert
            Assert.IsInstanceOf<ActionResult<List<Students>>>(controllerResult);
            var apiResponse = controllerResult.Result as OkObjectResult;
            Assert.NotNull(controllerResult);
            Assert.AreEqual(200, apiResponse.StatusCode);
         
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public async Task StudentController_UpdateStudentDetails_ReturnsOk(int studentId)
        {
            // Arrange
            var student = students.Find(x => x.StudentId == studentId);
            _studentService.Setup(service => service.UpdateStudent(studentId, student))
                       .ReturnsAsync(student);

            // Act
            var controllerResult = await studentController.UpdateStudent(studentId,student);

            // Assert
            Assert.IsInstanceOf<ActionResult<Students>>(controllerResult);
            var apiResponse = controllerResult.Result as OkObjectResult;
            Assert.NotNull(controllerResult);
            Assert.AreEqual(200, apiResponse.StatusCode);

        }

    }
}
