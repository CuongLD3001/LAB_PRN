using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LAB_1_LEDUYCUONG_HE163193.Models;
using AutoMapper;
using LAB_1_LEDUYCUONG_HE163193.DTO;

namespace LAB_1_LEDUYCUONG_HE163193.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly SchoolLab1Context _context;
        private readonly IMapper _mapper;

        public CoursesController(SchoolLab1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(course));
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseRequest request)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        [HttpPost]
        public async Task<ActionResult<CourseDto>> PostCourse(CourseRequest request)
        {
            var course = _mapper.Map<Course>(request);
            course.Subject = await _context.Subjects.FindAsync(request.SubjectId); // Set the Subject of the Course
            if (course.Subject == null)
            {
                return BadRequest("SubjectId does not exist.");
            }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            if (request.EndDate <= request.StartDate)
            {
                return BadRequest("EndDate must be after StartDate.");
            }
            if (request.DayOfWeek < 0 || request.DayOfWeek > 6)
            {
                return BadRequest("DayOfWeek must be between 0 and 6.");
            }
            // Create schedules for the course
            var startDate = course.StartDate;
            while (startDate < course.EndDate)
            {
                if ((int)startDate.DayOfWeek == request.DayOfWeek)
                {
                    var schedule = new Schedule
                    {
                        Slot = request.Slot,
                        Date = startDate,
                        TeacherId = request.TeacherId,
                        CourseId = course.CourseId,
                    };
                    _context.Schedules.Add(schedule);
                }
                startDate = startDate.AddDays(1);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, _mapper.Map<CourseDto>(course));
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseDto>> DeleteCourse(int id)
        {
            var course = await _context.Courses.Include(c => c.Schedules).FirstOrDefaultAsync(c => c.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            // Xóa t?t c? các l?ch h?c liên quan ??n khóa h?c
            foreach (var schedule in course.Schedules)
            {
                _context.Schedules.Remove(schedule);
            }

            // Xóa khóa h?c
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseDto>(course);
        }


        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
