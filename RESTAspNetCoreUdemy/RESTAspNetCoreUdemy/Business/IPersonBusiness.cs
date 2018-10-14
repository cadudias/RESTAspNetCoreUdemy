using RESTAspNetCoreUdemy.Data.VO;
using RESTAspNetCoreUdemy.Model;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RESTAspNetCoreUdemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindAll();

        List<Person> FindByName(string firstname, string lastName);

        PersonVO Update(PersonVO person);

        PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

        void Delete(long id);
    }
}