using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LMFY.Data;
using API_LMFY.Models.provas_org;

namespace API_LMFY.Controllers.provas_org
{
    [Route("api/[controller]")]
    [ApiController]
    public class provas_orgController : ControllerBase
    {
        private readonly APIContextoDB _context;

        public provas_orgController(APIContextoDB context)
        {
            _context = context;
        }

        // GET: api/provas_org
        [HttpGet("obterTodas")]
        public async Task<ActionResult<IEnumerable<Models.provas_org.provas_org>>> Getprovas_org()
        {
            return await _context.tbl_provas.ToListAsync();
        }

        // GET: api/provas_org/5
        [HttpGet("obterPor{id}")]
        public async Task<ActionResult<Models.provas_org.provas_org>> Getprovas_org(int id)
        {
            var provas_org = await _context.tbl_provas.FindAsync(id);

            if (provas_org == null)
            {
                return NotFound();
            }

            return provas_org;
        }

        // PUT: api/provas_org/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("editarPor{id}")]
        public async Task<IActionResult> Putprovas_org(int id, Models.provas_org.provas_org provas_org)
        {
            if (id != provas_org.id)
            {
                return BadRequest();
            }

            _context.Entry(provas_org).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!provas_orgExists(id))
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

        [HttpPost("criarNova")]
        public async Task<ActionResult<Models.provas_org.provas_org>> Postprovas_org(string userMail)
        {
            // Cria uma instância da classe Random
            Random random = new Random();

            Models.provas_org.provas_org teste = new Models.provas_org.provas_org();

            // Cria um array para armazenar os números aleatórios
            int[] numerosAleatorios = new int[10];

            // Gera os números aleatórios e os armazena no array
            for (int i = 0; i < numerosAleatorios.Length; i++)
            {
                numerosAleatorios[i] = random.Next(1, 50); // Gera um número entre 0 e 100 (inclusivo)

            }

            // Exibe os números gerados
            Console.WriteLine("Números aleatórios gerados:");
            foreach (int numero in numerosAleatorios)
            {
                // Atribuindo os valores do array a variáveis separadas
                int questaoID1 = numerosAleatorios[0];
                int questaoID2 = numerosAleatorios[1];
                int questaoID3 = numerosAleatorios[2];
                int questaoID4 = numerosAleatorios[3];
                int questaoID5 = numerosAleatorios[4];
                int questaoID6 = numerosAleatorios[5];
                int questaoID7 = numerosAleatorios[6];
                int questaoID8 = numerosAleatorios[7];
                int questaoID9 = numerosAleatorios[8];
                int questaoID10 = numerosAleatorios[9];

                var questao1 = await _context.Questoes_Provas.FindAsync(questaoID1);
                var questao2 = await _context.Questoes_Provas.FindAsync(questaoID2);
                var questao3 = await _context.Questoes_Provas.FindAsync(questaoID3);
                var questao4 = await _context.Questoes_Provas.FindAsync(questaoID4);
                var questao5 = await _context.Questoes_Provas.FindAsync(questaoID5);
                var questao6 = await _context.Questoes_Provas.FindAsync(questaoID6);
                var questao7 = await _context.Questoes_Provas.FindAsync(questaoID7);
                var questao8 = await _context.Questoes_Provas.FindAsync(questaoID8);
                var questao9 = await _context.Questoes_Provas.FindAsync(questaoID9);
                var questao10 = await _context.Questoes_Provas.FindAsync(questaoID10);

                var novaProva = new Models.provas_org.provas_org
                {
                    q_1 = questao1.idQuestoe_Provas,
                    q_1_resp = false,

                    q_2 = questao2.idQuestoe_Provas,
                    q_2_resp = false,

                    q_3 = questao3.idQuestoe_Provas,
                    q_3_resp = false,

                    q_4 = questao4.idQuestoe_Provas,
                    q_4_resp = false,

                    q_5 = questao5.idQuestoe_Provas,
                    q_5_resp = false,

                    q_6 = questao6.idQuestoe_Provas,
                    q_6_resp = false,

                    q_7 = questao7.idQuestoe_Provas,
                    q_7_resp = false,

                    q_8 = questao8.idQuestoe_Provas,
                    q_8_resp = false,

                    q_9 = questao9.idQuestoe_Provas,
                    q_9_resp = false,

                    q_10 = questao10.idQuestoe_Provas,
                    q_10_resp = false,

                    usermail = userMail,
                    finish = false
                };

                _context.tbl_provas.Add(novaProva);
                await _context.SaveChangesAsync();

                return Ok(new { Questão1 = questao1, Questão2 = questao2, Questão3 = questao3, Questão4 = questao4, Questão5 = questao5, Questão6 = questao6, Questão7 = questao7, Questão8 = questao8, Questão9 = questao9, Questão10 = questao10 });

            }

            return Ok("Prova gerada!");

        }

        /*
        // POST: api/provas_org
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("criarNova")]
        public async Task<ActionResult<Models.provas_org.provas_org>> Postprovas_org(Models.provas_org.provas_org provas_org)
        {
            _context.provas_org.Add(provas_org);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getprovas_org", new { id = provas_org.id }, provas_org);
        }
        */

        // DELETE: api/provas_org/5
        [HttpDelete("apagarPor{id}")]
        public async Task<IActionResult> Deleteprovas_org(int id)
        {
            var provas_org = await _context.tbl_provas.FindAsync(id);
            if (provas_org == null)
            {
                return NotFound();
            }

            _context.tbl_provas.Remove(provas_org);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool provas_orgExists(int id)
        {
            return _context.tbl_provas.Any(e => e.id == id);
        }
    }
}
