using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace academedyaStaj
{
    public class ServisController : ApiController
    {
        SqlConnectionControl Scc = new SqlConnectionControl();
        Response r = new Response();

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                string command = "Select firstname,lastname,email,username from Users";

                DataTable dt = new DataTable();
                Scc.getdataAdapter(command).Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    List<UsersModal> adList = new List<UsersModal>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        UsersModal user = new UsersModal();

                        user.ad = dr["firstname"].ToString();
                        user.soyad = dr["lastname"].ToString();
                        user.email = dr["email"].ToString();
                        user.kadi = dr["username"].ToString();
                        adList.Add(user);
                    }
                    r.message = "Veri başarıyla oluşturuldu.";
                    r.status = "S";
                    r.data = adList;
                    return Ok(r);
                }
                else
                {
                    r.message = "Gönderilecek veri bulunamadı.";
                    r.status = "W";
                    r.data = null;

                    return Content(HttpStatusCode.NoContent, r);
                }
            }
            catch (Exception ex)
            {
                r.message = "Hata oluştu : " + ex.Message;
                r.status = "E";
                r.data = null;

                return Content(HttpStatusCode.InternalServerError, r);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromBody]List<UsersModal> mdl)
        {
            try
            {
                string errorMessage = "";
                string command = "Select firstname,lastname,email,username from Users";
                DataTable dt = new DataTable();

                Scc.openDB(Scc._connection);
                Scc.getdataAdapter(command).Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    foreach (UsersModal x in mdl)
                    {
                        if (x.kadi == dr["username"].ToString())
                            errorMessage += x.kadi + " ismiyle kullanıcı bulunmaktadır. Farklı bir kullanıcı adı göndermeniz gerekmektedir.";
                    }
                }
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    r.message = "Hata oluştu :" + errorMessage;
                    r.status = "E";

                    return Content(HttpStatusCode.BadRequest, r);
                }
                else
                {
                    command = "INSERT INTO Users (firstname,lastname,email,username) VALUES (@firstname,@lastname,@email,@username)";
                    foreach (UsersModal item in mdl)
                    {
                        Scc.Add_Parameter("firstname", item.ad);
                        Scc.Add_Parameter("lastname", item.soyad);
                        Scc.Add_Parameter("email", item.email);
                        Scc.Add_Parameter("username", item.kadi);
                        Scc.basic(command);
                    }
                    r.message = "Kayıt Ekleme İşlemi Başarılı";
                    r.status = "S";
                    r.data = null;

                    return Ok(r);

                }
            }
            catch (Exception myExp)
            {
                r.message = "Hata oluştu : " + myExp.Message;
                r.status = "E";
                r.data = null;

                return Content(HttpStatusCode.InternalServerError, r);
            }
            finally
            {
                Scc.closeDB(Scc._connection);
            }
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
            public List<String> errorRows { get; set; }
        }
    }
}


