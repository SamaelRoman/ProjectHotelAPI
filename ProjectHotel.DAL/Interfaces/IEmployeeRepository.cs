using ProjectHotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.DAL.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
    }
}
