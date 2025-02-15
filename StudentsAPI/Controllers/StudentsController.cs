using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.DataModels;
using StudentsAPI.StudentServices.Implementations;
using StudentsAPI.StudentServices.Interfaces;

namespace StudentsAPI.Controllers
{
    [Route("api/Students")]
    [ApiController]
    
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        // Constructor to inject the StudentService
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("GetallStudents")]
        public async Task<ActionResult<List<Student>>> GetStudentslist()
        {
            var students =await _studentService.GetAllStudents();
            return Ok(students);
        }

        [HttpGet]
        [Route("GetStudentById/{srn}")]

        public async Task<ActionResult<Student>> GetStudentBySRN(int srn)
        {
            var student = await _studentService.GetStudentBySRN(srn);

            if (student == null)
            {
                return NotFound(new {message = "Student Not Found"});
            }
            return Ok(student);
        }

        [HttpPost]
        [Route("AddStudent")]

        public async Task<IActionResult> AddStudent([FromBody] Student newStudent)
        {
            if(newStudent == null)
            {
                return BadRequest();
            }
           await _studentService.AddStudent(newStudent);
            return Ok(new {message = "Student Record Added Successfully"});
        }

        [HttpPut]
        [Route("UpdateStudent/{srn}")]

        public async Task<IActionResult> UpdateStudent(int srn, [FromBody] Student updateStudent)
        {
            if (updateStudent == null)
            {
                return BadRequest();
            }
            var existingStudent =await _studentService.GetStudentBySRN(srn);

            if (existingStudent == null)
            {
                return NotFound(new { message = "Student not found" });
            }

           await _studentService.UpdateStudent(srn, updateStudent);

            return Ok(new { message = "Student record updated successfully" });
        }
    }
}
