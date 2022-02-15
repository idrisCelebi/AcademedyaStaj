using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace academedyaStaj
{
    public class ServisController : ApiController
    {
        SqlConnectionControl Scc = new SqlConnectionControl();
        idoResponse ido = new idoResponse();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            string command = "Select * from Users";
            ido.Data= Scc.getList(command, "username");
            return ido.Data;
        }

        // GET api/<controller>/5
        [HttpGet]
        public string Get(int id)
        {
            
            return id.ToString();
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
           
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}


