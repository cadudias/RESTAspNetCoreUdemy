using RESTAspNetCoreUdemy.Model;
using RESTAspNetCoreUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RESTAspNetCoreUdemy.Business.Implementations
{
    public class BookBusiness : IBookBusiness
    {
        private IRepository<Book> _repository;

        // sai o contexto daqui e entra o repositorio
        //private readonly MySQLContext _context;
        private readonly int count;

        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book Create(Book book)
        {
            // regras de negocio ficam aqui na camada de business

            return _repository.Create(book);
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}