using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Historial_TratamientoController : ControllerBase
    {
        // GET: api/<Historial_TratamientoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Historial_TratamientoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Historial_TratamientoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Historial_TratamientoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Historial_TratamientoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
