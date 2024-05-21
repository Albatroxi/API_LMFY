using API_LMFY.Data;
using API_LMFY.Helper.users;
using API_LMFY.Models.cursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_LMFY.Controllers.cursos
{
    [Route("api/[controller]")]
    [ApiController]
    public class cursosController : Controller
    {
        private readonly APIContextoDB _context;
        private readonly IEmails _emails;
        private readonly IConfiguration _configuration;

        public cursosController(APIContextoDB context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("obterCursos")]
        public async Task<ActionResult<IEnumerable<Models.cursos.cursos>>> ObterCursos()
        {
            var cursos = await _context.cursos.ToListAsync();

            if (cursos == null || !cursos.Any())
            {
                return NotFound("Não possui cursos cadastrados!");
            }

            return Ok(cursos);
        }

        [HttpPost("cadastrarCursos")]
        public async Task<ActionResult<Models.cursos.cursos>> cadastrarCurso(Models.cursos.cursos cursosModel)
        {
            var cursosModels = await _context.cursos.FindAsync(cursosModel.nome);

            if (cursosModels == null)
            {
                _context.cursos.Add(cursosModel);
                await _context.SaveChangesAsync();

                return Ok("Registro realizado !");
            }
            return BadRequest("Curso já cadastrado");
        }

        /*
        [HttpPost("editarCursos")]
        public async Task<IActionResult> editarCurso(string cursoNome, string NovocursoNome)
        {
            var cursosModels = await _context.cursos.FindAsync(cursoNome);

            if (cursosModels != null)
            {
                cursosModels.nome = NovocursoNome;
                cursosModels.id = cursosModels.id;
                _context.cursos.Update(cursosModels);
                await _context.SaveChangesAsync();

                //return Ok(new {NOME = cursosModels.nome, ID = cursosModels.id});
                return Ok("Atualizado!");
            }

            return NotFound("Nada encontrado !");
        }
        */

        [HttpDelete("apagarCursos")]
        public async Task<ActionResult<Models.cursos.cursos>> apagarCurso(string cursoNome)
        {
            var cursosModels = await _context.cursos.FindAsync(cursoNome);

            if (cursosModels != null)
            {
                _context.cursos.Remove(cursosModels);
                await _context.SaveChangesAsync();

                return Ok("Registro apagado !");
            }
            return NotFound("Código inexistente !");
        }

        private bool cursosExists(string id)
        {
            return _context.cursos.Any(e => e.nome == id);
        }
    }

}
