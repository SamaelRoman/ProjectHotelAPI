using Microsoft.EntityFrameworkCore;
using ProjectHotel.DAL.EF;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.DAL.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private ContextDB contextDB;
        public RoomRepository(ContextDB contextDB)
        {
            this.contextDB = contextDB;
        }
        public void Add(Room entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                contextDB.Rooms.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(string ID)
        {
            try
            {
                var CurrentEntity = contextDB.Rooms.Find(ID);
                if (CurrentEntity != null)
                {
                    contextDB.Rooms.Remove(CurrentEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(Room entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                var CurrentEntity = contextDB.Rooms.Find(entity.ID);
                if (CurrentEntity != null)
                {
                    if(entity.RoomImages != null || entity.RoomImages.Count != 0)
                    {
                        CurrentEntity.RoomImages = entity.RoomImages;
                    }
                    if(CurrentEntity.CategoryID != entity.CategoryID && entity.CategoryID != null)
                    {
                        CurrentEntity.CategoryID = entity.CategoryID;
                    }
                    contextDB.Rooms.Update(CurrentEntity);
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

        public Room Get(string ID)
        {
            var room = contextDB.Rooms.Find(ID);
            contextDB.Entry(room).Navigation("RoomImages").Load();
            contextDB.Entry(room).Navigation("Category").Load();
            contextDB.Entry(room.Category).Collection("CategoryInfos").Load();
            contextDB.Entry(room).Navigation("BookingInfos").Load();
            return room;
        }

        public IEnumerable<Room> Get()
        {
            return contextDB.Rooms.Include(R => R.RoomImages).Include(R=>R.Category).ThenInclude(C=>C.CategoryInfos).Include(R => R.BookingInfos);
        }
    }
}
