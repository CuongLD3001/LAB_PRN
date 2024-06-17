using LAB_1_LEDUYCUONG_HE163193.Models;

namespace LAB_1_LEDUYCUONG_HE163193.DTO
{
    public class AttendanceRequest
    {
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public Status Status { get; set; }
    }
}
