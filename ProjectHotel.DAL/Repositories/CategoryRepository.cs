using Microsoft.EntityFrameworkCore;
using ProjectHotel.DAL.EF;
using ProjectHotel.DAL.Entities;
using ProjectHotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHotel.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ContextDB contextDB;
        public CategoryRepository(ContextDB contextDB)
        {
            this.contextDB = contextDB;
        }
        public void Add(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                contextDB.Categories.Add(entity);
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
                var CurrentEntity = contextDB.Categories.Find(ID);
                if (CurrentEntity != null)
                {
                    contextDB.Categories.Remove(CurrentEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                var CurrentEntity = contextDB.Categories.Find(entity.ID);
                
                if (CurrentEntity != null)
                {
                    CurrentEntity.Title = entity.Title;
                    CurrentEntity.Capacity = entity.Capacity;
                    contextDB.Entry(CurrentEntity).Collection("CategoryInfos").Load();
                    if (entity.CategoryInfos.Count != 0)
                    {
                        if (CurrentEntity.CategoryInfos.First(CI => CI.PriceAtTheMomentEnd == null).Price != entity?.CategoryInfos?.Last()?.Price)
                        {
                            //CurrentEntity.CategoryInfos.First(CI => CI.PriceAtTheMomentEnd == null).PriceAtTheMomentEnd = DateTime.Now;

                            var AddedCategoryInfo = entity.CategoryInfos.Last();

                            var CurrentCategoryInfo = contextDB.CategoryInfos.Find(CurrentEntity.CategoryInfos.First(CI => CI.PriceAtTheMomentEnd == null).ID);
                            CurrentCategoryInfo.PriceAtTheMomentEnd = DateTime.Now;
                            contextDB.CategoryInfos.Update(CurrentCategoryInfo);

                            AddedCategoryInfo.CategoryID = CurrentEntity.ID;
                            contextDB.CategoryInfos.Add(AddedCategoryInfo);
                        }
                    }
                    contextDB.Categories.Update(CurrentEntity);
                    
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
        public Category Get(Guid ID)
        {
            if (ID == null)
            {
                throw new ArgumentNullException();
            }
            Category category = contextDB.Categories.Find(ID);
            if(category == null)
            {
                return null;
            }
            contextDB.Entry(category).Navigation("Rooms").Load();
            foreach(var R in category.Rooms)
            {
                contextDB.Entry(R).Collection("RoomImages").Load();
            }
            contextDB.Entry(category).Navigation("CategoryInfos").Load();
            return category;
        }

        public IEnumerable<Category> Get()
        {
            return contextDB.Categories.Include(C => C.CategoryInfos).Include(C => C.Rooms).ThenInclude(R=>R.RoomImages);
            //List<Category> categories = await contextDB.Categories.ToListAsync();

            //if(categories != null || categories.Count != 0)
            //{
            //    foreach (var C in categories)
            //    {
            //        contextDB.Entry(C).Navigation("Rooms").Load();
            //        foreach (var R in C.Rooms)
            //        {
            //            contextDB.Entry(R).Collection("RoomImages").Load();
            //        }
            //        contextDB.Entry(C).Navigation("CategoryInfos").Load();
            //    }
            //}
            //return categories;
        }
    }
}
