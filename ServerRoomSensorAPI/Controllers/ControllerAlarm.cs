using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ServerRoomSensorAPI.Data;
using ServerRoomSensorAPI.DTO;

namespace ServerRoomSensorAPI.Controllers
{
    [Route("Alarm")]
    [ApiController]
    public class ControllerAlarm : ControllerBase
    {
        private readonly Datacontext _context;
        public ControllerAlarm(Datacontext datacontext)
        {
            _context = datacontext;
        }
        [HttpGet("Report")]
        public async Task<ActionResult<List<Alarm>>> GetAll()
        {
            return Ok(await _context.Alarm.ToListAsync());
        }
        [HttpGet("Room")]
        public async Task<ActionResult<List<Alarm>>> GetFromRoom(ServerRoom room)
        {
            return Ok(await _context.Alarm.Where(a => a.Sensor.Room == room).ToListAsync());
        }
        [HttpPost("Log")]
        public async Task<ActionResult<Alarm>> AddAlarm(AlarmDTO inbound)
        {
            if (await _context.Sensor.FirstAsync(s => s.ID == inbound.SensorID) != null)
            {
                if (await _context.AlarmType.FirstAsync(at => at.ID == inbound.AlarmTypeID) != null)
                {
                    Alarm alarm = new Alarm
                    {
                        AlarmType = await _context.AlarmType.FirstAsync(at => at.ID == inbound.AlarmTypeID),
                        Sensor = await _context.Sensor.FirstAsync(s => s.ID == inbound.SensorID),
                        Time = inbound.time
                    };
                    await _context.Alarm.AddAsync(alarm);
                    await _context.SaveChangesAsync();
                    return Ok(_context.Alarm.FindAsync(alarm.ID));
                }
                return BadRequest();
            }
            return BadRequest();
        }
        [HttpPut("Change")]
        public async Task<ActionResult<Alarm>> UpdateAlarm(int id, AlarmDTO inbound)
        {
            if (await _context.Alarm.FindAsync(id) != null)
            {
                Alarm alarm = await _context.Alarm.FindAsync(id);
                alarm.Time = inbound.time;
                alarm.Sensor = await _context.Sensor.FindAsync(inbound.SensorID);
                alarm.AlarmType = await _context.AlarmType.FindAsync(inbound.AlarmTypeID);
                await _context.SaveChangesAsync();
                return Ok(_context.Alarm.FindAsync(id));
            }
            return Ok(_context.Alarm.FindAsync(id));
        }
    }
}
