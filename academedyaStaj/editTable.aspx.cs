using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class editTable : System.Web.UI.Page
    {
        SqlConnectionControl Scc;
        List<string> columnlist = new List<string>();
        List<string> typelist = new List<string>();
        List<string> nullable = new List<string>();
        List<string> columnData = new List<string>();
        List<bool> isNumber = new List<bool>();
        List<bool> isDate = new List<bool>();
        List<bool> isDecimal = new List<bool>();

        TextBox tb;
        DropDownList ddl;
        CheckBox cb;
        CustomValidator cv;
        RequiredFieldValidator rfv;
        ImageButton ib;

        bool tablehasData;    
        protected void Page_Load(object sender, EventArgs e)
        {
            Scc = new SqlConnectionControl(Session["username"].ToString());
            if (!IsPostBack)
            {
                allviews.ActiveViewIndex = 0;
            }
        }
        protected void View1_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                infochangetable.Text = "";
                filllist();
            }
        }
        protected void View2_Load(object sender, EventArgs e)
        {
            infotablename.Text = DropDownList1.SelectedValue;         
            getcolumns();
        }
        protected void filllist()
        {
            try
            {
                DataSet ds = new DataSet();
                String getString = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
                Scc.getdataAdapter(getString).Fill(ds);
                DropDownList1.DataTextField = ds.Tables[0].Columns["TABLE_NAME"].ToString();
                DropDownList1.DataValueField = ds.Tables[0].Columns["TABLE_NAME"].ToString();
                DropDownList1.DataSource = ds.Tables[0];
                DropDownList1.DataBind();
                if (Session["lastSelected"] != null)
                {
                    DropDownList1.SelectedValue = Session["lastSelected"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
        }
        protected void delete_Click(object sender, EventArgs e)
        {
            try
            {
                String deletecommand = "Drop table " + DropDownList1.SelectedValue;
                Scc.basic(deletecommand);
                Response.Redirect("editTable.aspx");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
        }
        protected void edit_Click(object sender, EventArgs e)
        {
            allviews.ActiveViewIndex = 1;
        }
        public void filldropdownforcolumntype()
        {
            ddl = new DropDownList();
            ddl.Items.Clear();
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
        public void getcolumns()
        {
            try
            {
                PlaceHolder1.Controls.Clear();
                columnlist.Clear();
                typelist.Clear();
                nullable.Clear();
                String comm = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + infotablename.Text + "';";
                
                columnlist = Scc.getList(comm, "COLUMN_NAME");
                typelist = Scc.getList(comm, "DATA_TYPE");
                nullable = Scc.getList(comm, "IS_NULLABLE");
                columnlist.RemoveAt(0);
                typelist.RemoveAt(0);
                nullable.RemoveAt(0);
                for (int i = 0; i < columnlist.Count; i++)
                {
                    ib = new ImageButton();
                    ib.ID = "delete" + i.ToString();
                    ib.ImageUrl = "assets/delete2.jpg";
                    ib.Width = 25;
                    ib.Height = 20;
                    ib.AlternateText = "Kaldır";
                    ib.ForeColor = System.Drawing.Color.Red;
                    ib.CausesValidation = false;
                    ib.OnClientClick = "return confirm('Bu sütun silinecektir ?')";
                    ib.Click += new ImageClickEventHandler(ImageButton1_Click);

                    tb = new TextBox();
                    tb.ID = "textbox" + i.ToString();
                    tb.Text = columnlist[i];
                    tb.CssClass = "form-control-sm";

                    filldropdownforcolumntype();
                    ddl.ID = "dropdown" + i.ToString();
                    ddl.CssClass = "nav-item dropdown";
                    String checkisempty = "select * from " + infotablename.Text;
                   
                  
                  
                    tablehasData = Scc.istableHasData(checkisempty);
                    if (tablehasData)
                    {
                        columnData.Clear();
                        isNumber.Clear();
                        isDate.Clear();
                        String checkData = "SELECT " + columnlist[i] + " FROM " + infotablename.Text;
                        columnData = Scc.getList(checkData, columnlist[i]);
                        
                        infochangetable.Text = "Tablonuzda veriler olduğu için sadece değiştirebileceğiniz sütun tipleri mevcuttur.";
                        if (typelist[i] == "nvarchar")
                        {
                            for (int j = 0; j < columnData.Count; j++)
                            {
                                isNumber.Add(int.TryParse(columnData[j], out int n));
                                isDate.Add(DateTime.TryParse(columnData[j], out DateTime da));
                                isDecimal.Add(decimal.TryParse(columnData[j], out decimal de));
                            }
                            if(isNumber.Contains(true)&& !isNumber.Contains(false))
                            {
                                ddl.Items.Remove(ddl.Items.FindByValue("date"));
                            }                          
                            else if (isDate.Contains(true) && !isDate.Contains(false))
                            {
                                ddl.Items.Remove(ddl.Items.FindByValue("decimal(18, 2)"));
                                ddl.Items.Remove(ddl.Items.FindByValue("int"));
                            }
                            else if (isDecimal.Contains(true) && !isDecimal.Contains(false))
                            {
                                ddl.Items.Remove(ddl.Items.FindByValue("date"));
                            }
                            else
                            {
                                ddl.Items.Remove(ddl.Items.FindByValue("decimal(18, 2)"));
                                ddl.Items.Remove(ddl.Items.FindByValue("int"));
                                ddl.Items.Remove(ddl.Items.FindByValue("date"));
                            }

                        }

                        if (typelist[i] == "decimal")
                        {                          
                            ddl.Items.Remove(ddl.Items.FindByValue("date"));
                        }
                        if (typelist[i] == "date")
                        {
                            ddl.Items.Remove(ddl.Items.FindByValue("decimal(18, 2)"));
                            ddl.Items.Remove(ddl.Items.FindByValue("int"));
                        }
                        if (typelist[i] == "int")
                        {
                            ddl.Items.Remove(ddl.Items.FindByValue("date"));
                        }

                    }
                    cb = new CheckBox();
                    cb.ID = "checkbox" + i.ToString();
                    cb.Text = "Null olabilir";
                    if (nullable[i] == "YES")
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        cb.Checked = false;
                    }
                    rfv = new RequiredFieldValidator();
                    rfv.ID = "validator" + i.ToString();
                    rfv.ControlToValidate = "textbox" + i.ToString();
                    rfv.ErrorMessage = "*";
                    rfv.ForeColor = System.Drawing.Color.Red;

                    cv = new CustomValidator();
                    cv.ID = "cvalidator" + i.ToString();
                    cv.ControlToValidate = "textbox" + i.ToString();
                    cv.ErrorMessage = "'id' eklenemez";
                    cv.ForeColor = System.Drawing.Color.Red;
                    cv.ClientValidationFunction = "cv_ServerValidate";
                    cv.ServerValidate += new ServerValidateEventHandler(cv_ServerValidate);
                    cv.Display = (ValidatorDisplay)2;

                    PlaceHolder1.Controls.Add(ib);
                    PlaceHolder1.Controls.Add(tb);
                    PlaceHolder1.Controls.Add(new HtmlGenericControl(" "));
                    PlaceHolder1.Controls.Add(ddl);
                    PlaceHolder1.Controls.Add(new HtmlGenericControl(" "));
                    PlaceHolder1.Controls.Add(cb);
                    PlaceHolder1.Controls.Add(new HtmlGenericControl("br"));
                    if (typelist[i] == "decimal")
                    {
                        ddl.SelectedValue = typelist[i] + "(18, 2)";
                    }
                    if (typelist[i] == "nvarchar")
                    {
                        ddl.SelectedValue = typelist[i] + "(50)";
                    }
                    if (typelist[i] == "int")
                    {
                        ddl.SelectedValue = typelist[i];
                    }
                    if (typelist[i] == "date")
                    {
                        ddl.SelectedValue = typelist[i];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            String deleterow = "ALTER TABLE " + infotablename.Text + " DROP COLUMN " + columnlist[getnumberinstring(btn.ID)];
            Scc.basic(deleterow);
            getcolumns();
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
        public bool issame()
        {
            inputlist ip = new inputlist();
            ip.alltextbox = PlaceHolder1.Controls.OfType<TextBox>().ToList();
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
        public int getnumberinstring(String ddl_id)
        {
            var data = Regex.Match(ddl_id, @"\d+").Value;
            int t_id = int.Parse(data);
            return t_id;
        }
        protected void change_Click(object sender, EventArgs e)
        {
            inputlist ip = new inputlist();
            ip.alltextbox = PlaceHolder1.Controls.OfType<TextBox>().ToList();
            ip.alldropdown = PlaceHolder1.Controls.OfType<DropDownList>().ToList();
            ip.allcheckbox = PlaceHolder1.Controls.OfType<CheckBox>().ToList();
            if (rfv == null)
            {
                infochangetable.Text = "Sütunsuz tablo oluşturulamaz";
                infochangetable.ForeColor = System.Drawing.Color.Red;
            }
            else if (rfv.IsValid != true || cv.IsValid != true)
            {
                infochangetable.Text = "Lütfen düzgün değerler giriniz";
                infochangetable.ForeColor = System.Drawing.Color.Red;
            }
            else if (!issame())
            {
                try
                {
                    StringBuilder columns = new StringBuilder();
                    StringBuilder columnnames = new StringBuilder();
                    int index = 0;
                    String columnsstring = "";
                    foreach (TextBox createtotextBox in ip.alltextbox)
                    {
                        if (ip.allcheckbox.Where(x => getnumberinstring(x.ID) == getnumberinstring(createtotextBox.ID)).First().Checked)
                        {
                            columnsstring = "Alter Table " + infotablename.Text + " Alter Column " + createtotextBox.Text + " " + ip.alldropdown.Where(x => getnumberinstring(x.ID) == getnumberinstring(createtotextBox.ID)).First().SelectedValue + " NULL";
                        }
                        else
                        {
                            columnsstring = "Alter Table " + infotablename.Text + " Alter Column " + createtotextBox.Text + " " + ip.alldropdown.Where(x => getnumberinstring(x.ID) == getnumberinstring(createtotextBox.ID)).First().SelectedValue + " NOT NULL";
                        }
                        String changenamgestring = "sp_rename '" + infotablename.Text + "." + columnlist[index] + "', '" + createtotextBox.Text + "', 'COLUMN';";
                        Scc.basic(changenamgestring);
                        if (!tablehasData)
                        {
                            String tovarchar = "Alter Table " + infotablename.Text + " Alter Column " + createtotextBox.Text + " nvarchar(50)";
                            Scc.basic(tovarchar);
                        }
                        Scc.basic(columnsstring);
                        index++;
                    }
                    Response.Redirect("editTable.aspx");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Inner Exception: " + ex.Message);
                }
            }
            else
            {
                infochangetable.Text = "Aynı sütun isimleri olamaz!";
                infochangetable.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void View3_Load(object sender, EventArgs e)
        {
            Label1.Text = "'"+DropDownList1.SelectedValue + "' tablosuna bir adet sütun eklenecektir";
            tb = new TextBox();
            tb.ID = "addtextbox";
            tb.CssClass = "form-control-sm";

            filldropdownforcolumntype();
            ddl.ID = "adddropdown";
            ddl.CssClass = "nav-item dropdown";

            cb = new CheckBox();
            cb.ID = "addcheckbox";
            cb.Text = "Null olabilir";
            cb.Checked = true;

            rfv = new RequiredFieldValidator();
            rfv.ID = "addvalidator";
            rfv.ControlToValidate = "addtextbox";
            rfv.ErrorMessage = "*";
            rfv.ForeColor = System.Drawing.Color.Red;
          
            PlaceHolder2.Controls.Add(rfv);          
            PlaceHolder2.Controls.Add(tb);
            PlaceHolder2.Controls.Add(new HtmlGenericControl(" "));
            PlaceHolder2.Controls.Add(ddl);
            PlaceHolder2.Controls.Add(new HtmlGenericControl(" "));
            if(!tablehasData)
            {
                PlaceHolder2.Controls.Add(cb);
            }
                  

        }

        protected void addcolumnone_Click(object sender, EventArgs e)
        {
            allviews.ActiveViewIndex = 2;
        }

        protected void finishaddColumn_Click(object sender, EventArgs e)
        {
            String addCommand;
            if (cb.Checked)
            {
                addCommand = "ALTER TABLE " + infotablename.Text + " ADD " + tb.Text + " " + ddl.SelectedValue + " NULL";
            }
            else
            {

                addCommand = "ALTER TABLE " + infotablename.Text + " ADD " + tb.Text + " " + ddl.SelectedValue + " NOT NULL";
            }
            try
            {
                Scc.basic(addCommand);
                allviews.ActiveViewIndex = 1;
                getcolumns();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
          
        }
    }
    public class inputlist2
    {
        public List<DropDownList> alldropdown { get; set; }
        public List<TextBox> alltextbox { get; set; }
        public List<CheckBox> allcheckbox { get; set; }
    }
}