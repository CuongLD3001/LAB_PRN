namespace LAB_1_LEDUYCUONG_HE163193.Models
{
    public enum Status
    {
        Present,
        Absent,
        NotYet
    }
    public class Attendance
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public Status Status { get; set; }
    }

}
