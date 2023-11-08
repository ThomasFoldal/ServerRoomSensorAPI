using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerRoomSensorAPI.DTO;
using ServerRoomSensorAPI.Data;
using ServerRoomSensorAPI.Model;

namespace ServerRoomSensorAPI.Controllers
{
    [ApiController]
    [Route("Temperature")]
    public class ControllerTemperature : ControllerBase
    {
        private readonly Datacontext _context;
        public ControllerTemperature(Datacontext datacontext)
        {
            _context = datacontext;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ServerRoom>>> GetTempAll()
        {
            return Ok(await _context.Temperature.ToListAsync());
        }
        [HttpGet("GetTime")]
        public async Task<ActionResult<Temperature>> GetTempDate(DateTime time)
        {
            return Ok(await _context.Temperature.LastAsync(temp => temp.Time <= time));
        }
        [HttpPost("Add")]
        public async Task<ActionResult<Temperature>> AddTemp(TemperatureDTO temp)
        {
            Temperature newTemp = new Temperature
            {
                Sensor = await _context.Sensor.FirstAsync(s => s.ID == temp.SensorID),
                Time = temp.Time,
                temperature = temp.Temperature
            };
            _context.Temperature.Add(newTemp);
            _context.SaveChanges();
            return Ok(newTemp);
        }
        [HttpPut("Edit")]
        public async Task<ActionResult<List<Temperature>>> EditTemp(int id, TemperatureDTO update)
        {
            Temperature temp = await _context.Temperature.FirstAsync(t => t.ID == id);
            if (temp != null)
            {
                temp.Sensor = await _context.Sensor.FirstAsync(s => s.ID == update.SensorID);
                temp.temperature = update.Temperature;
                temp.Time = update.Time;
                _context.SaveChanges();
                return Ok(await _context.Temperature.FindAsync(id));
            }
            return BadRequest();
        }
        [HttpDelete("Remove")]
        public async Task<ActionResult<List<Temperature>>> RemoveTemp(int id)
        {
            var temp = await _context.Temperature.FindAsync(id);
            if (temp != null)
            {
                _context.Temperature.Remove(temp);
                return Ok(await _context.Temperature.ToListAsync());
            }
            return BadRequest();
        }
    }
}
