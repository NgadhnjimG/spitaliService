using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Spitali.Helpers;
using Spitali.Models;

namespace Spitali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly SpitalsContext _context;

        public DepartmentsController(SpitalsContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var obj= await _context.Departments.ToListAsync();
            return obj;
        }

        [HttpGet("{depid}/institution/{insid}")]
        public async Task<ActionResult<IEnumerable<DoctorsListing>>> GetDepartmentsAndIns(int depid,int insid)
        {
            var obj=await _context.Doctors.Where (x=>x.Hospitals.InstitutionType==insid&&x.Department.DepartmentId==depid).ToListAsync();
            
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:26133/api/StarReviews");
            string responseBody = await response.Content.ReadAsStringAsync();
            List<DoctorReviewDataObject> objq= JsonConvert.DeserializeObject<List<DoctorReviewDataObject>>(responseBody);

            List<DoctorsListing> docList = new List<DoctorsListing>();
            foreach(var doc in obj) {
                DoctorsListing docSingle = new DoctorsListing();
                docSingle.Doctor = doc;
                docSingle.Reviews = objq.Where(x => x.DoctorId == doc.DoctorId).FirstOrDefault
                    ();
                if (docSingle.Reviews == null) docSingle.Reviews = new DoctorReviewDataObject();
                docList.Add(docSingle);
            }

            return docList;
        }
        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
    }
}
