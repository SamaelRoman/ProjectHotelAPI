using Microsoft.EntityFrameworkCore;
using ProjectHotel.DAL.EF;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.DAL.Repositories
{
    public class BookingInfosRepository : IBookingInfoRepository
    {
        private ContextDB contextDB;
        public BookingInfosRepository(ContextDB contextDB)
        {
            this.contextDB = contextDB;
        }
        public void Add(BookingInfo entity)
        {
            if (entity == null || entity.TotalPrice <= 0)
            {
                throw new ArgumentNullException();
            }
            try
            {
                contextDB.BookingInfos.Add(entity);
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
                var CurrentEntity = contextDB.BookingInfos.Find(ID);
                if (CurrentEntity != null)
                {
                    contextDB.BookingInfos.Remove(CurrentEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(BookingInfo entity)
        {
            if (entity == null || entity.TotalPrice <= 0)
            {
                throw new ArgumentNullException();
            }
            try
            {
                var CurrentEntity = contextDB.BookingInfos.Find(entity.ID);
                if (CurrentEntity != null)
                {
                    if (CurrentEntity.StartBooking > entity.StartBooking)
                    {
                        throw new Exception("Вы  не можете изменять дату начала бронирования на более раннюю дату!");
                    }
                    if (CurrentEntity.EndBooking < entity.EndBooking)
                    {
                        throw new Exception("Вы не можете изменять дату окончания бронирования на более позднюю дату!");
                    }

                    if (CurrentEntity.StartBooking < entity.StartBooking)
                    {
                        CurrentEntity.StartBooking = entity.StartBooking;
                    }
                    if (CurrentEntity.EndBooking > entity.EndBooking)
                    {
                        CurrentEntity.EndBooking = entity.EndBooking;
                    }
                    CurrentEntity.TotalPrice = entity.TotalPrice;
                    contextDB.BookingInfos.Update(CurrentEntity);
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

        public BookingInfo Get(Guid ID)
        {
            if (ID == null)
            {
                throw new ArgumentNullException();
            }
            BookingInfo bookingInfo = contextDB.BookingInfos.Find(ID);
            if(bookingInfo == null)
            {
                return null;
            }
            contextDB.Entry(bookingInfo).Navigation("Customer").Load();
            contextDB.Entry(bookingInfo).Navigation("Room").Load();
            contextDB.Entry(bookingInfo.Room).Navigation("Category").Load();
            contextDB.Entry(bookingInfo.Room.Category).Collection("CategoryInfos").Load();
            return bookingInfo;
        }

        public IEnumerable<BookingInfo> Get()
        {
            return contextDB.BookingInfos.Include(BI => BI.Customer).Include(BI => BI.Room).ThenInclude(R=>R.Category).ThenInclude(C=>C.CategoryInfos);
        }
    }
}
