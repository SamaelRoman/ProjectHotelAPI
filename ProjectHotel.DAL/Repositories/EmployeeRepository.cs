using Microsoft.EntityFrameworkCore;
using ProjectHotel.DAL.EF;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private ContextDB contextDB;
        public EmployeeRepository(ContextDB contextDB)
        {
            this.contextDB = contextDB;
        }
        public void Add(Employee entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                contextDB.Employees.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Guid ID)
        {
            if (ID == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                var CurrentEntity = contextDB.Employees.Find(ID);
                if(CurrentEntity != null)
                {
                    contextDB.Employees.Remove(CurrentEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(Employee entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                var CurrentEntity = contextDB.Employees.Find(entity.ID);
                if (CurrentEntity != null)
                {
                    CurrentEntity.Login = entity.Login;
                    CurrentEntity.Password = entity.Password;
                    CurrentEntity.Email = entity.Email;
                    CurrentEntity.EmployeeRoleID = entity.EmployeeRoleID;
                    contextDB.Employees.Update(CurrentEntity);
                }
                else
                {
                    throw new Exception("Сущность с таким ID в базе данных не обнаружена!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Employee Get(Guid ID)
        {
            if(ID == null)
            {
                throw new ArgumentNullException();
            }
            Employee employee =  contextDB.Employees.Find(ID);
            if(employee == null)
            {
                return null;
            }
            contextDB.Entry(employee).Navigation("Role").Load();
            return employee;
        }

        public IEnumerable<Employee> Get()
        {
            return contextDB.Employees.Include(E=>E.Role);
        }
    }
}
