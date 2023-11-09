using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerRoomSensorAPI.Data;
using ServerRoomSensorAPI.DTO;

namespace ServerRoomSensorAPI.Controllers
{
    [Route("AlarmType")]
    [ApiController]
    public class ControllerAlarmType : ControllerBase
    {
        private readonly Datacontext _context;
        public ControllerAlarmType(Datacontext datacontext)
        {
            _context = datacontext;
        }
        [HttpGet("Get")]
        public async Task<ActionResult<AlarmType>> Get(int id)
        {
            return Ok(await _context.AlarmType.FindAsync(id));
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<AlarmType>> GetAll()
        {
            return Ok(await _context.AlarmType.ToListAsync());
        }
        [HttpPost("Add")]
        public async Task<ActionResult<AlarmType>> Create(string inbound)
        {
            AlarmType alarmType = new AlarmType()
            {
                alarmType = inbound
            };
            await _context.AlarmType.AddAsync(alarmType);
            await _context.SaveChangesAsync();
            return Ok(_context.AlarmType.FirstAsync(at => at.alarmType == inbound));
        }
        [HttpPut("Update")]
        public async Task<ActionResult<AlarmType>> Update(int id, string inbound)
        {
            if (await _context.AlarmType.FindAsync(id) != null)
            {
                AlarmType alarm = await _context.AlarmType.FindAsync(id);
                alarm.alarmType = inbound;
                await _context.SaveChangesAsync();
                return Ok(_context.AlarmType.FindAsync(id));
            }
            return Ok(_context.AlarmType.FindAsync(id));
        }

    }
}
