using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LAB_1_LEDUYCUONG_HE163193.Models;
using AutoMapper;
using LAB_1_LEDUYCUONG_HE163193.DTO;
using Microsoft.AspNetCore.OData.Query;

namespace LAB_1_LEDUYCUONG_HE163193.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly SchoolLab1Context _context;
        private readonly IMapper _mapper;

        public TeachersController(SchoolLab1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery] // Enable OData query capabilities
        public async Task<ActionResult<IQueryable<TeacherDto>>> GetTeachers()
        {
            var teachers = _context.Teachers.AsQueryable();
            return Ok(_mapper.ProjectTo<TeacherDto>(teachers));
        }
        // GET: api/Teachers/5
        [HttpGet("{id}")]
        [EnableQuery] // Enable OData query capabilities
        public async Task<ActionResult<TeacherDto>> GetTeacher(int id)
        {
            var teacher = await _context.Teachers.Include(t => t.Schedules).FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            var teacherDto = _mapper.Map<TeacherDto>(teacher);
            teacherDto.Schedules = _mapper.Map<ICollection<ScheduleDto>>(teacher.Schedules);
            return Ok(teacherDto);
        }


        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, TeacherRequest request)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<TeacherDto>> PostTeacher(TeacherRequest request)
        {
            var teacher = _mapper.Map<Teacher>(request);
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            var teacherDto = _mapper.Map<TeacherDto>(teacher);
            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacherDto);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TeacherDto>> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.Include(t => t.Schedules).FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            var teacherDto = _mapper.Map<TeacherDto>(teacher);

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            teacherDto.Schedules = _mapper.Map<ICollection<ScheduleDto>>(teacher.Schedules);

            return teacherDto;
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
