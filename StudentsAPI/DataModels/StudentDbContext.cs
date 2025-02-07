using Microsoft.EntityFrameworkCore;
using StudentsAPI.DataModels;

namespace StudentsAPI.DataModels
{
    public class StudentDbContext : DbContext
    {
        // Constructor to pass options to the base class
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        // Define DbSet for the StudentInfo table
        public DbSet<Student> StudentInfo { get; set; }

    }
}
