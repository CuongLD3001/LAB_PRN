public class StudentCourseDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public StudentDto Student { get; set; }
    public CourseDto Course { get; set; }
}