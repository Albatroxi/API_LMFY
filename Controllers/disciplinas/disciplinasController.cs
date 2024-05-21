using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LMFY.Data;
using API_LMFY.Models.disciplinas;

namespace API_LMFY.Controllers.disciplinas
{
    [Route("api/[controller]")]
    [ApiController]
    public class disciplinasController : ControllerBase
    {
        private readonly APIContextoDB _context;

        public disciplinasController(APIContextoDB context)
        {
            _context = context;
        }

        [HttpGet("obterDisciplinas")]
        public async Task<ActionResult<IEnumerable<Models.disciplinas.disciplinas>>> obterDisciplina()
        {
            return await _context.Disciplinas.ToListAsync();
        }

        [HttpGet("obterDisciplinas{id}")]
        public async Task<ActionResult<Models.disciplinas.disciplinas>> obterDisciplina(int id)
        {
            var disciplinas = await _context.Disciplinas.FindAsync(id);

            if (disciplinas == null)
            {
                return NotFound("Nenhuma disciplina com o código informado foi encontrado.!");
            }

            return disciplinas;
        }

        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> Putdisciplinas(int id, Models.disciplinas.disciplinas disciplinas)
        {
            if (id != disciplinas.ID)
            {
                return BadRequest();
            }

            _context.Entry(disciplinas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!disciplinasExists(id))
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
        */

        [HttpPost("cadastrarDisciplinas")]
        public async Task<ActionResult<Models.disciplinas.disciplinas>> cadastrarDisciplina(Models.disciplinas.disciplinas disciplinas)
        {
            var disciplinas1 = await _context.Disciplinas.FindAsync(disciplinas.ID);

            if (disciplinas1 != null)
            {
                return BadRequest("Turma já existe");
            }

                _context.Disciplinas.Add(disciplinas);
                await _context.SaveChangesAsync();

            //return CreatedAtAction("Getdisciplinas", new { id = disciplinas.ID }, disciplinas);
            return Ok("Disciplina criada!");
        }

        [HttpDelete("apagarDisciplinas{id}")]
        public async Task<IActionResult> apagarDisciplina(int id)
        {
            var disciplinas = await _context.Disciplinas.FindAsync(id);
            if (disciplinas == null)
            {
                return NotFound();
            }

            _context.Disciplinas.Remove(disciplinas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool disciplinasExists(int id)
        {
            return _context.Disciplinas.Any(e => e.ID == id);
        }
    }
}
