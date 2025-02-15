using StudentsAPI.DataModels;

namespace StudentsAPI.StudentServices.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        Task<Student?> GetStudentBySRN(int srn);
        Task AddStudent(Student newStudent);
        Task UpdateStudent(int srn, Student updatedStudent);
    }
}
