using API_LMFY.Data;
using API_LMFY.Helper.users;
using API_LMFY.Models.users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API_LMFY.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly APIContextoDB _context;
        private readonly IEmails _emails;
        private readonly IConfiguration _configuration;



        private string GenerateJwtToken(Models.users.usuarios userReference)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userReference.email.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); ;
        }

        public usuariosController(APIContextoDB context, IEmails emails, IConfiguration configuration)
        {
            _context = context;
            _emails = emails;
            _configuration = configuration;
        }

        #region USUARIOS

        [HttpPost("registrarUsuario")]
        public async Task<ActionResult<Models.users.usuarios>> registrarUsuario(Models.users.usuarios usersModel)
        {
            var usersModels = await _context.usuarios.FindAsync(usersModel.email);

            if (usersModels == null)
            {
                _context.usuarios.Add(usersModel);
                await _context.SaveChangesAsync();

                return Ok("Registro realizado !");
            }
            return BadRequest("E-mail já registrado");
        }

        [HttpPost("esqueciasenhaUsuario")]

        public async Task<ActionResult<Models.users.usuarios>> esqueciAsenha(string email)
        {
            var usersModel = await _context.usuarios.FindAsync(email);


            if (usersModel != null)
            {
                string newPassW = usersModel.gnewpass();
                string message = $"Sua senha foi redefinida para: {newPassW}";

                bool sendMail = _emails.SendMail(usersModel.email, "Redefinição de Senha", message);

                if (sendMail)
                {
                    await _context.SaveChangesAsync();
                    return usersModel;
                }

                return BadRequest("Ocorreu um problema ao enviar o email, verifique a conexão com a internet.");
            }

            return BadRequest("E-mail não encontrado !!");
        }

        [HttpPost("loginAction")]
        public async Task<ActionResult<Models.users.usuarios>> login(string emailEntry, string passEntry)
        {
            var usermail = await _context.usuarios.SingleOrDefaultAsync(x => x.email == emailEntry);
            var userpass = await _context.usuarios.SingleOrDefaultAsync(x => x.pssW == passEntry);

            if (usermail == null || userpass == null)
            {
                return Unauthorized("Usuário não autorizado !");
            }

            return new Models.users.usuarios
            {
                nome = usermail.nome,
                email = usermail.email,
                token = GenerateJwtToken(usermail)
            };
        }
        #endregion USUARIOS
    }
}
