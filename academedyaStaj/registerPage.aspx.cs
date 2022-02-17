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

        SqlConnectionControl Scc = new SqlConnectionControl();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void registerbutton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    int newidto_use = Scc.newidgenerator("Users");
                    String insertCommand = "insert into Users (id,firstname,lastname,email,username,password,activation,admin)" +
                     " values(" + newidto_use + ",'" + firstname.Text + "','" + lastname.Text + "','" + email.Text + "','" + username.Text + "','" + password.Text + "',0,0)";
                    Scc.basic(insertCommand);
                    inforegister.Text = "Kayıt işlemi yapıldı. Mail adresinize gelen doğrulama linkine tıkladığınızda üyeliğiniz aktif olacaktır.";
                    inforegister.ForeColor = System.Drawing.Color.Green;
                    sendmail(email.Text, newidto_use);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Inner Exception: " + ex.Message);
                    inforegister.Text = "Hata, kayıt yapılamadı!";
                    inforegister.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        public void sendmail(String target, int whoid)
        {
            string mail = "idriscengizhancelebi@gmail.com";
            string sifre = "IDO18.rido";
            string yol = "https://localhost:44356/Activation.aspx?activationcode=" + whoid;
            Random rnd = new Random();
            try
            {
                MailMessage mesaj = new MailMessage(mail, target, "Onay Kodu", "Sayın " + firstname.Text + " " + lastname.Text + "Üyeliğinizi aktif etmek için lütfen linke tıklayınız." + yol);
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
            try
            {
                String command = "Select Count(1) from Users Where username='" + args.Value + "'";
                bool count = Scc.ifCount(command);
                if (count)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
        }
    }
}