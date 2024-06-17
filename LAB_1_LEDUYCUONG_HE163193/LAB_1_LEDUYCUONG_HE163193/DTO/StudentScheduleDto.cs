using LAB_1_LEDUYCUONG_HE163193.Models;

public class StudentScheduleDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public StudentDto Student { get; set; }
    public int ScheduleId { get; set; }
    public ScheduleDto Schedule { get; set; }
    public Status Status { get; set; }
}