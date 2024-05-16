using API_LMFY.Data;
using API_LMFY.Helper.users;
using API_LMFY.Models.cursos;
using Microsoft.AspNetCore.Mvc;

namespace API_LMFY.Controllers.cursos
{
    [Route("api/[controller]")]
    [ApiController]
    public class cursosController : Controller
    {
        private readonly APIContextoDB _context;

        public cursosController(APIContextoDB context)
        {
            _context = context;
        }

        [HttpPost("cadastrarCursos")]
        public async Task<ActionResult<Models.cursos.cursos>> registrarUsuario(Models.cursos.cursos cursosModel)
        {
            var cursosModels = await _context.usuarios.FindAsync(cursosModel.nome);

            if (cursosModels == null)
            {
                _context.cursos.Add(cursosModel);
                await _context.SaveChangesAsync();

                return Ok("Curso cadastrado !");
            }
            return BadRequest("Este curso já existe");
        }
    }
}
