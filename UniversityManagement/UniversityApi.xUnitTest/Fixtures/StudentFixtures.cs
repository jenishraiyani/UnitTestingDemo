using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementApi.Models;

namespace UniversityApi.xUnitTest.Fixtures
{
    public static class StudentFixtures
    {
        public static List<Students> GetStudents() => new()
        {
                 new Students()
                {
                    StudentId= 1,
                    FirstName= "Jenish",
                    LastName= "Raiyani",
                    Email= "jenishraiyani75@gmail.com",
                    PhoneNumber= "7573864415",
                    Address= "Jnd",
                    Branch= "ICT",
                },
                new Students()
                {
                    StudentId= 2,
                    FirstName= "Khush",
                    LastName= "Makadiya",
                    Email= "makadiyakhush12@gmail.com",
                    PhoneNumber= "98526548",
                    Address= "Rajkot",
                    Branch= "IT",
                } ,
                new Students()
                {
                    StudentId= 3,
                    FirstName= "Sumit",
                    LastName= "Patel",
                    Email= "sumit903@gmail.com",
                    PhoneNumber= "8855447895",
                    Address= "Amreli",
                    Branch= "CE",
                }

        };

        
    }
}
