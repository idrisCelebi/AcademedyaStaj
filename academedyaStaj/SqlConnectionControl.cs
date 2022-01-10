using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace academedyaStaj
{
    public class SqlConnectionControl
    {
        
        public SqlConnection connectionForUser;

        public SqlConnection getdatabasename()
        {
            login lo = new login();
            if(lo.Session["username"]==null)
            {
                connectionForUser = new SqlConnection(@"data source=DESKTOP-AR7QPFE\CENGIZHAN;initial catalog=AcademedyaStajMain;integrated security=True");
            }
            else
            {
                connectionForUser = new SqlConnection(@"data source=DESKTOP-AR7QPFE\CENGIZHAN;initial catalog=" + lo.Session["username"] + ";integrated security=True");
            }
             

            return connectionForUser;

        }
        public void openDB()
        {
            getdatabasename().Open();

        }
        public void closeDB()
        {
            getdatabasename().Close();

        }


    }
}