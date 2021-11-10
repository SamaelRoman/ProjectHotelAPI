using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.BLL.Interfaces
{
    public interface IAuthService
    {
        public object GetToken(string Login, string Pswd);
    }
}
