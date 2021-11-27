using AutoMapper;
using Microsoft.Extensions.Configuration;
using ProjectHotel.BLL.DTO;
using ProjectHotel.BLL.Helpers;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork DataBase;
        private IConfiguration Configuration;
        private IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EmployeeDTO, Employee>();
            cfg.CreateMap<Employee, EmployeeDTO>();
            cfg.CreateMap<EmployeeRoleDTO, EmployeeRole>();
            cfg.CreateMap<EmployeeRole, EmployeeRoleDTO>();
        }).CreateMapper();
        public EmployeeService(IUnitOfWork DataBase,IConfiguration Configuration)
        {
            this.DataBase = DataBase;
            this.Configuration = Configuration;
        }
        public void Add(EmployeeDTO employee)
        {
            try
            {
                employee.Password = HashPasword.CreateHashPassword(employee.Password, Configuration.GetSection("PswdHashKey").Value);

                DataBase.Employees.Add(mapper.Map<Employee>(employee));
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Delete(Guid ID)
        {
            try
            {
                DataBase.Employees.Delete(ID);
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }

        public void Edit(EmployeeDTO employee)
        {
            try
            {
                //employee.Password = HashPasword.CreateHashPassword(employee.Password, Configuration.GetSection("PswdHashKey").Value);
                DataBase.Employees.Edit(mapper.Map<Employee>(employee));
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EmployeeDTO Get(Guid ID)
        {
            try
            {
                return mapper.Map<EmployeeDTO>(DataBase.Employees.Get(ID));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EmployeeDTO> Get()
        {
            try
            {
                return mapper.Map<IEnumerable<EmployeeDTO>>(DataBase.Employees.Get());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
