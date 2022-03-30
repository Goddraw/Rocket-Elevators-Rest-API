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
        private bool BuildingExists(long id)
        {
            return _context.buildings.Any(e => e.Id == id);
        }
    }
}
