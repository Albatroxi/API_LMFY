using API_LMFY.Data;
using API_LMFY.Helper.users;
using API_LMFY.Models.users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace API_LMFY.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly APIContextoDB _context;
        private readonly IEmails _emails;
        private readonly IConfiguration _configuration;

        public usersController(APIContextoDB context, IEmails emails, IConfiguration configuration)
        {
            _context = context;
            _emails = emails;
            _configuration = configuration;
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

        [HttpPost("login")]
        public async Task<ActionResult<IEnumerable<Models.users.users>>>  loginActionAsync(string email, string password)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("ConexaoMysql")))
            {
                await connection.OpenAsync();
                //using (var command = new MySqlCommand("SELECT Id, Username, PasswordHash FROM Users WHERE Username = @Username", connection))
                using (var command = new MySqlCommand("SELECT email, pssW FROM users WHERE email = @email AND pssW = @password", connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return await _context.users.ToListAsync();
                        }
                    }

                }
            }

            return BadRequest("E-mail/Login não encontrado !");
        }
    }
}
