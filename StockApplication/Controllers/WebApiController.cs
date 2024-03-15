using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StockApplication.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WebApiController : ControllerBase
    {
        [HttpGet]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            var client = new HttpClient();

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "https://localhost:44343/connect/token",
                //ClientId = "ProCodeGuide",
                //ClientSecret = "client_secret",
                //UserName = username,
                //Password = password,
                //Scope = "weatherApi.read"
                UserName = username,
                Password = password,
            });

            if (tokenResponse.IsError)
            {
                return BadRequest(tokenResponse.Error);
            }

            return Ok(tokenResponse.AccessToken);
        }
    }
}
