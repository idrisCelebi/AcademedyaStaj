using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class Main : System.Web.UI.MasterPage
    {
        SqlConnectionControl Scc;

        protected void Page_Load(object sender, EventArgs e)
        {
            Scc = new SqlConnectionControl(Session["username"].ToString());
            if (Session["username"] == null)
            {              
                Response.Redirect("login.aspx");
            }
            else
            {
                bringidenty();
            }
        }
        public void bringidenty()
        {
            String com = "Select firstname+' '+lastname from Users where username='"+Session["username"]+"'";       
                showwho.Text = Scc.getidenty(com);         
        }
    }
}