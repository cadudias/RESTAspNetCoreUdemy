using RESTAspNetCoreUdemy.Model;

namespace RESTAspNetCoreUdemy.Business
{
    public interface IUserRepository
    {
        User FindByLogin(string login);
    }
}