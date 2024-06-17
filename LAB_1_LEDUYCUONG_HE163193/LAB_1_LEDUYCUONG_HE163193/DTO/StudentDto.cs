public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<StudentCourseDto> StudentCourses { get; set; }
    public ICollection<StudentScheduleDto> StudentSchedules { get; set; }
}