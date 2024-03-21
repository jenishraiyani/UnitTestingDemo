using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementApi.Data;
using UniversityManagementApi.Models;
using UniversityManagementApi.Services.StudentService;

namespace UniversityApi.MSUnitTest.Controller
{
    [TestClass]
    public class DatabaseConnectionTests
    {
        private DataContext _context;

        [TestInitialize]
        public void InitializeDatabaseConnection()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Data Source=SF-CPU-583\\SQLEXPRESS;Initial Catalog= DotNetAdvancedConcepts; Integrated Security = true;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;MultipleActiveResultSets=True").Options;
            _context = new DataContext(options);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void TestDatabaseConnection()
        {
            // Arrange
            var service = new StudentService<Students>(_context);

            // Act
            var result = service.GetAllStudent();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
