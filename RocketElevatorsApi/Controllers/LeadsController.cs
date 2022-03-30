#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevatorsApi.Data;

namespace RocketElevatorsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LeadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lead>>> Getleads()
        {
            return await _context.leads.ToListAsync();
        }

        // GET: api/Leads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lead>> GetLead(long id)
        {
            var lead = await _context.leads.FindAsync(id);

            if (lead == null)
            {
                return NotFound();
            }

            return lead;
        }

//         // GET: api/Leads. GET 30 last days non-customers lead
//         [HttpGet("newleads")]
//         public async Task<ActionResult<IEnumerable<Lead>>> GetNewLeads()
//         {
//             List<Lead> allLeads= await _context.leads.ToListAsync();
//             List<User> allUsers = await _context.users.ToListAsync();
//             List<Customer> allCustomers= await _context.customers.ToListAsync();
//             List<Lead> potentialCustomers = new List<Lead>();
//             List<Lead> recentLeads = new List<Lead>();

// // add a column in a table to match 2 tables. Foreign key between Leads and Customers
//             List<long?> allCustomersUserId = new List<long?>();
//             foreach (Customer customer in allCustomers){
//                 allCustomersUserId.Add(customer.user_id);
//             }

//             foreach (Lead lead in allLeads)
//             {
//                 DateTime today = DateTime.Now;
//                 DateTime leadDate = lead.created_at;    
//                 if ((today - leadDate).TotalDays < 50)
//                 {
//                     recentLeads.Add(lead);
                    
//                 }
//             }

//             Console.WriteLine(recentLeads);

//             List<String> recentLeadEmails = new List<String>();
//             foreach(Lead lead in recentLeads){
//                 recentLeadEmails.Add(lead.email);
//             }

//             Console.WriteLine(recentLeadEmails);

//             foreach (User user in allUsers){
//                 if (!recentLeadEmails.Contains(user.email)){
//                     var lead = await _context.leads.Collection(x => x.email == user.email).LoadAsync();
//                     potentialCustomers.Add(lead);
//                     Console.WriteLine(lead);
//                 } else {
//                     if (!allCustomersUserId.Contains(user.Id)){
                        
//                         var lead = await _context.leads.Where(x => x.email == user.email).LoadAsync();
//                         potentialCustomers.Add(lead);
//                     }
//                 }
//             }

//             return potentialCustomers;
//         }

         // GET: api/Leads. GET 30 last days non-customers lead
        [HttpGet("recentleads")]
        public async Task<ActionResult<IEnumerable<Lead>>> GetRecentLeads()
        {
            List<Lead> allLeads= await _context.leads.ToListAsync();
            List<User> allUsers = await _context.users.ToListAsync();
            List<Customer> allCustomers= await _context.customers.ToListAsync();
            List<Lead> potentialCustomers = new List<Lead>();
            List<Lead> recentLeads = new List<Lead>();

// add a column in a table to match 2 tables. Foreign key between Leads and Customers
            List<long?> allCustomersUserId = new List<long?>();
            foreach (Customer customer in allCustomers){
                allCustomersUserId.Add(customer.user_id);
            }

            foreach (Lead lead in allLeads)
            {
                DateTime today = DateTime.Now;
                DateTime leadDate = lead.created_at;    
                if ((today - leadDate).TotalDays < 50)
                {
                    recentLeads.Add(lead);
                    
                }
            }

            Console.WriteLine(recentLeads);
            return recentLeads;
        }


        // PUT: api/Leads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLead(long id, Lead lead)
        {
            if (id != lead.Id)
            {
                return BadRequest();
            }

            _context.Entry(lead).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeadExists(id))
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

        // POST: api/Leads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lead>> PostLead(Lead lead)
        {
            _context.leads.Add(lead);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLead", new { id = lead.Id }, lead);
        }

        // DELETE: api/Leads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLead(long id)
        {
            var lead = await _context.leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }

            _context.leads.Remove(lead);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeadExists(long id)
        {
            return _context.leads.Any(e => e.Id == id);
        }
    }
}
