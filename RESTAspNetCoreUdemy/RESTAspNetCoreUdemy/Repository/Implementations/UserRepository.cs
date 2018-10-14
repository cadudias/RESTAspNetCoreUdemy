using RESTAspNetCoreUdemy.Model;
using RESTAspNetCoreUdemy.Model.Context;
using System.Linq;

namespace RESTAspNetCoreUdemy.Business.Implementattions
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User FindByLogin(string login)
        {
            return _context.Users.SingleOrDefault(u => u.Login.Equals(login));
        }
    }
}