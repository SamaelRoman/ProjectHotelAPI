using AutoMapper;
using ProjectHotel.BLL.DTO;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.BLL.Services
{
    public class EmployeeRoleService : IEmployeeRoleService
    {
        private IUnitOfWork DataBase;
        private IMapper mapper = new MapperConfiguration(cfg=>
        {
            cfg.CreateMap<EmployeeDTO, Employee>();
            cfg.CreateMap<Employee, EmployeeDTO>();
            cfg.CreateMap<EmployeeRoleDTO, EmployeeRole>();
            cfg.CreateMap<EmployeeRole, EmployeeRoleDTO>();
        }).CreateMapper();
        public EmployeeRoleService(IUnitOfWork DataBase)
        {
            this.DataBase = DataBase;
        }
        public void Add(EmployeeRoleDTO Role)
        {
            try
            {
                DataBase.EmployeeRoles.Add(mapper.Map<EmployeeRole>(Role));
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
                DataBase.EmployeeRoles.Delete(ID);
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Edit(EmployeeRoleDTO Role)
        {
            try
            {
                DataBase.EmployeeRoles.Edit(mapper.Map<EmployeeRole>(Role));
                DataBase.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EmployeeRoleDTO Get(Guid ID)
        {
            try
            {
                return mapper.Map<EmployeeRoleDTO>(DataBase.EmployeeRoles.Get(ID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EmployeeRoleDTO> Get()
        {
            try
            {
                return mapper.Map<IEnumerable<EmployeeRoleDTO>>(DataBase.EmployeeRoles.Get());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
