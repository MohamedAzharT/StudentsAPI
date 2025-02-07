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
            existingStudent.Department = updatedStudent.Department;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Email = updatedStudent.Email;

            _context.StudentInfo.Update(existingStudent);
            _context.SaveChanges();
        }
    }
}
