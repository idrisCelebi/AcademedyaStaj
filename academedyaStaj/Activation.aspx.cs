using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class Activation : System.Web.UI.Page
    {
        SqlConnectionControl Scc = new SqlConnectionControl();

        public string Databaseusername = "";
        protected void Page_Load(object sender, EventArgs e)
        {   if (Request.QueryString["activationcode"] != null)
            {   
               
                int idtoactive = int.Parse(Request.QueryString["activationcode"]);
                if (!isactive(idtoactive))
                {
                    activationinfo.Text = "Profiliniz zaten aktif durumdadır. Buradan giriş yapın.";
                }
                else
                {
                    Scc.openDB(Scc._connection);
                    SqlCommand command = new SqlCommand("update Users Set activation=1 where id=@id",Scc._connection );
                    command.Parameters.AddWithValue("@id", idtoactive);
                    SqlDataReader dataread = command.ExecuteReader();

                    Scc.closeDB(Scc._connection);
                    createDatabase(Databaseusername);
                        activationinfo.Text = "Aktivasyon işlemi başarıyla yapılmıştır.Buradan giriş yapabilirsiniz.";
                    activationinfo.ForeColor = System.Drawing.Color.Green;
                }
            }
        else
            {

            }
        }
  
        public bool isactive(int userid)
        {

            try
            {
                Scc.openDB(Scc._connection);

                SqlCommand command = new SqlCommand("select username from Users where id=@id and activation=0", Scc._connection);
                command.Parameters.AddWithValue("@id", userid);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Databaseusername = reader.GetValue(0).ToString();

                    return true;
                }
                else
                {


                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                return true;

            }
            finally
            {
                Scc.closeDB(Scc._connection);
            }

        }
        public void createDatabase(String getusername)
        {

            try
            {
                Scc.openDB(Scc._connection);
                String commandtext = "Create Database " + getusername;
                SqlCommand command = new SqlCommand(commandtext, Scc._connection);

                int ithappen = command.ExecuteNonQuery();
                Scc.closeDB(Scc._connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);


            }
            finally
            {
                Scc.closeDB(Scc._connection);
            }
        }
    }
}