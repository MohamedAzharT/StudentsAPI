using StudentsAPI.DataModels;

namespace StudentsAPI.StudentServices
{
    public class StudentService
    {
        private readonly StudentDbContext _context;

        public StudentService(StudentDbContext context)
        {
            _context = context;
        }

        public List<Student> GetAllStudents()
        {
            return _context.StudentInfo.ToList();
        }

        public Student? GetStudentBySRN(int srn)
        {
            return _context.StudentInfo.FirstOrDefault(s => s.SRN == srn);
        }
        
        public void AddStudent(Student newStudent)
        {
            _context.StudentInfo.Add(newStudent);
            _context.SaveChanges();
        }

        public void UpdateStudent(int srn, Student updatedStudent)
        {
            var existingStudent = _context.StudentInfo.FirstOrDefault(u => u.SRN == srn);
            
            if (existingStudent == null)
            {
                throw new Exception("Student not Found");
            }

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Password = updatedStudent.Password;
            existingStudent.Department = updatedStudent.Department;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Email = updatedStudent.Email;

            _context.StudentInfo.Update(existingStudent);
            _context.SaveChanges();
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
