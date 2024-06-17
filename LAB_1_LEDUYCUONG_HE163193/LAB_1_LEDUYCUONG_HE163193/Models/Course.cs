namespace LAB_1_LEDUYCUONG_HE163193.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}