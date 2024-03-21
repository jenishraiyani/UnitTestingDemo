using AutoMapper;
using Azure;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UniversityApi.xUnitTest.Fixtures;
using UniversityManagementApi.Controllers;
using UniversityManagementApi.Models;
using UniversityManagementApi.Services.StudentService;

namespace UniversityApi.xUnitTest.Controller
{

    public class StudentControllerTests
    {
        private readonly Mock<IStudentService<Students>> _studentService;
        private readonly Mock<ILogger<StudentController>> _logger;
        public StudentControllerTests()
        {
            _studentService = new Mock<IStudentService<Students>>();
            _logger = new Mock<ILogger<StudentController>>();

        }

        [Fact]
        public async Task StudentController_GetStudents_ReturnOk()
        {
            //Arrage
            var controller = new StudentController(_studentService.Object, _logger.Object);
            _studentService.Setup(serivce => serivce.GetAllStudent());

            //Act
            var controllerResult = await controller.GetAllStudents();

            //Assert
            var actionResult = Assert.IsType<ActionResult<List<Students>>>(controllerResult);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            controllerResult.Should().NotBeNull();
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task StudentController_GetStudentById_ReturnOk(int studentId)
        {
            //Arrage
            var student = StudentFixtures.GetStudents().Find(x => x.StudentId == studentId);
            var controller = new StudentController(_studentService.Object, _logger.Object);
            _studentService.Setup(service => service.GetSingleUser(studentId))
                            .ReturnsAsync(student);

            //Act
            var controllerResult = await controller.GetStudentById(studentId);

            //Assert    
            var actionResult = Assert.IsType<ActionResult<Students>>(controllerResult);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<Students>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Theory]
        [InlineData(132)]
        public async Task StudentController_GetStudentById_ReturnNotFoundResult(int studentId)
        {
            //Arrage
            var student = StudentFixtures.GetStudents().Find(x => x.StudentId == studentId);
            var controller = new StudentController(_studentService.Object, _logger.Object);
            _studentService.Setup(service => service.GetSingleUser(studentId))
                            .ReturnsAsync(student);

            //Act
            var controllerResult = await controller.GetStudentById(studentId);

            //Assert    
            var actionResult = Assert.IsType<ActionResult<Students>>(controllerResult);
            var okResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status404NotFound, okResult.StatusCode);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task StudentController_UpdateStudent_ReturnOk(int studentId)
        {
            //Arrage
            var student = StudentFixtures.GetStudents().Find(x => x.StudentId == studentId);
            var controller = new StudentController(_studentService.Object, _logger.Object);
            _studentService.Setup(service => service.UpdateStudent(studentId,student))
                         .ReturnsAsync(student);

            //Act
            var controllerResult = await controller.UpdateStudent(studentId,student);

            //Assert    
            var actionResult = Assert.IsType<ActionResult<Students>>(controllerResult);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<Students>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(student, okResult.Value);
        }


    }


        /*
        [Fact]
        public async Task StudentController_GetStudent_ReturnOk()
        {
            //Arrange
            var mockStudentService = new Mock<IStudentService<Students>>();
            mockStudentService.Setup(service => service.GetAllStudent()).ReturnsAsync(new List<Students>());

            var studentController = new StudentController(mockStudentService.Object, _logger);

            //Act
            var result = (OkObjectResult) await studentController.GetAllStudents();

            //Assert
            studentController.Verify(service => service.Get);

        } */
       

    
}
