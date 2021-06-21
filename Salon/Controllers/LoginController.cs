using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Salon.DAL.Models;
using Salon.Common.JWT;
using Salon.DAL.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
namespace Salon.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(IConfiguration configuration, IUnitOfWork IUnitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = IUnitOfWork;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<LoginDetailsModel>> LoginByMobilePasswordAsync([FromQuery] LoginModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _unitOfWork.Login.LoginByMobilePassword(login);
            if (user == null)
                return Unauthorized();
            user.AccessToken = JWTHandler.GenerateJSONWebToken(user, _configuration);
            return user;
        }
        [AllowAnonymous]
        [HttpGet("loginbymobile")]
        public async Task<ActionResult<LoginDetailsModel>> LoginByMobileNoAsync([FromQuery] MobileNoModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _unitOfWork.Login.LoginByMobile(login);
            if (user == null)
                return Unauthorized();
            user.AccessToken = JWTHandler.GenerateJSONWebToken(user, _configuration);
            return user;
        }     
    }
}