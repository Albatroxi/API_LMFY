using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LMFY.Data;
using API_LMFY.Models.questoes;

namespace API_LMFY.Controllers.questoes
{
    [Route("api/[controller]")]
    [ApiController]
    public class questoesController : ControllerBase
    {
        private readonly APIContextoDB _context;

        public questoesController(APIContextoDB context)
        {
            _context = context;
        }

        // GET: api/questoes
        [HttpGet("obterQuestoes")]
        public async Task<ActionResult<IEnumerable<Models.questoes.questoes>>> obterQuestao()
        {
            return await _context.Questoes_Provas.ToListAsync();
        }

        // GET: api/questoes/5
        [HttpGet("obterQuestoes{id}")]
        public async Task<ActionResult<Models.questoes.questoes>> obterQuestao(int id)
        {
            var questoes = await _context.Questoes_Provas.FindAsync(id);

            if (questoes == null)
            {
                return NotFound("Nenhuma questão com o código informado foi encontrado.!");
            }

            return questoes;
        }

        // PUT: api/questoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> Putquestoes(int id, Models.questoes.questoes questoes)
        {
            if (id != questoes.idQuestoes_Provas)
            {
                return BadRequest();
            }

            _context.Entry(questoes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!questoesExists(id))
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

        [HttpPost("cadastrarQuestoes")]
        public async Task<ActionResult<Models.questoes.questoes>> cadastrarQuestao(Models.questoes.questoes questoes)
        {
            var questoes1 = await _context.Turmas.FindAsync(questoes.idQuestoe_Provas);

            if (questoes1 != null)
            {
                return BadRequest("Questão já existe");
            }

            _context.Questoes_Provas.Add(questoes);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("Getquestoes", new { id = questoes.idQuestoes_Provas }, questoes);
            return Ok("Turma criada!");
        }

        // DELETE: api/questoes/5
        [HttpDelete("apagarQuestoes{id}")]
        public async Task<IActionResult> apagarQuestao(int id)
        {
            var questoes = await _context.Questoes_Provas.FindAsync(id);
            if (questoes == null)
            {
                return NotFound();
            }

            _context.Questoes_Provas.Remove(questoes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool questoesExists(int id)
        {
            return _context.Questoes_Provas.Any(e => e.idQuestoe_Provas == id);
        }
    }
}
