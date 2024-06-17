using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LAB_1_LEDUYCUONG_HE163193.Models;
using AutoMapper;
using LAB_1_LEDUYCUONG_HE163193.DTO;


[Route("api/[controller]")]
[ApiController]
public class AttendancesController : ControllerBase
{
    private readonly SchoolLab1Context _context;
    private readonly IMapper _mapper;

    public AttendancesController(SchoolLab1Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Attendances
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAttendances()
    {
        var attendances = await _context.Attendances.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<AttendanceDto>>(attendances));
    }

    // GET: api/Attendances/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AttendanceDto>> GetAttendance(int id)
    {
        var attendance = await _context.Attendances.FindAsync(id);
        if (attendance == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<AttendanceDto>(attendance));
    }

    // PUT: api/Attendances/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAttendance(int id, AttendanceRequest request)
    {
        var attendance = await _context.Attendances.FindAsync(id);
        _context.Entry(attendance).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AttendanceExists(id))
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

    // POST: api/Attendances
    [HttpPost]
    public async Task<ActionResult<AttendanceDto>> PostAttendance(AttendanceRequest request)
    {
        var attendance = _mapper.Map<Attendance>(request);
        _context.Attendances.Add(attendance);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAttendance", new { id = attendance.Id }, _mapper.Map<AttendanceDto>(attendance));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AttendanceDto>> DeleteAttendance(int id)
    {
        var attendance = await _context.Attendances.FindAsync(id);
        if (attendance == null)
        {
            return NotFound();
        }

        _context.Attendances.Remove(attendance);
        await _context.SaveChangesAsync();

        return _mapper.Map<AttendanceDto>(attendance);
    }


    private bool AttendanceExists(int id)
    {
        return _context.Attendances.Any(e => e.Id == id);
    }
}
