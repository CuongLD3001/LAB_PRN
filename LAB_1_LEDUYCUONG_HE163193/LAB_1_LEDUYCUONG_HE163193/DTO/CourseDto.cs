
public class CourseDto
{
    public int CourseId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DayOfWeek { get; set; }
    public int Slot { get; set; }
    public ICollection<ScheduleDto> Schedules { get; set; }
}