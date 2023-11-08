using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerRoomSensorAPI.Data;
using ServerRoomSensorAPI.DTO;
using ServerRoomSensorAPI.Model;

namespace ServerRoomSensorAPI.Controllers
{
    [ApiController]
    [Route("Sensor")]
    public class ControllerSensor : Controller
    {
        private readonly Datacontext _context;
        public ControllerSensor(Datacontext datacontext)
        {
            _context = datacontext;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Sensor>>> GetAll()
        {
            var room = await _context.ServerRoom.ToListAsync(); //hack to get ServerRoom relation in next call
            return Ok(await _context.Sensor.ToListAsync());
        }
        [HttpGet("GetOne")]
        public async Task<ActionResult<Sensor>> GetOne(int id)
        {
            return Ok(await _context.Sensor.FirstAsync(sr => sr.ID == id));
        }
        [HttpPost("Add")]
        public async Task<ActionResult<List<Sensor>>> AddSensor(SensorDTO sensor)
        {
            Sensor newSensor = new Sensor
            {
                Designation = sensor.Designation,
                Room = await _context.ServerRoom.FirstAsync(sr => sr.ID == sensor.RoomID)
            };
            _context.Sensor.Add(newSensor);
            await _context.SaveChangesAsync();
            return Ok(await _context.Sensor.ToListAsync());
        }
    }
}
