
using HRMS.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace HRMS.Areas.Admin 
{
    public class UserLogInController : BaseApiController
    {
        // GET: api/<UserLogInController>
        
        // GET api/<UserLogInController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserLogInController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserLogInController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserLogInController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
