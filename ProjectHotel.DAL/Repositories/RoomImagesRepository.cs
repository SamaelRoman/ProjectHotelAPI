using Microsoft.EntityFrameworkCore;
using ProjectHotel.DAL.EF;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHotel.DAL.Repositories
{
    public class RoomImagesRepository : IRoomImageRepository
    {
        private ContextDB contextDB;
        public RoomImagesRepository(ContextDB contextDB)
        {
            this.contextDB = contextDB;
        }
        public void Add(RoomImage entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                contextDB.RoomImages.Add(entity);
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
                var CurrentEntity = contextDB.RoomImages.Find(ID);
                if (CurrentEntity != null)
                {
                    contextDB.RoomImages.Remove(CurrentEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(RoomImage entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                var CurrentEntity = contextDB.RoomImages.Find(entity.ID);
                if (CurrentEntity != null)
                {

                    CurrentEntity.ImgUrl = entity.ImgUrl;
                    contextDB.RoomImages.Update(CurrentEntity);
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

        public RoomImage Get(Guid ID)
        {
            if (ID == null)
            {
                throw new ArgumentNullException();
            }
            RoomImage roomImage = contextDB.RoomImages.Find(ID);
            contextDB.Entry(roomImage).Navigation("Room").Load();
            return roomImage;
        }

        public IEnumerable<RoomImage> Get()
        {
            return contextDB.RoomImages.Include(RI => RI.Room);
            //List<RoomImage> roomImages = await contextDB.RoomImages.ToListAsync();
            //if(roomImages != null || roomImages.Count != 0)
            //{
            //    foreach (var RI in roomImages)
            //    {
            //        contextDB.Entry(RI).Navigation("Room").Load();
            //    }
            //}
            //return roomImages;
        }
    }
}
