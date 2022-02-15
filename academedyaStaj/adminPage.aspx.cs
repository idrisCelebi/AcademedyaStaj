using DevExpress.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class adminPage : System.Web.UI.Page
    {
        SqlConnectionControl Scc = new SqlConnectionControl();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (!IsPostBack && !IsCallback)
            {
                // fillUsers();
                
            }
            
           
           

        }
        public void fillUsers()
        {
            String comm = "Select * from Users";
            dt = new DataTable();
            Scc.getdataAdapter(comm).Fill(dt);
                 
            grid.DataSource = dt;
            grid.DataBind();
           
        }

        protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        { e.Cancel = true;
            Response.Write("girdim0");
            
        }

        protected void grid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            Response.Write("girdim1");
        }

        protected void grid_EditFormLayoutCreated(object sender, ASPxGridViewEditFormLayoutEventArgs e)
        {
            Response.Write("girdim2");
        }

        protected void grid_HtmlEditFormCreated1(object sender, ASPxGridViewEditFormEventArgs e)
        {
            Response.Write("girdim3");
        }

        protected void grid_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            Response.Write(e.CommandArgs.CommandName);
        }

       
    }
}