using RESTAspNetCoreUdemy.Data.Converters;
using RESTAspNetCoreUdemy.Data.VO;
using RESTAspNetCoreUdemy.Model;
using RESTAspNetCoreUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RESTAspNetCoreUdemy.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly PersonConverter _converter;

        private IRepository<Person> _repository;

        // sai o contexto daqui e entra o repositorio
        //private readonly MySQLContext _context;

        public PersonBusiness(IRepository<Person> repository)
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
    }
}