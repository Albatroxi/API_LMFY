using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LMFY.Data;
using API_LMFY.Models.users;

namespace API_LMFY.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly APIContextoDB _context;

        public usersController(APIContextoDB context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<usersModel>>> GetusersModel()
        {
            return await _context.usersModel.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<usersModel>> GetusersModel(int id)
        {
            var usersModel = await _context.usersModel.FindAsync(id);

            if (usersModel == null)
            {
                return NotFound();
            }

            return usersModel;
        }

        // PUT: api/users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutusersModel(int id, usersModel usersModel)
        {
            if (id != usersModel.id)
            {
                return BadRequest();
            }

            _context.Entry(usersModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usersModelExists(id))
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

        // POST: api/users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<usersModel>> PostusersModel(usersModel usersModel)
        {
            _context.usersModel.Add(usersModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetusersModel", new { id = usersModel.id }, usersModel);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteusersModel(int id)
        {
            var usersModel = await _context.usersModel.FindAsync(id);
            if (usersModel == null)
            {
                return NotFound();
            }

            _context.usersModel.Remove(usersModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool usersModelExists(int id)
        {
            return _context.usersModel.Any(e => e.id == id);
        }
    }
}
