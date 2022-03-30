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

        // GET: api/Leads. GET 30 last days non-customers lead
        [HttpGet("newleads")]
        public async Task<ActionResult<IEnumerable<Lead>>> GetNewLeads()
        {
            List<Lead> allLeads= await _context.leads.ToListAsync();
            List<User> allUsers = await _context.users.ToListAsync();
            List<Customer> allCustomers= await _context.customers.ToListAsync();
            List<Lead> potentialCustomers = new List<Lead>();
            List<Lead> recentLeads = new List<Lead>();

            List<string> userEmails = new List<string>();
            foreach (User user in allUsers){
                userEmails.Add(user.email);
            }

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

            List<String> recentLeadEmails = new List<String>();
            foreach(Lead lead in recentLeads){
                recentLeadEmails.Add(lead.email);
            }

            foreach(Lead lead in recentLeads){
                if (!userEmails.Contains(lead.email)){
                    potentialCustomers.Add(lead);
                } else {
                    //check there is a matching user id in customers
                    var user = await _context.users.FirstOrDefaultAsync(x => x.email == lead.email);
                    if (!allCustomersUserId.Contains(user.Id)){
                        potentialCustomers.Add(lead);
                    }
                }
            }
            return potentialCustomers;
        }

        private bool LeadExists(long id)
        {
            return _context.leads.Any(e => e.Id == id);
        }
    }
}