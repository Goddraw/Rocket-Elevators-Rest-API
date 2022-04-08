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
    public class InterventionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InterventionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetNewInterventions()
        {
            List<Intervention> allInterventions = await _context.interventions.ToListAsync();
            List<Intervention> pendingInterventions = new List<Intervention>();

            foreach (Intervention intervention in allInterventions)
            {
                if (intervention.startDateAndTimeOfIntervention == null && intervention.status == "Pending")
                    pendingInterventions.Add(intervention);
            }
            return pendingInterventions;
        }
        // PUT: api/interventions/status/1/inprogress  If status was changed to inprogress
        [HttpPut("status/{id}/inprogress")]
        public async Task<ActionResult<Intervention>> ChangeInterventionInProgress(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }
            intervention.status = "InProgress";
            intervention.startDateAndTimeOfIntervention = DateTime.Now;  
            await _context.SaveChangesAsync();
            return intervention;
        }
        // PUT: api/interventions/Status/1/completed if status was changed to Completed
        [HttpPut("status/{id}/completed")]
        public async Task<ActionResult<Intervention>> ChangeInterventionCompleted(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }
            intervention.status = "Completed";
            intervention.endDateAndTimeOfIntervention = DateTime.Now;
            await _context.SaveChangesAsync();
            return intervention;
        }
    }
}