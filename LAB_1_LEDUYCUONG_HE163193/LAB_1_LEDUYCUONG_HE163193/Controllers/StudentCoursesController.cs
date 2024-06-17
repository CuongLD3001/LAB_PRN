using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LAB_1_LEDUYCUONG_HE163193.Models;
using AutoMapper;
using LAB_1_LEDUYCUONG_HE163193.DTO;


[Route("api/[controller]")]
[ApiController]
public class StudentCoursesController : ControllerBase
{
    private readonly SchoolLab1Context _context;
    private readonly IMapper _mapper;

    public StudentCoursesController(SchoolLab1Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/StudentCourses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentCourseDto>>> GetStudentCourses()
    {
        var studentCourses = await _context.StudentCourses.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<StudentCourseDto>>(studentCourses));
    }

    // GET: api/StudentCourses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentCourseDto>> GetStudentCourse(int id)
    {
        var studentCourse = await _context.StudentCourses.FindAsync(id);
        if (studentCourse == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<StudentCourseDto>(studentCourse));
    }

    // PUT: api/StudentCourses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudentCourse(int id, StudentCourseRequest request)
    {
        var studentCourse = await _context.StudentCourses.FindAsync(id);
        _context.Entry(studentCourse).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentCourseExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/StudentCourses
    [HttpPost]
    public async Task<ActionResult<StudentCourseDto>> PostStudentCourse(StudentCourseRequest request)
    {
        var studentCourse = _mapper.Map<StudentCourse>(request);
        _context.StudentCourses.Add(studentCourse);
        await _context.SaveChangesAsync();

        // Get all schedules for the course
        var schedules = _context.Schedules.Where(s => s.CourseId == request.CourseId);

        // Add the student to the attendance table for all schedules of the course
        foreach (var schedule in schedules)
        {
            var attendance = new Attendance
            {
                ScheduleId = schedule.Id,
                StudentId = request.StudentId,
                Status = Status.NotYet
            };
            _context.Attendances.Add(attendance);
        }
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStudentCourse", new { id = studentCourse.Id }, _mapper.Map<StudentCourseDto>(studentCourse));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<StudentCourseDto>> DeleteStudentCourse(int id)
    {
        var studentCourse = await _context.StudentCourses.FindAsync(id);
        if (studentCourse == null)
        {
            return NotFound();
        }

        _context.StudentCourses.Remove(studentCourse);
        await _context.SaveChangesAsync();

        return _mapper.Map<StudentCourseDto>(studentCourse);
    }


    private bool StudentCourseExists(int id)
    {
        return _context.StudentCourses.Any(e => e.Id == id);
    }
}
