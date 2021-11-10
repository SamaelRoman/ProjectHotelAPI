using ProjectHotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.BLL.Interfaces
{
    public interface IEmployeeRoleService
    {
        public void Add(EmployeeRoleDTO Role);
        public EmployeeRoleDTO Get(Guid ID);
        public IEnumerable<EmployeeRoleDTO> Get();
        public void Edit(EmployeeRoleDTO Role);
        public void Delete(Guid ID);
    }
}
