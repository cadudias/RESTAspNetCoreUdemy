using RESTAspNetCoreUdemy.Data.Converters;
using RESTAspNetCoreUdemy.Data.VO;
using RESTAspNetCoreUdemy.Model;
using RESTAspNetCoreUdemy.Repository;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RESTAspNetCoreUdemy.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly PersonConverter _converter;

        private IPersonRepository _repository;

        // sai o contexto daqui e entra o repositorio
        //private readonly MySQLContext _context;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            // regras de negocio ficam aqui na camada de business

            // aqui convertemos pra salvar e depois convertemos pra retornar pro controller
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            // offset começa na posição 0, temos que decrementar a pagina passada por parametro
            page = page > 0 ? page - 1 : 0;

            // esse 1 = 1 é só pra não quebrar o where se caso abaixo não venha o name
            // não é nada intuitivo
            string query = @"SELECT * FROM persons p WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(name))
            {
                query += $" and p.FirstName like '%{name}%'";
            }
            // limit {pageSize} offset {page} = paginação
            query += $" order by p.FirstName {sortDirection} limit {pageSize} offset {page}";

            string countQuery = @"SELECT Count(*) from persons p WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(name))
            {
                countQuery += $" and p.FirstName like '%{name}%'";
            }

            var persons = _converter.ParseList(_repository.FindWithPagedSearch(query));

            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonVO>()
            {
                CurrentPage = page + 1, // tem que incrementar por causa do offset do banco
                List = persons,
                PageSize = page,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
        }

        public List<Person> FindByName(string firstname, string lastName)
        {
            return _repository.FindByName(firstname, lastName);
        }
    }
}