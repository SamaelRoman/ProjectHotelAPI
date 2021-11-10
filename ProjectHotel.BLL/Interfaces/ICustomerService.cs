using ProjectHotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.BLL.Interfaces
{
    public interface ICustomerService
    {
        public void Add(CustomerDTO customer);
        public IEnumerable<CustomerDTO> Get();
        public CustomerDTO Get(Guid ID);
        public void Edit(CustomerDTO customer);
        public void Delete(Guid ID);
        public CustomerDTO GetByPassportID(string PassportID);
    }
}
