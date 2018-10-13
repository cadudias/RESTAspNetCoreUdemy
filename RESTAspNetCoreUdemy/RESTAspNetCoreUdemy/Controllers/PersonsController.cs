using Microsoft.AspNetCore.Mvc;
using RESTAspNetCoreUdemy.Business;
using RESTAspNetCoreUdemy.Data.VO;

namespace RESTAspNetCoreUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    //[Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        public IPersonBusiness _personBusiness;

        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // POST: api/Person
        [HttpPost]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            return new ObjectResult(_personBusiness.Create(person));
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PersonVO person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            var updatedPerson = _personBusiness.Update(person);
            if (updatedPerson == null)
            {
                return NoContent();
            }

            return new ObjectResult(_personBusiness.Update(person));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}