using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectHotel.DAL.Entities
{
    public class EmployeeRole
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public virtual IEnumerable<Employee> Employees { get; set; }
        public EmployeeRole()
        {
            this.ID = Guid.NewGuid();
            Employees = new List<Employee>();
        }
    }
}
