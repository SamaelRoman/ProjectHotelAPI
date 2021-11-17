using Microsoft.EntityFrameworkCore;
using ProjectHotel.DAL.EF;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.DAL.Repositories
{
    public class EmployeeRoleRepository : IEmployeeRoleRepository
    {
        private ContextDB contextDB;
        public EmployeeRoleRepository(ContextDB contextDB)
        {
            this.contextDB = contextDB;
        }
        public void Add(EmployeeRole entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                contextDB.EmployeeRoles.Add(entity);
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
                var CurrentEntity = contextDB.EmployeeRoles.Find(ID);
                if (CurrentEntity != null)
                {
                    contextDB.EmployeeRoles.Remove(CurrentEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(EmployeeRole entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                var CurrentEntity = contextDB.EmployeeRoles.Find(entity.ID);
                if (CurrentEntity != null)
                {
                    CurrentEntity.RoleName = entity.RoleName;
                    contextDB.EmployeeRoles.Update(CurrentEntity);
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

        public EmployeeRole Get(Guid ID)
        {
            if (ID == null)
            {
                throw new ArgumentNullException();
            }
            var ER = contextDB.EmployeeRoles.Find(ID);
            if(ER == null)
            {
                return null;
            }
            contextDB.Entry(ER).Collection(ER => ER.Employees).Load();
            return ER;
        }

        public IEnumerable<EmployeeRole> Get()
        {
            return contextDB.EmployeeRoles.Include(ER=>ER.Employees);
        }
    }
}
