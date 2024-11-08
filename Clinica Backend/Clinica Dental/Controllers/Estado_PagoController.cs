using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Estado_PagoController : ControllerBase
    {
        // GET: api/<Estado_PagoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Estado_PagoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Estado_PagoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Estado_PagoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Estado_PagoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
