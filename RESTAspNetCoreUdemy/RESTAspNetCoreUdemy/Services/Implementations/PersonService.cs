using RESTAspNetCoreUdemy.Model;
using RESTAspNetCoreUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RESTAspNetCoreUdemy.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly MySQLContext _context;
        private volatile int count;

        public PersonService(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        public List<Person> FindAll()
        {
            return _context.Persons?.ToList();
        }

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

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public Person FindById(long id)
        {
            return _context.Persons.Find(id);
        }

        public Person Update(Person person)
        {
            if (!Exist(person.Id))
            {
                return new Person();
            }

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        private bool Exist(long? id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }

        public void Delete(long id)
        {
            var result = _context.Persons.Find(id);

            try
            {
                if (result != null)
                {
                    _context.Persons.Remove(result);

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}