using LAB_1_LEDUYCUONG_HE163193.Models;

namespace LAB_1_LEDUYCUONG_HE163193.DTO
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public Status Status { get; set; }
    }
}
