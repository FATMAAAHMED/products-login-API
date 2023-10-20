using AutoMapper;
using Context;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductDomain.Entities;
using ProductDTOS;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace productsApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DContext context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(DContext _context, IConfiguration configuration,
            UserManager<User> userManager, SignInManager<User> signInManager,
            IMapper mapper)
        {
            context = _context;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result
                   = await _signInManager.PasswordSignInAsync
                      (loginDTO.Email, loginDTO.Password, false, false);
            var user1 = await _userManager.FindByNameAsync(loginDTO.Email);
            if (user1 is null || !await _userManager.CheckPasswordAsync(user1, loginDTO.Password))
            {
                return BadRequest("Invalid UserName Or Password .");
            }
            else if (result.IsNotAllowed == true)
            {
                return BadRequest("Invalid UserName Or Password .");

            }
            else if (result.IsLockedOut)
            {
                return BadRequest("user is Locked Out .");
            }
            else
            {
                var token = GenerateJwtToken(loginDTO.Email);
                return Ok(new { token });
            }

            return Unauthorized();
        }


        //registeration

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDTO loginDTO)
        {
           var user = context.Users
                .FirstOrDefaultAsync(user => user.UserName== loginDTO.Email
                || user.Email==loginDTO.Email).Result;

            if (user!=null)
                {
                    return BadRequest("user is exist");
                }
            else
            {
                var myUser = _mapper.Map<User>(loginDTO);
                myUser.UserName = myUser.Email;
                myUser.LastLoginTime = DateTime.Now.ToString();
                var result = await _userManager.CreateAsync(myUser, loginDTO.Password);

                await context.SaveChangesAsync();
                    
                var token = GenerateJwtToken(loginDTO.Email);
                return Ok(new { token });
            }

            

        }
       
        
        ///jwt
        private string GenerateJwtToken(string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]??"kit"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: new[] { new Claim(ClaimTypes.Name, email) },
                expires: expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
