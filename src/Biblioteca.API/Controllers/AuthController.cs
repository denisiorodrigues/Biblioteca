using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.API.DTO;
using Biblioteca.API.Extensions;
using Biblioteca.API.Model.login;
using Biblioteca.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Biblioteca.API.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/conta")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTSettings _jwtSetting;

        public AuthController(INotificador notificador,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<JWTSettings> jwtSetting,
                              IUser user) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSetting = jwtSetting.Value;
        }   

        [HttpPost("regitrar")]
        public async Task<ActionResult> Registrar(RegisterUserDTO registerUser)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser()
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GerarJwt(user.Email));
            }

            foreach (var item in result.Errors)
            {
                NotificarErro(item.Description);
            }
            
            return CustomResponse(registerUser);
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserDTO loginUser)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if(result.Succeeded)
            {
                return CustomResponse(await GerarJwt(loginUser.Email));
            }

            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuário ou senha incorreto");
            return CustomResponse(loginUser);
        }

        private async Task<LoginResponseViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf,ToUnixEpochDate(DateTime.UtcNow).ToString()));
            //Quando ele foi emitido
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat,ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach(var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSetting.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSetting.Emissor,
                Audience = _jwtSetting.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_jwtSetting.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            
            var encodedToken = tokenHandler.WriteToken(token);
            
            var response = new LoginResponseViewModel()
            {
              AccessToken = encodedToken,
              ExpiresIn = TimeSpan.FromHours(_jwtSetting.ExpiracaoHoras).TotalSeconds,
              User = new UserViewModel()
              {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new ClaimViewModel() { Type = c.Type, Value = c.Value })
              }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970,1,1,0,0,0, TimeSpan.Zero)).TotalSeconds);
    }
}