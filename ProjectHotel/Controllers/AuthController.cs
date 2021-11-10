using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotel.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("/Token")]
        public JsonResult Token([FromBody] EmplpoyeeGetTokenViewModel emplpoyee)
        {
            return new JsonResult(authService.GetToken(emplpoyee.Login, emplpoyee.Password));
        }
    }
}
