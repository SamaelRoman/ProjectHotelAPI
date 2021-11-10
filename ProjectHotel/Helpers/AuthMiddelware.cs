using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectHotel.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHotel.Helpers
{
    public class AuthMiddelware
    {
        public RequestDelegate Next { get; set; }
        private string Key;
        public AuthMiddelware(RequestDelegate Next,IConfiguration configuration)
        {
            this.Next = Next;
            this.Key = configuration.GetSection("SecurityJWTKey").Value;
           
        }

        public async Task Invoke(HttpContext context, IEmployeeService employeeService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                AttachUserToContext(context, employeeService, token);
            }
            await Next.Invoke(context);
        }
        private void AttachUserToContext(HttpContext context, IEmployeeService employeeService, string token)
        {
            try
            {
                var TokenHandler = new JwtSecurityTokenHandler();
                var Bkey = Encoding.UTF8.GetBytes(Key);
                TokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Bkey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);
                var JWTToken = (JwtSecurityToken)validatedToken;
                Guid EmployeeID = new Guid(JWTToken.Claims.First(C => C.Type == "ID").Value);
                context.Items["Employee"] = employeeService.Get(EmployeeID);
            }
            catch
            {

            }
        }
    }
}
