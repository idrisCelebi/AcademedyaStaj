using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace academedyaStaj
{
    public partial class addData : System.Web.UI.Page
    {
        SqlConnectionControl Scc;
        viewTable vtable = new viewTable();
        Label ll;
        TextBox tb;
        RegularExpressionValidator rev;
        RequiredFieldValidator rfv;

        List<string> columnlist = new List<string>();
        List<string> typelist = new List<string>();
        List<string> nullable = new List<string>();
        bool send = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            Scc = new SqlConnectionControl(Session["username"].ToString());

            if (!IsPostBack)
            {
                filllist();

            }
            if (allviews.ActiveViewIndex == 1)
            {
                getcolumns();
            }


        }
        public void filllist()
        {
            allviews.ActiveViewIndex = 0;
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
        protected void forwardbutton_Click(object sender, EventArgs e)
        {
            Session["lastSelected"] = DropDownList1.SelectedValue;
            infotablename.Text = DropDownList1.SelectedValue;
            getcolumns();
        }



        public void getcolumns()
        {
            allviews.ActiveViewIndex = 1;
            try
            {
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
                    ll = new Label();
                    ll.ID = "label" + i.ToString();
                    ll.Text = columnlist[i];


                    tb = new TextBox();
                    tb.ID = "textbox" + i.ToString();
                    tb.TextMode = (TextBoxMode)getTextmode(typelist[i]);
                    tb.CssClass = "form-control-sm";


                    PlaceHolder1.Controls.Add(ll);
                    PlaceHolder1.Controls.Add(new HtmlGenericControl("  "));
                    PlaceHolder1.Controls.Add(tb);
                    if (typelist[i] == "decimal")
                    {
                        rev = new RegularExpressionValidator();
                        rev.ID = "Regularvalidate" + i.ToString();
                        rev.ControlToValidate = "textbox" + i.ToString();
                        rev.ErrorMessage = "Tam sayı veya ondalıklı sayı girebilirsin sadece";
                        rev.ValidationExpression = @"^[1-9]\d*(\.\d+)?$$";
                        rev.ForeColor = System.Drawing.Color.Red;
                        rev.Display = (ValidatorDisplay)2;
                        PlaceHolder1.Controls.Add(rev);
                    }
                    if (nullable[i] == "NO")
                    {
                        rfv = new RequiredFieldValidator();
                        rfv.ID = "Requiredvalidate" + i.ToString();
                        rfv.ControlToValidate = "textbox" + i.ToString();
                        rfv.ErrorMessage = "Bu sütun boş kalamaz";
                        rfv.ForeColor = System.Drawing.Color.Red;
                        rfv.Display = (ValidatorDisplay)2;
                        PlaceHolder1.Controls.Add(rfv);
                    }
                    PlaceHolder1.Controls.Add(new HtmlGenericControl("br"));
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
        }
        public int getTextmode(String typeColumn)
        {
            switch (typeColumn)
            {
                case "int":
                    return 9;
                case "date":
                    return 4;
                case "nvarchar":
                    return 0;
                case "decimal":
                    return 0;
                default:
                    return 0;
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();
            try
            {
                addDataControls ip = new addDataControls();
                ip.alltextbox = PlaceHolder1.Controls.OfType<TextBox>().ToList();
                for (int i = 0; i < columnlist.Count; i++)
                {
                    values.AppendFormat("'{0}'", ip.alltextbox[i].Text);
                    columns.AppendFormat("{0}", columnlist[i]);
                    if (i == columnlist.Count - 1)
                    {
                        columns.Append(")");
                        values.Append(")");
                    }
                    else
                    {
                        columns.Append(",");
                        values.Append(",");
                    }
                }
                String command = "insert into " + infotablename.Text + " (" + columns.ToString() + " values(" + values.ToString();
                Scc.basic(command);
                Response.Redirect("viewTable.aspx?view=1");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
        }

        protected void multiAddButton_Click(object sender, EventArgs e)
        {
            allviews.ActiveViewIndex = 2;
            Session["lastSelected"] = DropDownList1.SelectedValue;
            infotablename.Text = DropDownList1.SelectedValue;

        }

        protected void sendExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(excelPath);
                    string conString = string.Empty;
                    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    switch (extension)
                    {
                        case ".xls": //Excel 97-03
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //Excel 07 or higher
                            conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                            break;
                    }
                    conString = string.Format(conString, excelPath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();
                        String comm = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + infotablename.Text + "';";
                        columnlist = Scc.getList(comm, "COLUMN_NAME");
                        typelist = Scc.getList(comm, "DATA_TYPE");
                        columnlist.RemoveAt(0);
                        typelist.RemoveAt(0);
                        for (int i = 0; i < columnlist.Count; i++)
                        {
                            if (typelist[i] == "int")
                            {
                                dtExcelData.Columns.AddRange(new DataColumn[1] { new DataColumn(columnlist[i], typeof(int)) });
                            }
                            if (typelist[i] == "nvarchar")
                            {
                                dtExcelData.Columns.AddRange(new DataColumn[1] { new DataColumn(columnlist[i], typeof(string)) });
                            }
                            if (typelist[i] == "decimal")
                            {
                                dtExcelData.Columns.AddRange(new DataColumn[1] { new DataColumn(columnlist[i], typeof(decimal)) });
                            }
                            if (typelist[i] == "date")
                            {
                                dtExcelData.Columns.AddRange(new DataColumn[1] { new DataColumn(columnlist[i], typeof(DateTime)) });
                            }

                        }


                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        int colindex = 0;
                        if (columnlist.Count == dtExcelData.Columns.Count)
                        {
                            for (int j = 0; j < dtExcelData.Columns.Count; j++)
                            {
                                for (int i = 0; i < dtExcelData.Rows.Count; i++)
                                {
                                    
                                    if(typelist[colindex]=="int")
                                    {
                                          var temp = dtExcelData.Rows[i][j].ToString();
                                          bool wh =int.TryParse(temp, out int n);
                                            if(wh==false)
                                        {
                                            infoAdd.Text = "Excel dosyanız yüklenemedi. Lütfen "+(i+2).ToString()+"'nci satır ve "+(j+1).ToString()+"'nci sütununu kontrol ediniz.";
                                            infoAdd.ForeColor = System.Drawing.Color.Red;
                                            FileUpload1.Attributes.Clear();
                                            send = false;
                                        }
                                    }
                                    if (typelist[colindex] == "decimal")
                                    {
                                        var temp = dtExcelData.Rows[i][j].ToString();
                                        bool wh = decimal.TryParse(temp, out decimal n);
                                        if (wh == false)
                                        {
                                            infoAdd.Text = "Excel dosyanız yüklenemedi. Lütfen " + (i+2).ToString() + "'nci satır ve " + (j+1).ToString() + "'nci sütununu kontrol ediniz.";
                                            infoAdd.ForeColor = System.Drawing.Color.Red;
                                            FileUpload1.Attributes.Clear();
                                            send = false;
                                        }
                                    }                                 
                                    if (typelist[colindex] == "date")
                                    {
                                        
                                           var temp = dtExcelData.Rows[i][j].ToString();
                                        bool wh = DateTime.TryParse(temp, out DateTime n);
                                        if (wh == false)
                                        {
                                            infoAdd.Text = "Excel dosyanız yüklenemedi. Lütfen " + (i+2).ToString() + "'nci satır ve " + (j+1).ToString() + "'nci sütununu kontrol ediniz.";
                                            infoAdd.ForeColor = System.Drawing.Color.Red;
                                            FileUpload1.Attributes.Clear();
                                            send = false;
                                        }
                                    }
                                }
                                colindex++;
                                
                            }
                            excel_con.Close();
                            if(send)
                            {
                               
                                Scc.addExcel(infotablename.Text, dtExcelData);                               
                                Response.Redirect("viewTable.aspx?view=1");
                            }
                            else
                            {
                               
                                FileUpload1.Attributes.Clear();
                            }

                        }
                        else
                        {
                            infoAdd.Text = "Excel veritabanına yüklenemedi çünkü sütun sayıları uyuşmuyor.";
                            infoAdd.ForeColor = System.Drawing.Color.Red;
                            FileUpload1.Attributes.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);              
                infoAdd.Text = "Excel veritabanına yüklenemedi lütfen içerisine tablonuza uygun değerler giriniz";
                infoAdd.ForeColor = System.Drawing.Color.Red;
            }


        }
    }
    public class addDataControls
    {
        public List<TextBox> alltextbox { get; set; }
    }
}