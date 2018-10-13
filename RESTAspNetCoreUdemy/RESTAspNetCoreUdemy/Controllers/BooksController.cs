using Microsoft.AspNetCore.Mvc;
using RESTAspNetCoreUdemy.Business;
using RESTAspNetCoreUdemy.Model;

namespace RESTAspNetCoreUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public IBookBusiness _bookBusiness;

        public BooksController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _bookBusiness.FindById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // POST: api/Person
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            return new ObjectResult(_bookBusiness.Create(book));
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            var updatedPerson = _bookBusiness.Update(book);
            if (updatedPerson == null)
            {
                return NoContent();
            }

            return new ObjectResult(_bookBusiness.Update(book));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}