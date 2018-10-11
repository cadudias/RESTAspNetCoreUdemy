using RESTAspNetCoreUdemy.Model;
using RESTAspNetCoreUdemy.Repository;
using System.Collections.Generic;
using System.Threading;

namespace RESTAspNetCoreUdemy.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        // sai o contexto daqui e entra o repositorio
        //private readonly MySQLContext _context;
        private volatile int count;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
        }

        public Person Create(Person person)
        {
            // regras de negocio ficam aqui na camada de business

            return _repository.Create(person);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        #region Mocks

        //public List<Person> FindAll()
        //{
        //    List<Person> persons = new List<Person>();
        //    for (int i = 0; i < 8; i++)
        //    {
        //        Person person = MockPerson(i);
        //        persons.Add(person);
        //    }
        //    return persons;
        //}

        //private Person MockPerson(int id)
        //{
        //    return new Person()
        //    {
        //        Id = IncrementAndGet(),
        //        FirstName = "Person Name " + id,
        //        LastName = "Person LastName " + id,
        //        Address = "Avendia xyz, 345",
        //        Gender = "Masculino"
        //    };
        //}

        #endregion Mocks

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}