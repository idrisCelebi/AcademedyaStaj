using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace academedyaStaj
{
    public partial class registerPage : System.Web.UI.Page
    {
      
        SqlConnection conn = new SqlConnection(@"data source=DESKTOP-AR7QPFE\CENGIZHAN;initial catalog=AcademedyaStajMain;integrated security=True");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void registerbutton_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {

                try
                {
                    int newidto_use = newidgenerator("Users", conn);
                    conn.Open();
                    SqlCommand adduser = new SqlCommand("insert into Users (id,firstname,lastname,email,username,password,activation) values(@id,@firstname,@lastname,@email,@username,@password,@activation)", conn);
                    adduser.Parameters.AddWithValue("@id", newidto_use);
                    adduser.Parameters.AddWithValue("@firstname", firstname.Text);
                    adduser.Parameters.AddWithValue("@lastname", lastname.Text);
                    adduser.Parameters.AddWithValue("@email", email.Text);
                    adduser.Parameters.AddWithValue("@username", username.Text);
                    adduser.Parameters.AddWithValue("@password", password.Text);
                    adduser.Parameters.AddWithValue("@activation", 0);
                    int ithappen = adduser.ExecuteNonQuery();
                    conn.Close();
                    if (ithappen>0)
                    {
                        inforegister.Text = "Kayıt işlemi yapıldı. Mail adresinize gelen doğrulama linkine tıkladığınızda üyeliğiniz aktif olacaktır.";
                        inforegister.ForeColor = System.Drawing.Color.Green;
                        sendmail(email.Text,newidto_use);

                    }
                    else
                    {
                        inforegister.Text = "Hata, kayıt yapılamadı!";
                        inforegister.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch
                {

                }
            }

        }
        public int newidgenerator(String tableName, SqlConnection conname)
        {
            conname.Open();
            Random r = new Random();
            int yenidsayi = 0;

            bool eslesme = false;
            do
            {
                StringBuilder query = new StringBuilder();
                query.AppendFormat("select count(1) from {0} where id = @yenid", tableName);
                yenidsayi = r.Next();
                SqlCommand yeniduret = new SqlCommand(query.ToString(), conname);
                yeniduret.Parameters.AddWithValue("@yenid", yenidsayi);
                int count = Convert.ToInt32(yeniduret.ExecuteScalar());
                if (count == 1)
                {
                    eslesme = true;
                }
                else
                {
                    eslesme = false;

                }
            }
            while (eslesme == true);
            conname.Close();
            return yenidsayi;

        }
        public void sendmail(String target,int whoid)
        {
            string mail = "idriscengizhancelebi@gmail.com";
            string sifre = "IDO18.rido";
            string yol = "https://localhost:44356/Activation.aspx?activationcode="+whoid;
            Random rnd = new Random();
            
            try
            {

                MailMessage mesaj = new MailMessage(mail, target, "Onay Kodu","Sayın "+firstname.Text+" "+lastname.Text+"Üyeliğinizi aktif etmek için lütfen linke tıklayınız."+yol);
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(mail, sifre);
                smtp.EnableSsl = true;
               
                smtp.Send(mesaj);
               
            }
            catch (Exception ex)
            {
                new Exception("Onay Kodu Gönderiminde Hata" + ex.Message);
            }
           
        }
        protected void usernamecustomvalidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            
            conn.Open();
        
            SqlCommand sqlCmd = new SqlCommand("Select Count(1) from Users Where username=@username", conn);

            sqlCmd.Parameters.AddWithValue("@username", args.Value);
           
            int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            conn.Close();
            if (count == 1)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}