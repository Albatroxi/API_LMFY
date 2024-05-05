using API_LMFY.Data;
using API_LMFY.Helper.users;
using API_LMFY.Models.users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_LMFY.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly APIContextoDB _context;
        private readonly IEmails _emails;

        public usersController(APIContextoDB context, IEmails emails)
        {
            _context = context;
            _emails = emails;
        }

        [HttpGet]
        public async Task<ActionResult<Models.users.users>> UserEMAIL(string email)
        {
            var usersModel = await _context.users.FindAsync(email);

            if (usersModel == null)
            {
                return NotFound();
            }

            return usersModel;
        }


        [HttpPost("register")]
        public async Task<ActionResult<Models.users.users>> registerUser(Models.users.users usersModel)
        {
            _context.users.Add(usersModel);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("resetPass")]

        public async Task<ActionResult<Models.users.users>> resetPassW(string email)
        {
            var usersModel = await _context.users.FindAsync(email);

            if (usersModel != null)
            {
                string newPassW = usersModel.gnewpass();
                string message = $"Sua senha foi redefinida para: {newPassW}";

                bool sendMail = _emails.SendMail(usersModel.email, "Redefinição de Senha", message);

                if (sendMail)
                {
                    return usersModel;
                }

                return BadRequest("Ocorreu um problema ao enviar o email, verifique a conexão com a internet.");
            }

            return BadRequest("E-mail não encontrado !!");
        }

    }
}
