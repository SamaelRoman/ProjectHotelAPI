using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectHotel.ViewModels
{
    public class EmployeeViewModel
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
        
        [DataType(DataType.Password)]
        [MinLength(10,ErrorMessage = "Минимальная длина пароля 10 символов!")]
        public string Password { get; set; }
        public Guid EmployeeRoleID { get; set; }
        public EmployeeRoleViewModel Role { get; set; }
        public EmployeeViewModel()
        {
            this.ID = Guid.NewGuid();
        }
    }
    public class EmployeeRegistrationViewModel
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MinLength(6, ErrorMessage = "Минимальная длина Логина 6 символов!")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "Минимальная длина пароля 10 символов!")]

        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "Минимальная длина пароля 10 символов!")]

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public Guid EmployeeRoleID { get; set; }

        public EmployeeRegistrationViewModel()
        {
            this.ID = Guid.NewGuid();
        }
    }
    public class EmplpoyeeGetTokenViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(6, ErrorMessage = "Минимальная длина Логина 6 символов!")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(10, ErrorMessage = "Минимальная длина пароля 10 символов!")]
        public string Password { get; set; }
    }

}
