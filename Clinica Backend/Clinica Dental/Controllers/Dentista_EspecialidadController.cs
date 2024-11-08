using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dentista_EspecialidadController : ControllerBase
    {
        // GET: api/<Dentista_EspecialidadController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Dentista_EspecialidadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Dentista_EspecialidadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Dentista_EspecialidadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Dentista_EspecialidadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
