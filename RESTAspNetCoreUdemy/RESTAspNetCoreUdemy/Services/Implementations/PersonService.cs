using RESTAspNetCoreUdemy.Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RESTAspNetCoreUdemy.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        private Person MockPerson(int id)
        {
            return new Person()
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name " + id,
                LastName = "Person LastName " + id,
                Address = "Avendia xyz, 345",
                Gender = "Masculino"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public Person FindById(long id)
        {
            return new Person()
            {
                Id = 1,
                FirstName = "Ricardo",
                LastName = "Dias",
                Address = "Avendia xyz, 345",
                Gender = "Masculino"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
    }
}