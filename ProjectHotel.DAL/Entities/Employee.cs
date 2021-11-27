using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectHotel.DAL.Entities
{
    public class Employee
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MinLength(6,ErrorMessage = "Минимальная длина Логина 6 символов!")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        public string Password { get; set; }

        public Guid EmployeeRoleID { get; set; }
        public virtual EmployeeRole Role { get; set; }

        public Employee()
        {
            this.ID = Guid.NewGuid();
        }

    }
}
