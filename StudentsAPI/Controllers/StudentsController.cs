using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.DataModels;
using StudentsAPI.StudentServices;

namespace StudentsAPI.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        // Constructor to inject the StudentService
        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("GetallStudents")]
        public ActionResult<List<Student>> GetStudentslist()
        {
            var students = _studentService.GetAllStudents();
            return Ok(students);
        }

        [HttpGet]
        [Route("GetStudentById/{srn}")]

        public ActionResult<Student> GetStudentBySRN(int srn)
        {
            var student = _studentService.GetStudentBySRN(srn);

            if (student == null)
            {
                return NotFound(new {message = "Student Not Found"});
            }
            return Ok(student);
        }

        [HttpPost]
        [Route("AddStudent")]

        public IActionResult AddStudent([FromBody] Student newStudent)
        {
            if(newStudent == null)
            {
                return BadRequest();
            }
            _studentService.AddStudent(newStudent);
            return Ok(new {message = "Student Record Added Successfully"});
        }

        [HttpPut]
        [Route("UpdateStudent/{srn}")]

        public IActionResult UpdateStudent(int srn, [FromBody] Student updateStudent)
        {
            if (updateStudent == null)
            {
                return BadRequest();
            }
            var existingStudent = _studentService.GetStudentBySRN(srn);

            if (existingStudent == null)
            {
                return NotFound(new { message = "Student not found" });
            }

            _studentService.UpdateStudent(srn, updateStudent);

            return Ok(new { message = "Student record updated successfully" });
        }
    }
}
