using LAB_1_LEDUYCUONG_HE163193.Models;

namespace LAB_1_LEDUYCUONG_HE163193.DTO
{
    public class ScheduleRequest
    {
        public int Slot { get; set; }
        public DateTime Date { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public Status Status { get; set; }
    }
}
