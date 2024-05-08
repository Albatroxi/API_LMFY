using API_LMFY.Data;
using API_LMFY.Helper.users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_LMFY.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly APIContextoDB _context;
        private readonly IEmails _emails;
        private readonly IConfiguration _configuration;


        
        private string GenerateJwtToken(Models.users.users userReference)
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

        [HttpPost("changePass")]

        public async Task<ActionResult<Models.users.users>> changePassW(/*HttpContext context,*/ string token, string email, string passwordNew)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "name").Value);
                var userEMAIL = jwtToken.Claims.First(x => x.Type == "name").Value;


            }
            catch
            {
                return BadRequest("Autenticação não validada !");
            }
            return Ok("Autenticação Validada !");
        }


        [HttpPost("loginAction")]
        public async Task<string> loginAction(string emailEntry, string passEntry)
        {
            var emailINFO = await _context.users.Where(x => x.email.ToLower() == emailEntry.ToLower()).FirstOrDefaultAsync();
            var passwordINFO = await _context.users.Where(x => x.pssW.ToLower() == passEntry.ToLower()).FirstOrDefaultAsync();

            if (emailINFO == null || passwordINFO == null)
            {
                return "Usuário não autorizado !";
            }

            return GenerateJwtToken(emailINFO);
        }
    }
}
