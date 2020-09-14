using System;
using System.Collections.Generic;

namespace HungryPizza.Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T obj);

        T GetById(Guid id);

        IEnumerable<T> GetAll();

        void Update(T obj);

        void Delete(T obj);

        void DeleteById(Guid id);
    }
}
