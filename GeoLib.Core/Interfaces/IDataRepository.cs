using System.Collections.Generic;

namespace GeoLib.Core.Interfaces
{
    public interface IDataRepository
    {
    }

    public interface IDataRepository<T> : IDataRepository
        where T : class, IIdentifiableEntity, new()
    {
        T Add(T entity);
        void Remove(T entity);
        void Remove(int id);
        T Update(T entity);
        List<T> Get();
        T Get(int id);
    }
}
