using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LAB_1_LEDUYCUONG_HE163193.Models;
using AutoMapper;
using LAB_1_LEDUYCUONG_HE163193.DTO;

namespace LAB_1_LEDUYCUONG_HE163193.Controllers
{
    // SchedulesController
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly SchoolLab1Context _context;
        private readonly IMapper _mapper;

        public SchedulesController(SchoolLab1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedules()
        {
            var schedules = await _context.Schedules.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ScheduleDto>>(schedules));
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDto>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ScheduleDto>(schedule));
        }

        // PUT: api/Schedules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, ScheduleRequest request)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
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

        // POST: api/Schedules
        [HttpPost]
        public async Task<ActionResult<ScheduleDto>> PostSchedule(ScheduleRequest request)
        {
            var schedule = _mapper.Map<Schedule>(request);
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            var scheduleDto = _mapper.Map<ScheduleDto>(schedule);
            return CreatedAtAction("GetSchedule", new { id = schedule.Id }, scheduleDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ScheduleDto>> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();


            return _mapper.Map<ScheduleDto>(schedule);
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }

}
