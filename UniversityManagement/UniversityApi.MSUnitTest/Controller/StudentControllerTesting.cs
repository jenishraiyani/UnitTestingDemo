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

namespace UniversityApi.MSUnitTest.Controller
{
    [TestClass]
    public class StudentControllerTesting
    {
        private StudentController _controller;
        private Mock<IStudentService<Students>> _mockStudentService;
        private Mock<ILogger<StudentController>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            _mockStudentService = new Mock<IStudentService<Students>>();
            _mockLogger = new Mock<ILogger<StudentController>>();
            _controller = new StudentController(_mockStudentService.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task StudentController_GetStudents_ReturnOk()
        {
            // Arrange
            var students = new List<Students> { new Students { StudentId = 1, FirstName = "Jenish", LastName = "Raiyani" } };
            _mockStudentService.Setup(service => service.GetAllStudent()).ReturnsAsync(students);

            // Act
             
            var result = await _controller.GetAllStudents();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(students, okResult.Value);
        }

        [TestMethod]
        public async Task StudentController_GetStudentById_ReturnOk()
        {
            // Arrange
            int studentId = 1;
            var student = new Students { StudentId = 1, FirstName = "Jenish", LastName = "Raiyani" };
            _mockStudentService.Setup(service => service.GetSingleUser(studentId)).ReturnsAsync(student);

            // Act
            var result = await _controller.GetSingleStudent(studentId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(student, okResult.Value);
        }

        [TestMethod]
        public async Task StudentController_GetStudentById_NotFound()
        {
            // Arrange
            int studentId = 1;
            _mockStudentService.Setup(service => service.GetSingleUser(studentId)).ReturnsAsync((Students)null);

            // Act
            var result = await _controller.GetSingleStudent(studentId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("Student not found.", notFoundResult.Value);
        }

        [TestMethod]
        public async Task StudentController_AddStudent_ReturnOk()
        {
            // Arrange
            var student = new Students { StudentId = 1, FirstName = "Jenish", LastName = "Raiyani" };
            var addedStudents = new List<Students> { student };
            _mockStudentService.Setup(service => service.AddStudent(student)).ReturnsAsync(addedStudents);

            // Act
            var result = await _controller.AddStudent(student);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(addedStudents, okResult.Value);
        }

        [TestMethod]
        public async Task StudentController_UpdateStudent_ReturnOk()
        {
            // Arrange
            int studentId = 1;
            var updatedStudent = new Students { StudentId = 1, FirstName = "Jenish", LastName = "Raiyani" };
            _mockStudentService.Setup(service => service.UpdateStudent(studentId, updatedStudent)).ReturnsAsync(updatedStudent);

            // Act
            var result = await _controller.UpdateStudent(studentId, updatedStudent);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(updatedStudent, okResult.Value);
        }

        [TestMethod]
        public async Task StudentController_UpdateStudent_NotFound()
        {
            // Arrange
            int studentId = 1;
            var updatedStudent = new Students {StudentId = 1, FirstName = "Jenish", LastName = "Raiyani" };
            _mockStudentService.Setup(service => service.UpdateStudent(studentId, updatedStudent)).ReturnsAsync((Students)null);

            // Act
            var result = await _controller.UpdateStudent(studentId, updatedStudent);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("Student not found.", notFoundResult.Value);
        }

        [TestMethod]
        public async Task StudentController_DeleteStudent_OkResult()
        {
            // Arrange
            int studentId = 1;
            var remainingStudents = new List<Students> { new Students { StudentId = 1, FirstName = "Jenish", LastName = "Raiyani" }};
            _mockStudentService.Setup(service => service.DeleteStudent(studentId)).ReturnsAsync(remainingStudents);

            // Act
            var result = await _controller.DeleteStudent(studentId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(remainingStudents, okResult.Value);
        }

        [TestMethod]
        public async Task StudentController_DeleteStudent_NotFound()
        {
            // Arrange
            int studentId = 1;
            _mockStudentService.Setup(service => service.DeleteStudent(studentId)).ReturnsAsync((List<Students>)null);

            // Act
            var result = await _controller.DeleteStudent(studentId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("Student not found.", notFoundResult.Value);
        }

    }
}
