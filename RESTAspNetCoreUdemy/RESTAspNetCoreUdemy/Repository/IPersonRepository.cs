using RESTAspNetCoreUdemy.Model;
using RESTAspNetCoreUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RESTAspNetCoreUdemy.Repository
{
    /**
     * Foi criada uma Interface nova já que FindByName é um metodo especifico de pessoa
     **/

    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> FindByName(string firstname, string lastName);
    }
}