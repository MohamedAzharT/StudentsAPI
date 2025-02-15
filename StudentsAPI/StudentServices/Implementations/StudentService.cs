using Microsoft.EntityFrameworkCore;
using StudentsAPI.DataModels;
using StudentsAPI.StudentServices.Interfaces;

namespace StudentsAPI.StudentServices.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _context;

        public StudentService(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.StudentInfo.ToListAsync();
        }

        public async Task<Student?> GetStudentBySRN(int srn)
        {
            return await _context.StudentInfo.FirstOrDefaultAsync(s => s.SRN == srn);
        }

        public async Task AddStudent(Student newStudent)
        {
            // Check if Email is already in use
            var isEmailDuplicate = await _context.StudentInfo
                                    .AnyAsync(s => s.Email == newStudent.Email);

            if (isEmailDuplicate)
            {
                throw new Exception("Student with this Email already exists");
            }

            // Check if Name and Password combination is already used
            var isNamePasswordDuplicate = await _context.StudentInfo
                                             .AnyAsync(s => s.Name == newStudent.Name &&
                                                            s.Password == newStudent.Password);

            if (isNamePasswordDuplicate)
            {
                throw new Exception("Student with the same Name and Password already exists");
            }
            await _context.StudentInfo.AddAsync(newStudent);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateStudent(int srn, Student updatedStudent)
        {
            var existingStudent = await _context.StudentInfo.FirstOrDefaultAsync(u => u.SRN == srn);

            if (existingStudent == null)
            {
                throw new Exception("Student not Found");
            }

            // Check for duplicate Email (exclude current record)
            var isEmailDuplicate = await _context.StudentInfo
                                    .AnyAsync(s => s.Email == updatedStudent.Email && s.SRN != srn);

            if (isEmailDuplicate)
            {
                throw new Exception("Another student with this Email already exists");
            }

            // Check for duplicate Name and Password (exclude current record)
            var isNamePasswordDuplicate = await _context.StudentInfo
                                             .AnyAsync(s => s.Name == updatedStudent.Name &&
                                                            s.Password == updatedStudent.Password &&
                                                            s.SRN != srn);
            if (isNamePasswordDuplicate)
            {
                throw new Exception("Another student with the same Name and Password already exists");
            }

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Password = updatedStudent.Password;
            existingStudent.Department = updatedStudent.Department;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Email = updatedStudent.Email;

            _context.StudentInfo.Update(existingStudent);
           await _context.SaveChangesAsync();
        }

        public Student? Authenticate(string name, string password)
        {
            // Check if a student exists with the given Name and Password
            var student = _context.StudentInfo.FirstOrDefault(s => s.Name == name && s.Password == password);

            if (student == null)
            {
                return null; // Return null if credentials don't match
            }

            return student; // Return student if credentials are valid
        }

    }
}
