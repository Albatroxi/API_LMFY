using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LMFY.Data;
using API_LMFY.Models.turmas;

namespace API_LMFY.Controllers.turmas
{
    [Route("api/[controller]")]
    [ApiController]
    public class turmasController : ControllerBase
    {
        private readonly APIContextoDB _context;

        public turmasController(APIContextoDB context)
        {
            _context = context;
        }

        [HttpGet("obterTurmas")]
        public async Task<ActionResult<IEnumerable<Models.turmas.turmas>>> obterTurma()
        {
            return await _context.Turmas.ToListAsync();
        }

        [HttpGet("obterTurmas{id}")]
        public async Task<ActionResult<Models.turmas.turmas>> obterTurmasID(int id)
        {
            var turmas = await _context.Turmas.FindAsync(id);

            if (turmas == null)
            {
                return NotFound("Turma não encontrada!");
            }

            return turmas;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Putturmas(int id, Models.turmas.turmas turmas)
        {
            if (id != turmas.ID)
            {
                return BadRequest();
            }

            _context.Entry(turmas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!turmasExists(id))
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

        [HttpPost("cadastrarTurma")]
        public async Task<ActionResult<Models.turmas.turmas>> cadastrarTurma(Models.turmas.turmas turmas)
        {
            var turmas1 = await _context.Turmas.FindAsync(turmas.ID);

            if (turmas1 != null)
            {
                return BadRequest("Turma já existe");
            }
            _context.Turmas.Add(turmas);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("obterTurma", new { id = turmas.ID }, turmas);
            return Ok("Turma criada!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteturmas(int id)
        {
            var turmas = await _context.Turmas.FindAsync(id);
            if (turmas == null)
            {
                return NotFound();
            }

            _context.Turmas.Remove(turmas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool turmasExists(int id)
        {
            return _context.Turmas.Any(e => e.ID == id);
        }
    }
}
