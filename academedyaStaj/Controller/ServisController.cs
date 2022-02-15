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

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                string command = "Select AD,SOYAD from Users";

                List<idoResponse> adList = new List<idoResponse>();

                for (int i = 0; i < 2; i++)
                {
                    idoResponse resp = new idoResponse();
                    resp.ad = "";
                    resp.soyad = "";

                    adList.Add(resp);
                }

                Response r = new Response();
                r.message = "Veri başarıyla oluşturuldu.";
                r.status = "S";
                r.data = adList;

                return Ok(adList);
            }
            catch (Exception)
            {

                throw;
            }
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

        public class Response
        {
            public string message { get; set; }
            public List<idoResponse> data { get; set; }
            public string status { get; set; }
        }
    }
}


