using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Salon.DAL.Models;
using Salon.DAL.UnitOfWork;
using Salon.Common.Configuration;
namespace Salon.Common.JWT
{
    public class JWTHandler
    {
        private readonly RequestDelegate _next;
        private readonly JWTSetting _JWTSetting;
        public JWTHandler(RequestDelegate next, IOptions<JWTSetting> JWTSettings)
        {
            _next = next;
            _JWTSetting = JWTSettings.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            //var token = context.Request.Headers["Authorization"].FirstOrDefault();
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                {
                    var userId = ValidateToken_and_GetUserId(token);
                    var unitOfWork = new UnitOfWork();
                    context.Items["User"] = await unitOfWork.Login.LoginByUserId(userId);
                }
                await _next(context);
        }
        private long ValidateToken_and_GetUserId(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JWTSetting.SecretKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            var JwtSecurityToken = (JwtSecurityToken)validatedToken;
            UserConfig.UserId = long.Parse(JwtSecurityToken.Claims.First().Value);
            UserConfig.GroupId = int.Parse(JwtSecurityToken.Claims.ElementAt(1).Value);
            return long.Parse(JwtSecurityToken.Claims.First().Value);
        }
        public static string GenerateJSONWebToken(LoginDetailsModel User, IConfiguration _config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, User.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, User.GroupId.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, User.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_config["JWTSettings:Issuer"],
                    _config["JWTSettings:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMonths(1),
                    signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //------------------------- for help --------------------------------
        //private IConfiguration _config;
        //public JWTService(IConfiguration config)
        //{
        //    _config = config;
        //}
        //private IJsonSerializer _serializer = new JsonNetSerializer();
        //private IDateTimeProvider _provider = new UtcDateTimeProvider();
        //private IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
        //private IJwtAlgorithm _algorithm = new HMACSHA256Algorithm();

        //public DateTime GetExpiryTimestamp(string accessToken)
        //{
        //    IJwtValidator _validator = new JwtValidator(_serializer, _provider);
        //    IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
        //    var token = decoder.DecodeToObject<JwtToken>(accessToken);
        //    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(token.exp);
        //    return dateTimeOffset.LocalDateTime;
        //}
    }
}
