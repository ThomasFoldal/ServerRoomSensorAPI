using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerRoomSensorAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ServerRoomSensorAPI.Controllers
{
    [Route("Test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly Datacontext _context;
        public TestController(Datacontext datacontext)
        {
            _context = datacontext;
        }
        [HttpGet]
        public async Task<ActionResult<List<ServerRoom>>> Get()
        {
            return Ok(await _context.ServerRoom.ToListAsync());
        }
    }
}
