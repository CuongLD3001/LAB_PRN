namespace LAB_1_LEDUYCUONG_HE163193.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public int Slot { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
