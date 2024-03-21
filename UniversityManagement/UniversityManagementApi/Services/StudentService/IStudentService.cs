using UniversityManagementApi.Models;

namespace UniversityManagementApi.Services.StudentService
{
    public interface IStudentService <T> where T : Students
    {
        Task<List<T>> GetAllStudent();
        Task<T?> GetSingleUser(int id);
        Task<List<T>> AddStudent(T student);
        Task<T?> UpdateStudent(int id, T student);
        Task<List<T>> DeleteStudent(int id);
    }
}
