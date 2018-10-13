using RESTAspNetCoreUdemy.Data.Converter;
using RESTAspNetCoreUdemy.Data.VO;
using RESTAspNetCoreUdemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RESTAspNetCoreUdemy.Data.Converters
{
    public class PersonConverter : IParser<Person, PersonVO>, IParser<PersonVO, Person>
    {
        public PersonVO Parse(Person origin)
        {
            if (origin == null)
            {
                return new PersonVO();
            }

            return new PersonVO()
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public Person Parse(PersonVO origin)
        {
            if (origin == null)
            {
                return new Person();
            }

            return new Person()
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<PersonVO> ParseList(List<Person> originList)
        {
            if (originList == null)
            {
                new List<PersonVO>();
            }

            return originList.Select(item => Parse(item)).ToList();
        }

        public List<Person> ParseList(List<PersonVO> originList)
        {
            if (originList == null)
            {
                new List<Person>();
            }

            return originList.Select(item => Parse(item)).ToList();
        }
    }
}