using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerRoomSensorAPI.Data;
using ServerRoomSensorAPI.DTO;

namespace ServerRoomSensorAPI.Controllers
{
    [Route("Settings")]
    [ApiController]
    public class ControllerSettings : ControllerBase
    {
        private readonly Datacontext _context;
        public ControllerSettings(Datacontext context)
        {
            _context = context;
        }
        [HttpGet("GetOverview")]
        public async Task<ActionResult<List<Settings>>> GetSettings()
        {
            return Ok(await _context.Settings.ToListAsync());
        }
        [HttpGet("GetByName")]
        public async Task<ActionResult<Settings>> GetSetting(string search)
        {
            return Ok(await _context.Settings.FirstAsync(s => s.Setting == search));
        }
        [HttpPut("ChangeSetting")]
        public async Task<ActionResult<Settings>> UpdateSetting(int id, decimal inbound)
        {
            if ( _context.Settings.FirstAsync(s => s.ID == id) != null)
            {
                Settings setting = await _context.Settings.FindAsync(id);
                setting.Variable = inbound;
                await _context.SaveChangesAsync();
                return Ok(setting);
            }
            return BadRequest();
        }
        [HttpPost("NewSetting")]
        public async Task<ActionResult<Settings>> AddSetting(SettingsDTO inbound)
        {
            Settings setting = new Settings
            {
                Setting = inbound.Setting,
                Variable = inbound.Variable
            };
            await _context.Settings.AddAsync(setting);
            await _context.SaveChangesAsync();
            return Ok(await _context.Settings.FirstAsync(s => s.ID == setting.ID));
        }
        [HttpPut("RemoveSetting")]
        public async Task<ActionResult<List<Settings>>> DeleteSetting(int id)
        {
            var setting = await _context.Settings.FindAsync(id);
            if (setting != null)
            {
                _context.Settings.Remove(setting);
                await _context.SaveChangesAsync();
                return Ok(await _context.Settings.ToListAsync());
            }
            return BadRequest();
        }
    }
}
