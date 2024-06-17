using Microsoft.EntityFrameworkCore;

namespace LAB_1_LEDUYCUONG_HE163193.Models
{
    public class SchoolLab1Context : DbContext
    {
        public SchoolLab1Context(DbContextOptions<SchoolLab1Context> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}
