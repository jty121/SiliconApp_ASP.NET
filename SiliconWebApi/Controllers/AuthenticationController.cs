
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using SiliconWebApi.Attributes;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;

namespace SiliconWebApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[UseApiKey]  // bara dom som har tillgång till en api nyckel ska få acccess till token nyckeln
    //public class AuthenticationController(IConfiguration configuration) : ControllerBase
    //{

        //    private readonly IConfiguration _configuration = configuration;


        //    [HttpPost]  
        //    public IActionResult GetToken()
        //    {
        //        try
        //        {
        //            var tokenHandler = new JwtSecurityTokenHandler();
        //            var secret = Encoding.UTF8.GetBytes(_configuration["Token:Secret"]!);


        //            //vilka värden ska token nyckeln ha?
        //            var tokenDescriptor = new SecurityTokenDescriptor
        //            {
        //                Issuer = _configuration["Token:Issuer"],
        //                Audience = _configuration["Token:Audience"],
        //                Expires = DateTime.Now.AddMinutes(15),
        //                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha512Signature)
        //            };

        //            var token = tokenHandler.CreateToken(tokenDescriptor);
        //            //omvandla till textsträng
        //            return Ok(tokenHandler.WriteToken(token));
        //        }
        //        catch { }
        //        return Unauthorized();
     
    //}
}
