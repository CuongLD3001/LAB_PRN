public class SubjectDto
{
    public int SubjectId { get; set; }
    public string Name { get; set; }
    public int Slot { get; set; }
    public ICollection<CourseDto> Courses { get; set; }
}
