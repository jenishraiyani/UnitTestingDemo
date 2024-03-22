using Microsoft.EntityFrameworkCore;
using UniversityManagementApi.Models;

namespace UniversityManagementApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Students> Students { get; set; }
     

    }
}
