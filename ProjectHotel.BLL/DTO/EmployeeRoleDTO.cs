using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectHotel.BLL.DTO
{
    public class EmployeeRoleDTO
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public IEnumerable<EmployeeDTO> Employees { get; set; }
        public EmployeeRoleDTO()
        {
            this.ID = Guid.NewGuid();
            Employees = new List<EmployeeDTO>();
        }
    }
}
