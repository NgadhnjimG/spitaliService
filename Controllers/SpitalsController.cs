using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Spitali.Helpers;
using Spitali.Models;
using Spitali.Services;

namespace Spitali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpitalsController : ControllerBase
    {
        private readonly SpitalsContext _context;
        private IAuthService _authService;

        public SpitalsController(SpitalsContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // GET: api/Spitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spital>>> GetSpitals()
        {
            return await _context.Spitals.ToListAsync();
        }

        // GET: api/Spitals/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Spital>> GetSpital(int id)
        {
            var spital = await _context.Spitals.FindAsync(id);

            if (spital == null)
            {
                return NotFound();
            }

            return spital;
        }


        // PUT: api/Spitals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpital(int id, Spital spital)
        {
            if (id != spital.SpitalId)
            {
                return BadRequest();
            }

            _context.Entry(spital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpitalExists(id))
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

        // POST: api/Spitals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Spital>> PostSpital(Spital spital)
        {
            _context.Spitals.Add(spital);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpital", new { id = spital.SpitalId }, spital);
        }

        // DELETE: api/Spitals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpital(int id)
        {
            var spital = await _context.Spitals.FindAsync(id);
            if (spital == null)
            {
                return NotFound();
            }

            _context.Spitals.Remove(spital);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpitalExists(int id)
        {
            return _context.Spitals.Any(e => e.SpitalId == id);
        }

        [HttpGet("login/{email}/{password}")]
        public async Task<ActionResult<bool>> login(string email, string password)
        {

            User user = new User();
            user.Email = email;
            user.Password = password;

            HttpClient client = new HttpClient();
            var myContent = JsonConvert.SerializeObject(user);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage user = await client.PostAsync("http://localhost:26133/api/Users/login", byteContent);
            var res = _authService.generateJwtToken(user);

            // string responseBody = await response.Content.ReadAsStringAsync();
            // return responseBody.ToLower() == "true";
            return Ok(res);
        }

        //api/Spitals/rate/doctor/' + doctorId + "/star/+ starRate + '/comment/' + comment,
        [HttpGet("rate/doctor/{doctorId}/star/{starRate}/comment/{comment}")]
        public async Task<ActionResult<bool>> reviewSent(int doctorId, int starRate, string comment)
        {
            ReviewHelper review = new ReviewHelper();
            review.Comment = comment;
            review.DoctorId = doctorId;
            review.StarRate = starRate;
            HttpClient client = new HttpClient();
            var myContent = JsonConvert.SerializeObject(review);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:26133/api/StarReviews", byteContent);

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody.ToLower() == "true";
        }

    }
}
