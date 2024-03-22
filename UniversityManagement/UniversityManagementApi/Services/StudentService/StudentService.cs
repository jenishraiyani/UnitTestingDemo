using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UniversityManagementApi.Data;
using UniversityManagementApi.Models;

namespace UniversityManagementApi.Services.StudentService
{
    public class StudentService<T> : IStudentService<T> where T : Students
    {
        private readonly DataContext _context;
        private DbSet<T> _student = null;

        public StudentService(DataContext context)
        {
            _context = context;
            _student = _context.Set<T>();

        }
        public async Task<List<T>> AddStudent(T student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return await _student.ToListAsync();
        }
        public async Task<List<T>> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student is null)
                return null;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return await _student.ToListAsync();
        }
        public async Task<List<T>> GetAllStudent()
        {
            var students = await _student.ToListAsync();
            return students;
        }
        public async Task<T> GetSingleUser(int id)
        {
            var student = await _student.FindAsync(id);
            if (student is null)
                return null;

            return student;
        }
        public async Task<T> UpdateStudent(int id, T studentDetails)
        {
            var student = await _student.FindAsync(id);
            if (student is null)
                return null;


            student.FirstName = studentDetails.FirstName;
            student.LastName = studentDetails.LastName;
            student.PhoneNumber = studentDetails.PhoneNumber;
            student.Branch = studentDetails.Branch;
            student.Email = studentDetails.Email;
            student.Address = studentDetails.Address;

            await _context.SaveChangesAsync();

            return student;
        }


    }
}
