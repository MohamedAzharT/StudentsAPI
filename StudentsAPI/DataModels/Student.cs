using System.ComponentModel.DataAnnotations;

namespace StudentsAPI.DataModels
{
    public class Student
    {
        [Key]
        public int SRN { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
    }
}
