using RESTAspNetCoreUdemy.Model;
using RESTAspNetCoreUdemy.Model.Context;
using RESTAspNetCoreUdemy.Repository.Generic;
using System.Collections.Generic;
using System.Linq;

namespace RESTAspNetCoreUdemy.Repository.Implementations
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context)
        {
        }

        public List<Person> FindByName(string firstname, string lastName)
        {
            if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(q => q.FirstName.Contains(firstname) && q.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrEmpty(firstname) && string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(q => q.FirstName.Contains(firstname)).ToList();
            }
            else if (string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(q => q.LastName.Contains(lastName)).ToList();
            }
            else
            {
                return _context.Persons.ToList();
            }
        }
    }
}