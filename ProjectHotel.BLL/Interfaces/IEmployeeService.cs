using ProjectHotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.BLL.Interfaces
{
    public interface IEmployeeService
    {
        public void Add(EmployeeDTO employee);
        public EmployeeDTO Get(Guid ID);
        public IEnumerable<EmployeeDTO> Get();
        public void Edit(EmployeeDTO employee);
        public void Delete(Guid ID);

    }
}
