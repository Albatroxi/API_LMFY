using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LMFY.Data;
using API_LMFY.Models.questoes;
using IdentityServer3.Core.Services;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace API_LMFY.Controllers.questoes
{
    [Route("api/[controller]")]
    [ApiController]
    public class questoesController : ControllerBase
    {
        private readonly APIContextoDB _context;
        private readonly IConfiguration _configuration;

        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration;
        private readonly byte[] _key;
        private readonly string _issuer;
        private readonly string _audience;

        public questoesController(APIContextoDB context, IConfiguration configuration, IMemoryCache cache)
        {
            _context = context;
            _configuration = configuration;

            _cache = cache;
            _cacheExpiration = TimeSpan.FromMinutes(int.Parse(configuration["Jwt:DurationInMinutes"]));
            _key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }

        /*
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok("This is a public endpoint");
        }

        [Authorize]
        [HttpGet("private")]
        public IActionResult Private()
        {
            var userId = User.Identity.IsAuthenticated; // ou outro claim, ex: User.FindFirst(ClaimTypes.NameIdentifier)?.Value

            return Ok($"This is a private endpoint. User ID: {userId}");
        }
        */

        [HttpGet("obterQuestoes")]
        public async Task<ActionResult<IEnumerable<Models.questoes.questoes>>> obterQuestao()
        {
            return await _context.Questoes_Provas.ToListAsync();
        }

        [HttpGet("responderQuestoes")]
        public async Task<ActionResult<IEnumerable<Models.questoes.questoes>>> responderQuestao(int provaID, int respostaINFO)
        {
            var checkPoint = await _context.Questoes_Provas.SingleOrDefaultAsync(x => x.op_correta == respostaINFO && x.idQuestoe_Provas == provaID);

            if (checkPoint != null)
            {
                return Ok("Resposta correta");
            }

            return BadRequest("Resposta incorreta !");

        }

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
            var questoes1 = await _context.Questoes_Provas.FindAsync(questoes.idQuestoe_Provas);

            if (questoes1 != null)
            {
                return BadRequest("Código já existe");
            }

            _context.Questoes_Provas.Add(questoes);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("Getquestoes", new { id = questoes.idQuestoes_Provas }, questoes);
            return Ok("Questão criada!");
        }

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

            return Ok("Questão apagada !");
        }

        private bool questoesExists(int id)
        {
            return _context.Questoes_Provas.Any(e => e.idQuestoe_Provas == id);
        }
    }
}
