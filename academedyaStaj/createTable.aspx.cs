using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class createTable : System.Web.UI.Page
    {
        SqlConnectionControl scc = new SqlConnectionControl();
      
        TextBox tb;
        DropDownList ddl;
        RequiredFieldValidator rfv;
        CustomValidator cv;
        static int countcolumn = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Allviews.ActiveViewIndex = 0;
            }
        }

        protected void forwardbutton_Click(object sender, EventArgs e)
        {
            if (tablename.Text != null && CustomValidator1.IsValid)
            { Allviews.ActiveViewIndex = 1; }

        }
        public void filldropdownforcolumntype()
        {
            ddl = new DropDownList();
            ListItem sayi = new ListItem();
            sayi.Text = "Sayı";
            sayi.Value = "int";

            ListItem metin = new ListItem();
            metin.Text = "Metin";
            metin.Value = "nvarchar(50)";

            ListItem tarih = new ListItem();
            tarih.Text = "Tarih";
            tarih.Value = "datetime";

            ListItem benzersiz = new ListItem();
            benzersiz.Text = "Benzersiz";
            benzersiz.Value = "uniqueidentifier";

            ddl.Items.Insert(0, sayi);
            ddl.Items.Insert(1, metin);
            ddl.Items.Insert(2, tarih);
            ddl.Items.Insert(3, benzersiz);
        }

        protected void addcolumn_Click(object sender, EventArgs e)
        {
            infocreatetable.Text = "";
            countcolumn++;
            showcolumns();

        }
        public bool issame()
        {
            inputlist ip = new inputlist();
            ip.alltextbox = PlaceHolder1.Controls.OfType<TextBox>().ToList();
            ip.alldropdown = PlaceHolder1.Controls.OfType<DropDownList>().ToList();
            foreach (TextBox list1 in ip.alltextbox)
            {
                foreach (TextBox list2 in ip.alltextbox)
                {
                    if (list1.Text == list2.Text && list1.ID != list2.ID)
                    {
                        return true;
                    }
                }

            }

            return false;
        }
        protected void finishcreateTable_Click(object sender, EventArgs e)
        {
            inputlist ip = new inputlist();
            ip.alltextbox = PlaceHolder1.Controls.OfType<TextBox>().ToList();
            ip.alldropdown = PlaceHolder1.Controls.OfType<DropDownList>().ToList();


            if (rfv == null)
            {
                infocreatetable.Text = "Sütunsuz tablo oluşturulamaz";
                infocreatetable.ForeColor = System.Drawing.Color.Red;
            }
            else if (rfv.IsValid != true || cv.IsValid != true)
            {

                infocreatetable.Text = "Lütfen düzgün değerler giriniz";
                infocreatetable.ForeColor = System.Drawing.Color.Red;

            }

            else if (!issame())
            {
                SqlConnection conn = scc.getdatabasename();
                try
                {

                   

                    conn.Open();
                    String Command1 = "Create Table " + tablename.Text + "(id int IDENTITY(1,1) PRIMARY KEY);";
                    SqlCommand addtable = new SqlCommand(Command1, conn);
                    addtable.ExecuteNonQuery();

                    foreach (TextBox createtotextBox in ip.alltextbox)
                    {

                        String Command2 = "alter table " + tablename.Text + " add " + createtotextBox.Text + " " + ip.alldropdown.Where(x => getnumberinstring(x.ID) == getnumberinstring(createtotextBox.ID)).First().SelectedValue + ";";
                        SqlCommand addcolumn = new SqlCommand(Command2, conn);
                        int happen = addcolumn.ExecuteNonQuery();
                    }

                    Response.Redirect("Tables.aspx");

                }

                catch
                {
                    Response.Write("Beklenmedik bir hata");
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                infocreatetable.Text = "Aynı sütun isimleri olamaz!";
                infocreatetable.ForeColor = System.Drawing.Color.Red;
            }





        }
        public void showcolumns()
        {
            PlaceHolder1.Controls.Clear();
            for (int j = 0; j < countcolumn; j++)
            {
                tb = new TextBox();
                tb.ID = "textbox" + j.ToString();
                tb.CssClass = "form-control-sm";


                filldropdownforcolumntype();
                ddl.ID = "dropdown" + j.ToString();
                ddl.CssClass = "nav-item dropdown";
                //ddl.AutoPostBack = true;
                // ddl.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);

                rfv = new RequiredFieldValidator();
                rfv.ID = "validator" + j.ToString();
                rfv.ControlToValidate = "textbox" + j.ToString();
                rfv.ErrorMessage = "*";
                rfv.ForeColor = System.Drawing.Color.Red;
               

                cv = new CustomValidator();
                cv.ID = "cvalidator" + j.ToString();
                cv.ControlToValidate = "textbox" + j.ToString();
                cv.ErrorMessage = "'id' eklenemez";
                cv.ForeColor = System.Drawing.Color.Red;
                cv.ClientValidationFunction = "cv_ServerValidate";
                cv.ServerValidate += new ServerValidateEventHandler(cv_ServerValidate);
                cv.Display = (ValidatorDisplay)2;

                PlaceHolder1.Controls.Add(rfv);
                PlaceHolder1.Controls.Add(cv);
                PlaceHolder1.Controls.Add(tb);
                PlaceHolder1.Controls.Add(new HtmlGenericControl("  "));
                PlaceHolder1.Controls.Add(ddl);
                PlaceHolder1.Controls.Add(new HtmlGenericControl("br"));

            }
        }

        /* protected void SelectedIndexChanged(object sender, EventArgs e)
         {
             DropDownList ddl = (DropDownList)sender;
             string ID = ddl.ID;
             inputlist ip = new inputlist();
             ip.alltextbox = PlaceHolder1.Controls.OfType<TextBox>().ToList();
             ip.alldropdown = PlaceHolder1.Controls.OfType<DropDownList>().ToList();


                      foreach (TextBox createtotextBox in ip.alltextbox)
             {

                 }
         }
        */
        public int getnumberinstring(String ddl_id)
        {
            var data = Regex.Match(ddl_id, @"\d+").Value;
            int t_id = int.Parse(data);
            return t_id;


        }

        protected void columnview_Load(object sender, EventArgs e)
        {
            showcolumns();

        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            SqlConnection conn = scc.getdatabasename();
            conn.Open();
            SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(1) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@tablename", conn);

            sqlCmd.Parameters.AddWithValue("@tablename", args.Value);

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

        protected void cv_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value == "id" || args.Value=="ID")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }

    public class inputlist
    {
        public List<DropDownList> alldropdown { get; set; }

        public List<TextBox> alltextbox { get; set; }
    }



}