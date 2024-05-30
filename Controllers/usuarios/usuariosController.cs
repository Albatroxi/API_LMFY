using API_LMFY.Data;
using API_LMFY.Helper.users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;


//using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
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

        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration;
        private readonly byte[] _key;
        private readonly string _issuer;
        private readonly string _audience;



        public usuariosController(APIContextoDB context, IEmails emails, IConfiguration configuration ,IMemoryCache cache)
        {
            _context = context;
            _emails = emails;
            _configuration = configuration;


            _cache = cache;
            _cacheExpiration = TimeSpan.FromMinutes(int.Parse(configuration["Jwt:DurationInMinutes"]));
            _key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];


        }

        [HttpPost("loginAction")]
        public async Task<IActionResult> LoginAsync( string emailEntry, string passEntry)
        {
            var _userService = await _context.usuario.SingleOrDefaultAsync(x => x.email == emailEntry && x.senha == passEntry);
            // Simulação de validação do usuário (em um caso real, você deveria consultar um banco de dados)
            if (_userService != null)
            {
                var token = GenerateToken(
                    _userService.email,
                    _configuration["Jwt:Key"],
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"]
                );

                return Ok(new { Usuario = _userService.nome_completo, Token = token, Perfil = _userService.perfil });
            }

            return Unauthorized("Não autorizado !");
        }

        public static string GenerateToken(string username, string secretKey, string issuer, string audience)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
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

        
        /*
        [HttpPost("loginAction")]
        public async Task<ActionResult<Models.users.usuarios>> login(string emailEntry, string passEntry)
        {
            var _userService = await _context.usuario.SingleOrDefaultAsync(x => x.email == emailEntry && x.senha == passEntry);

            //var user = await _userService.Authenticate(emailEntry, passEntry);

            if (_userService == null)
            {
                return Unauthorized("Usuário não autorizado !");
            }
            
            
            var token = GenerateJwtToken(_userService.email);
            var checkToken = RetrieveTokenFromCache(token.ToString());

            string usuarioLogado = _userService.email;
            string tokenLogado = checkToken.ToString();


            return Ok(new { Usuario = _userService.nome_completo, Token = token, Perfil = _userService.perfil });


        }
        */

        private bool RetrieveTokenFromCache(string token)
        {
            return _cache.TryGetValue(token, out _);
        }

        private object GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(_key);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, email)
            }),
                Expires = DateTime.UtcNow.Add(_cacheExpiration),
                SigningCredentials = creds,
                Issuer = _issuer,
                Audience = _audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            _cache.Set(tokenString, tokenString, _cacheExpiration);

            return tokenString;
        }
    }
}
