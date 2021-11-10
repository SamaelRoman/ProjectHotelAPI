using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectHotel.BLL.DTO;
using ProjectHotel.BLL.Helpers;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ProjectHotel.BLL.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork DataBase;
        private IConfiguration Configuration;
        private IMapper mapper = new MapperConfiguration(cfg=> {
            cfg.CreateMap<EmployeeDTO, Employee>();
            cfg.CreateMap<Employee, EmployeeDTO>();

            cfg.CreateMap<EmployeeRoleDTO, EmployeeRole>();
            cfg.CreateMap<EmployeeRole, EmployeeRoleDTO>();
        }).CreateMapper();
        public AuthService(IUnitOfWork DataBase, IConfiguration Configuration)
        {
            this.DataBase = DataBase;
            this.Configuration = Configuration;
        }
        public object GetToken(string Login, string Pswd)
        {
            var BKey = Encoding.ASCII.GetBytes(Configuration.GetSection("SecurityJWTKey").Value);
            var identity = GetIdentity(Login, Pswd);
            var tokenHandler = new JwtSecurityTokenHandler();

            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(BKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var Token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(Token);
        }
        private ClaimsIdentity GetIdentity(string Login, string Pswd)
        {
            EmployeeDTO employee = mapper.Map<EmployeeDTO>(DataBase.Employees.Get()
                .FirstOrDefault(E => E.Login == Login && E.Password == HashPasword.CreateHashPassword(Pswd, Configuration.GetSection("PswdHashKey").Value)));
            if(employee == null)
            {
                throw new Exception("Invalid username or password.");
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,employee.Login),
                new Claim("RoleID",employee.EmployeeRoleID.ToString()),
                new Claim("ID",employee.ID.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
            return claimsIdentity;
        }
    }
}
