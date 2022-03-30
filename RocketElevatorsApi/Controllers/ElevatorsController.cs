#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorsApi.Data;
using RocketElevatorsApi.Models;

namespace RocketElevatorsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ElevatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

         // GET: api/elevators/status/5
        [HttpGet("status/{id}")]
        public async Task<ActionResult<String>> GetElevatorStatus(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator.Status;
        }

        // POST: api/elevators/Status/1?status=offline
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("status/{id}")]
        public async Task<ActionResult<Elevator>> ChangeElevatorStatus(long id, string status)
        {

            var elevator = await _context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }            
            elevator.Status = status;
            await _context.SaveChangesAsync();
            return elevator;
        }

         // GET: api/elevators/offline
        [HttpGet("offline")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetOfflineElevators()
        {
            List<Elevator> allElevators = await _context.elevators.ToListAsync();
            List<Elevator> offlineElevators = new List<Elevator>();
            foreach (Elevator elevator in allElevators) {
                if (elevator.Status == "offline") {
                    offlineElevators.Add(elevator);
                }
            }
            return offlineElevators;
        }

        private bool ElevatorExists(long id)
        {
            return _context.elevators.Any(e => e.Id == id);
        }
    }
}
