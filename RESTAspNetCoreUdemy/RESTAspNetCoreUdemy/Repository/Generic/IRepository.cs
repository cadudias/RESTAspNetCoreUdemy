using RESTAspNetCoreUdemy.Model.Base;
using System.Collections.Generic;

namespace RESTAspNetCoreUdemy.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);

        T FindById(long id);

        List<T> FindAll();

        List<T> FindWithPagedSearch(string query);

        int GetCount(string query);

        T Update(T item);

        void Delete(long id);

        bool Exists(long? id);
    }
}