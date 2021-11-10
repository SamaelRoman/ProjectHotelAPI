using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectHotel.ViewModels
{
    public class EmployeeRoleViewModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
        public EmployeeRoleViewModel()
        {
            this.ID = Guid.NewGuid();
            Employees = new List<EmployeeViewModel>();
        }
    }
    public class EmployeeRoleCreateViewModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public EmployeeRoleCreateViewModel()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
