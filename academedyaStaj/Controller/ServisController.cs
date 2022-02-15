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
        

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                string command = "Select firstname,lastname,email,username from Users where activation=0";

                List<UsersModal> adList = new List<UsersModal>();
                List<String> ad = new List<string>();
                List<String> soyad = new List<string>();
                List<String> email = new List<string>();
                List<String> kadi = new List<string>();

                ad=Scc.getList(command, "firstname");
                soyad=Scc.getList(command, "lastname");
                email=Scc.getList(command, "email");
                kadi =Scc.getList(command, "username");
                for (int i = 0; i < ad.Count; i++)
                {
                    UsersModal resp = new UsersModal();
                    resp.ad = ad[i];
                    resp.soyad = soyad[i];
                    resp.email = email[i];
                    resp.kadi = kadi[i];


                    adList.Add(resp);
                }

                Response r = new Response();
                r.message = "Veri başarıyla oluşturuldu.";
                r.status = "S";
                r.data = adList;

                return Ok(r);
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
            public List<UsersModal> data { get; set; }
            public string status { get; set; }
        }
    }
}


