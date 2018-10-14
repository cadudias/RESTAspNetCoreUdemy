using RESTAspNetCoreUdemy.Model;

namespace RESTAspNetCoreUdemy.Business
{
    public interface ILoginBusiness
    {
        object FindByLogin(UserVO user);
    }
}