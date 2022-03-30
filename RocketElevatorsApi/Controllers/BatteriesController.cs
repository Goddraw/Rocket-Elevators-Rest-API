#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorsApi.Models;
using RocketElevatorsApi.Data;

namespace RocketElevatorsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BatteriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/batteries/status/5
        [HttpGet("status/{id}")]
        public async Task<ActionResult<String>> GetBatteryStatus(long id)
        {
            var battery = await _context.batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery.status;
        }

        // POST: api/batteries/Status/1?status=offline
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("status/{id}")]
        public async Task<ActionResult<Battery>> ChangeBatteryStatus(long id, string status)
        {

            var battery = await _context.batteries.FindAsync(id);
            if (battery == null)
            {
                return NotFound();
            }            
            battery.status = status;
            await _context.SaveChangesAsync();
            return battery;
        }

        private bool BatteryExists(long id)
        {
            return _context.batteries.Any(e => e.Id == id);
        }
    }
}
