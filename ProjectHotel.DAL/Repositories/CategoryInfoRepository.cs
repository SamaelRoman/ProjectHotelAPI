using Microsoft.EntityFrameworkCore;
using ProjectHotel.DAL.EF;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.DAL.Repositories
{
    public class CategoryInfoRepository : ICategoryInfoRepository
    {
        private ContextDB contextDB;
        public CategoryInfoRepository(ContextDB contextDB)
        {
            this.contextDB = contextDB;
        }
        public void Add(CategoryInfo entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                contextDB.CategoryInfos.Add(entity);
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
                var CurrentEntity = contextDB.CategoryInfos.Find(ID);
                if (CurrentEntity != null)
                {
                    contextDB.CategoryInfos.Remove(CurrentEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(CategoryInfo entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                var CurrentEntity = contextDB.CategoryInfos.Find(entity.ID);
                if (CurrentEntity != null)
                {
                    CurrentEntity.Price = CurrentEntity.Price;
                    contextDB.CategoryInfos.Update(CurrentEntity);
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

        public CategoryInfo Get(Guid ID)
        {
            if (ID == null)
            {
                throw new ArgumentNullException();
            }
            CategoryInfo categoryInfo = contextDB.CategoryInfos.Find(ID);
            if(categoryInfo == null)
            {
                return null;
            }
            contextDB.Entry(categoryInfo).Navigation("Category").Load();
            return categoryInfo;
        }

        public IEnumerable<CategoryInfo> Get()
        {
            return contextDB.CategoryInfos.Include(CI => CI.Category);
        }
    }
}
