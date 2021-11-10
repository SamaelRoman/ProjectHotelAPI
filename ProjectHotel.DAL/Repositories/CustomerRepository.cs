using Microsoft.EntityFrameworkCore;
using ProjectHotel.DAL.EF;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectHotel.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private ContextDB contextDB;
        public CustomerRepository(ContextDB contextDB)
        {
            this.contextDB = contextDB;
        }
        public void Add(Customer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                contextDB.Customers.Add(entity);
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
                var CurrentEntity = contextDB.Customers.Find(ID);
                if (CurrentEntity != null)
                {
                    contextDB.Customers.Remove(CurrentEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(Customer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                var CurrentEntity = contextDB.Customers.FirstOrDefault(C=>C.ID == entity.ID && C.PassportID == entity.PassportID);
                if (CurrentEntity != null)
                {
                    CurrentEntity.FirstName = entity.FirstName ?? CurrentEntity.FirstName;
                    CurrentEntity.LastName = entity.LastName ?? CurrentEntity.LastName;
                    CurrentEntity.Patronymic = entity.Patronymic ?? CurrentEntity.Patronymic;
                    if(CurrentEntity.PhoneNumber != entity.PhoneNumber)
                    {
                        CurrentEntity.PhoneNumber = entity.PhoneNumber;
                    }

                    contextDB.Customers.Update(CurrentEntity);
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

        public Customer Get(Guid ID)
        {
            if (ID == null)
            {
                throw new ArgumentNullException();
            }
            Customer customer = contextDB.Customers.Find(ID);
            contextDB.Entry(customer).Navigation("BookingInfos").Load();
            foreach(var BI in customer.BookingInfos)
            {
                contextDB.Entry(BI).Navigation("Room").Load();
                contextDB.Entry(BI.Room).Navigation("Category").Load();
                contextDB.Entry(BI.Room.Category).Collection("CategoryInfo").Load();
            }
           
            return customer;
        }

        public IEnumerable<Customer> Get()
        {
            return contextDB.Customers.Include(C => C.BookingInfos).ThenInclude(BI=>BI.Room).ThenInclude(R=>R.Category).ThenInclude(C=>C.CategoryInfos);
        }
    }
}
