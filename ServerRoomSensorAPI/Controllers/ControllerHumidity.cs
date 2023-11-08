using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerRoomSensorAPI.Data;
using ServerRoomSensorAPI.DTO;

namespace ServerRoomSensorAPI.Controllers
{
    [Route("Humidity")]
    [ApiController]
    public class ControllerHumidity : ControllerBase
    {
        private readonly Datacontext _context;
        public ControllerHumidity(Datacontext datacontext)
        {
            _context = datacontext;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Humidity>>> GetAll()
        {
            var room = await _context.Sensor.ToListAsync(); //hack to get Sensor relation in next call
            return Ok(await _context.Humidity.ToListAsync());
        }
        [HttpGet("GetOne")]
        public async Task<ActionResult<Humidity>> GetOne(int id)
        {
            return Ok(await _context.Humidity.FirstAsync(sr => sr.ID == id));
        }
        [HttpPost("Add")]
        public async Task<ActionResult<List<Humidity>>> AddHumidity(HumidityDTO humid)
        {
            Humidity newHumidity = new Humidity
            {
                Sensor = await _context.Sensor.FirstAsync(s => s.ID == humid.SensorID),
                humidity = humid.Humidity,
                Time = humid.Time
            };
            _context.Humidity.Add(newHumidity);
            await _context.SaveChangesAsync();
            return Ok(await _context.Humidity.ToListAsync());
        }
    }
}
