namespace LAB_1_LEDUYCUONG_HE163193.DTO
{
    public class CourseRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DayOfWeek { get; set; }
        public int Slot { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
    }
}
