using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerRoomSensorAPI.Data;
using ServerRoomSensorAPI.DTO;

namespace ServerRoomSensorAPI.Controllers
{
    [ApiController]
    [Route("ServerRoom")]
    public class ControllerServerRoom : Controller
    {
        private readonly Datacontext _context;
        public ControllerServerRoom(Datacontext datacontext)
        {
            _context = datacontext;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ServerRoom>>> GetAll()
        {
            return Ok(await _context.ServerRoom.ToListAsync());
        }
        [HttpGet("GetOne")]
        public async Task<ActionResult<ServerRoom>> GetOne(int id)
        {
            return Ok(await _context.ServerRoom.FirstAsync(sr => sr.ID == id));
        }
        [HttpPost("Add")]
        public async Task<ActionResult<List<ServerRoom>>> AddServerRoom(ServerRoomDTO serverRoom)
        {
            ServerRoom newSR = new ServerRoom
            {
                Location = serverRoom.location,
                Region = serverRoom.region
            };
            _context.ServerRoom.Add(newSR);
            await _context.SaveChangesAsync();
            return Ok(await _context.ServerRoom.ToListAsync());
        }
    }
}
