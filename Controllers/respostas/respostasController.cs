using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LMFY.Data;
using API_LMFY.Models.respostas;

namespace API_LMFY.Controllers.respostas
{
    [Route("api/[controller]")]
    [ApiController]
    public class respostasController : ControllerBase
    {
        private readonly APIContextoDB _context;

        public respostasController(APIContextoDB context)
        {
            _context = context;
        }

        [HttpGet("obterRespostas")]
        public async Task<ActionResult<IEnumerable<Models.respostas.respostas>>> obterResposta()
        {
            return await _context.Respostas_Alunos.ToListAsync();
        }

        [HttpGet("obterRespostas{id}")]
        public async Task<ActionResult<Models.respostas.respostas>> obterResposta(int id)
        {
            var respostas = await _context.Respostas_Alunos.FindAsync(id);

            if (respostas == null)
            {
                return NotFound();
            }

            return respostas;
        }

        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> Putrespostas(int id, Models.respostas.respostas respostas)
        {
            if (id != respostas.ID)
            {
                return BadRequest();
            }

            _context.Entry(respostas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!respostasExists(id))
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

        [HttpPost("cadastrarRespostas")]
        public async Task<ActionResult<Models.respostas.respostas>> cadastrarResposta(Models.respostas.respostas respostas)
        {
            _context.Respostas_Alunos.Add(respostas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getrespostas", new { id = respostas.ID }, respostas);
        }

        [HttpDelete("apagarRespostas{id}")]
        public async Task<IActionResult> apagarResposta(int id)
        {
            var respostas = await _context.Respostas_Alunos.FindAsync(id);
            if (respostas == null)
            {
                return NotFound();
            }

            _context.Respostas_Alunos.Remove(respostas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool respostasExists(int id)
        {
            return _context.Respostas_Alunos.Any(e => e.ID == id);
        }
    }
}
