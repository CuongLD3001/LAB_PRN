using LAB_1_LEDUYCUONG_HE163193.Models;

public class ScheduleDto
{
    public int ScheduleId { get; set; }
    public DateTime Date { get; set; }
    public int Slot { get; set; }
    public int TeacherId { get; set; }
    public Status Status { get; set; }
}