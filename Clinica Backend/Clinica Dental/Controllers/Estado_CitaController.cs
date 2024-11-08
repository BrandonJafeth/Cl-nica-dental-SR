using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Estado_CitaController : ControllerBase
    {
        // GET: api/<Estado_CitaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Estado_CitaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Estado_CitaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Estado_CitaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Estado_CitaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
