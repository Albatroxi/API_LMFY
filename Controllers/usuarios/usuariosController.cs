using API_LMFY.Data;
using API_LMFY.Helper.users;
using API_LMFY.Models.users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Mysqlx;
using NuGet.Common;
using Org.BouncyCastle.Crypto.Digests;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
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

        [HttpPost("registrarUsuario")]
        public async Task<ActionResult<Models.users.usuarios>> registrarUsuario(Models.users.usuarios usersModel)
        {
            var usersModels = await _context.usuario.FindAsync(usersModel.email);

            if (usersModels == null)
            {
                _context.usuario.Add(usersModel);
                await _context.SaveChangesAsync();

                return Ok("Registro realizado !");
            }
            return BadRequest("E-mail já registrado");
        }

        [HttpPost("esqueciasenhaUsuario")]

        public async Task<ActionResult<Models.users.usuarios>> esqueciAsenha(string email)
        {
            var usersModel = await _context.usuario.FindAsync(email);

            MailMessage emailMessage = new MailMessage();

            if (usersModel != null)
            {
                string newPassW = usersModel.gnewpass();
                string message = $"Sua senha foi redefinida para: {newPassW}";

                try
                {
                    var smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);
                    smtpClient.EnableSsl = true;
                    smtpClient.Timeout = 60 * 60;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("devalbatrox@hotmail.com", "Lapsparks02@");

                    emailMessage.From = new MailAddress("devalbatrox@hotmail.com", "SISTEMA DE PROVAS");
                    emailMessage.Body = message;
                    emailMessage.Subject = "Redefinição de Senha";
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Priority = MailPriority.Normal;
                    emailMessage.To.Add(email);

                    smtpClient.Send(emailMessage);

                    await _context.SaveChangesAsync();

                    return Ok("Senha alterada e enviada ao e-mail cadastrado.");
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Erro = ex.Message });
                }

            }
            return NotFound("Usuário não encontrado!");
        }

        [HttpPost("loginAction")]
        public async Task<ActionResult<Models.users.usuarios>> login(string emailEntry, string passEntry)
        {
            var usermail = await _context.usuario.SingleOrDefaultAsync(x => x.email == emailEntry);
            var userpass = await _context.usuario.SingleOrDefaultAsync(x => x.senha == passEntry);

            if (usermail == null || userpass == null)
            {
                return Unauthorized("Usuário não autorizado !");
            }

            var token = GenerateJwtToken(usermail);


            return Ok( new { Usuario = usermail.nome_completo, Token = token, Perfil = usermail.perfil});
        }
    }
}
