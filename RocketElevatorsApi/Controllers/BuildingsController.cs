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
    public class BuildingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BuildingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> Getbuildings()
        {
            return await _context.buildings.ToListAsync();
        }

        // GET: api/Buildings/intervention
        [HttpGet("intervention")]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildingsWithIntervention()
        {
            List<Building> allBuildings = await _context.buildings.ToListAsync();
            List<Building> buildingsWithInterventions = new List<Building>();
            List<Battery> allBatteries = await _context.batteries.ToListAsync();
            List<Column> allColumns = await _context.columns.ToListAsync();
            List<Elevator> allElevators = await _context.elevators.ToListAsync();
            
            foreach (Battery battery in allBatteries){
                if (battery.status == "intervention"){
                    var building = await _context.buildings.FindAsync(battery.building_id);
                    buildingsWithInterventions.Add(building);
                }
            }

            foreach (Column column in allColumns){
                if (column.status == "intervention"){
                    var battery = await _context.batteries.FindAsync(column.battery_id);
                    var building = await _context.buildings.FindAsync(battery.building_id);
                    buildingsWithInterventions.Add(building);
                }
            }

             foreach (Elevator elevator in allElevators){
                if (elevator.Status == "intervention"){
                    var column = await _context.columns.FindAsync(elevator.column_id);
                    var battery = await _context.batteries.FindAsync(column.battery_id);
                    var building = await _context.buildings.FindAsync(battery.building_id);
                    buildingsWithInterventions.Add(building);
                }
            }

            return buildingsWithInterventions;
        }



        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(long id, Building building)
        {
            if (id != building.Id)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Buildings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            _context.buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilding", new { id = building.Id }, building);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(building);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingExists(long id)
        {
            return _context.buildings.Any(e => e.Id == id);
        }
    }
}
