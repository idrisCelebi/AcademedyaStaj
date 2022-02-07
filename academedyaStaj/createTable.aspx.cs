
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class createTable : System.Web.UI.Page
    {
        SqlConnectionControl Scc;

        TextBox tb;
        DropDownList ddl;
        CheckBox cb;
        RequiredFieldValidator rfv;
        CustomValidator cv;
        ImageButton ib;

        static int countcolumn;
        protected void Page_Load(object sender, EventArgs e)
        {
            Scc = new SqlConnectionControl(Session["username"].ToString());

            if (!IsPostBack)
            {
                countcolumn = 0;
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
            tarih.Value = "date";

            ListItem benzersiz = new ListItem();
            benzersiz.Text = "Decimal";
            benzersiz.Value = "decimal(18, 2)";

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
            ip.allcheckbox = PlaceHolder1.Controls.OfType<CheckBox>().ToList();
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
                try
                {
                    StringBuilder columns = new StringBuilder();
                    foreach (TextBox createtotextBox in ip.alltextbox)
                    {
                        if (ip.allcheckbox.Where(x => getnumberinstring(x.ID) == getnumberinstring(createtotextBox.ID)).First().Checked)
                        {
                            columns.AppendFormat("{0} {1} {2}", createtotextBox.Text, ip.alldropdown.Where(x => getnumberinstring(x.ID) == getnumberinstring(createtotextBox.ID)).First().SelectedValue, "NULL,");
                        }
                        else
                        {
                            columns.AppendFormat("{0} {1} {2}", createtotextBox.Text, ip.alldropdown.Where(x => getnumberinstring(x.ID) == getnumberinstring(createtotextBox.ID)).First().SelectedValue, "NOT NULL,");
                        }
                    }
                    String createTable = "Create Table " + tablename.Text + "(id int IDENTITY(1,1) PRIMARY KEY," + columns.ToString() + ");";
                    Scc.basic(createTable);
                    Response.Redirect("Tables.aspx");

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Inner Exception: " + ex.Message);
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
                ib = new ImageButton();
                ib.ImageUrl = "assets/delete2.jpg";
                ib.Width = 25;
                ib.Height = 20;
                ib.AlternateText = "Kaldır";
                ib.ForeColor = System.Drawing.Color.Red;
                ib.CausesValidation = false;
                ib.Click += new ImageClickEventHandler(ImageButton1_Click);

                tb = new TextBox();
                tb.ID = "textbox" + j.ToString();
                tb.CssClass = "form-control-sm";

                filldropdownforcolumntype();
                ddl.ID = "dropdown" + j.ToString();
                ddl.CssClass = "nav-item dropdown";           

                cb = new CheckBox();
                cb.ID = "checkbox" + j.ToString();
                cb.Text = "Null olabilir";
                cb.Checked = true;

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

                PlaceHolder1.Controls.Add(ib);
                PlaceHolder1.Controls.Add(rfv);
                PlaceHolder1.Controls.Add(cv);
                PlaceHolder1.Controls.Add(tb);
                PlaceHolder1.Controls.Add(new HtmlGenericControl(" "));
                PlaceHolder1.Controls.Add(ddl);
                PlaceHolder1.Controls.Add(new HtmlGenericControl(" "));
                PlaceHolder1.Controls.Add(cb);
                PlaceHolder1.Controls.Add(new HtmlGenericControl("br"));

            }

        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            infocreatetable.Text = "";
            countcolumn--;
            showcolumns();
        }   
        public int getnumberinstring(String ddl_id)
        {
            var data = Regex.Match(ddl_id, @"\d+").Value;
            int t_id = int.Parse(data);
            return t_id;


        }
        protected void columnview_Load1(object sender, EventArgs e)
        {
            showcolumns();
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                String command = "Select Count(1) from INFORMATION_SCHEMA.TABLES Where TABLE_NAME='" + args.Value + "'";
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
        protected void cv_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value == "id" || args.Value == "ID")
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
        public List<CheckBox> allcheckbox { get; set; }

    }



}