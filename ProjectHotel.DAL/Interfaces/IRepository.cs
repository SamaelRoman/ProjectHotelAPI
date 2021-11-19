using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHotel.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="Tid">Тип идентификатора, к примеру int или Guid</typeparam>
    public interface IRepository<TEntity,Tid> where TEntity : class
    {
        void Add(TEntity entity);
        TEntity Get(Tid ID);
        IEnumerable<TEntity> Get();
        void Edit(TEntity entity);
        void Delete(Tid ID);

    }
}
