namespace LAB_1_LEDUYCUONG_HE163193.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
