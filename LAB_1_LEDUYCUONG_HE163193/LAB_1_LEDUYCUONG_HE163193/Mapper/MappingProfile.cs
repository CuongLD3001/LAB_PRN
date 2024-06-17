using AutoMapper;
using LAB_1_LEDUYCUONG_HE163193.DTO;
using LAB_1_LEDUYCUONG_HE163193.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CourseRequest>().ReverseMap();
        CreateMap<Schedule, ScheduleRequest>().ReverseMap();
        CreateMap<Schedule, ScheduleDto>().ReverseMap();
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student, StudentRequest>().ReverseMap();
        CreateMap<StudentCourse, StudentCourseDto>().ReverseMap();
        CreateMap<StudentCourse, StudentCourseRequest>().ReverseMap();
        CreateMap<Subject, SubjectDto>().ReverseMap();
        CreateMap<Subject, SubjectRequest>().ReverseMap();
        CreateMap<Teacher, TeacherDto>().ReverseMap();
        CreateMap<Teacher, TeacherRequest>().ReverseMap();
        CreateMap<Attendance, AttendanceDto>().ReverseMap();
        CreateMap<Attendance, AttendanceRequest>().ReverseMap();
    }
}