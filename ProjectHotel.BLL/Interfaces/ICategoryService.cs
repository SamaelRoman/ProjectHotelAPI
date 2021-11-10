using ProjectHotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHotel.BLL.Interfaces
{
    public interface ICategoryService
    {
        public void Add(CategoryDTO category);
        public IEnumerable<CategoryDTO> Get();
        public CategoryDTO Get(Guid ID);
        public void Edit(CategoryDTO category);
        public void Delete(Guid ID);
    }
}
