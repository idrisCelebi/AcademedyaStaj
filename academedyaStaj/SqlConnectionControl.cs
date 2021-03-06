using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace academedyaStaj
{
    public class SqlConnectionControl
    {


        public SqlConnection _connection;
        public SqlTransaction transaction;
        public SqlCommand mainCommand;
        public SqlConnectionControl(String username = "akaStajIdris")
        {

            SqlConnection conn;

            //conn = new SqlConnection(@"data source=(localdb)\desktop;initial catalog=" + username + ";integrated security=True");

            conn = new SqlConnection(@"Data Source=178.18.205.230,49501;Initial Catalog=" + username + ";User ID=devUser;Password=akadev2018");
            _connection = conn;


        }

        public void openDB(SqlConnection conn)
        {
            mainCommand = new SqlCommand();
            mainCommand.Connection = _connection;
            conn.Open();

        }
        public void closeDB(SqlConnection conn)
        {
            conn.Close();

        }
        public bool istableHasData(String command)
        {
            try
            {
                openDB(_connection);
                mainCommand.CommandType = CommandType.Text;
                mainCommand.CommandText = command;
                SqlDataReader ch = mainCommand.ExecuteReader();
                if (ch.Read())
                {
                    ch.Close();
                    return true;
                }
                else
                {
                    ch.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                return false;
            }
            finally
            {
                closeDB(_connection);
            }
        }
        public String getidenty(String command)
        {
            try
            {
                openDB(_connection);
                mainCommand.CommandType = CommandType.Text;
                mainCommand.CommandText = command;
                SqlDataReader ch = mainCommand.ExecuteReader();
                if (ch.Read())
                {
                    String idi = ch.GetValue(0).ToString();
                    ch.Close();
                    return idi;
                }
                else
                {
                    ch.Close();
                    return "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                return "";
            }
            finally
            {
                closeDB(_connection);
            }
        }
        public int loginUserType(String command)
        {
            try
            {
                openDB(_connection);
                mainCommand.CommandType = CommandType.Text;
                mainCommand.CommandText = command;
                SqlDataReader reader = mainCommand.ExecuteReader();
                if (reader.Read())
                {

                    if (int.Parse(reader.GetValue(0).ToString()) == 1)//aktivasyonu yapılmış mı kontrolü
                    {
                        return 1;

                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                return -1;
            }
            finally
            {
                closeDB(_connection);
            }

        }
        public bool ifCount(String command)
        {
            try
            {
                openDB(_connection);
                mainCommand.CommandType = CommandType.Text;
                mainCommand.CommandText = command;            
                int count = Convert.ToInt32(mainCommand.ExecuteScalar());
                if (count == 1)
                {
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
                return false;
            }
            finally
            {
                closeDB(_connection);
            }
        }
        public int newidgenerator(String tableName)
        {
            int yenidsayi = 0;
            try
            {
                openDB(_connection);
                Random r = new Random();


                bool eslesme = false;
                do
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendFormat("select count(1) from {0} where id = @yenid", tableName);
                    yenidsayi = r.Next();
                    mainCommand.CommandType = CommandType.Text;
                    mainCommand.CommandText = query.ToString();
                    
                    mainCommand.Parameters.AddWithValue("@yenid", yenidsayi);
                    int count = Convert.ToInt32(mainCommand.ExecuteScalar());
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

                return yenidsayi;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                return yenidsayi;

            }
            finally
            {
                closeDB(_connection);
            }
        }
        public void basic(String command)
        {
            try
            {
                if (_connection != null && _connection.State == ConnectionState.Closed)
                    openDB(_connection);

                mainCommand.CommandType = CommandType.Text;
                mainCommand.CommandText = command;
               
                mainCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
            finally
            {
                mainCommand.Parameters.Clear();
            }
        }
        public SqlDataAdapter getdataAdapter(String command)
        {
            try
            {
                if (_connection != null && _connection.State == ConnectionState.Closed)
                    openDB(_connection);

                mainCommand.CommandType = CommandType.Text;
                mainCommand.CommandText = command;
              
                SqlDataAdapter da = new SqlDataAdapter(mainCommand);
                return da;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                return null;
            }
        }
        public List<string> getList(String command, String columnname)
        {
            try
            {
                List<string> columnlist = new List<string>();
                openDB(_connection);
                mainCommand.CommandType = CommandType.Text;
                mainCommand.CommandText = command;
                SqlDataReader rdr = mainCommand.ExecuteReader();
                while (rdr.Read())
                {
                    columnlist.Add(rdr[columnname].ToString());

                }
                return columnlist;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                return null;
            }
            finally
            {
                closeDB(_connection);
            }
        }
        public String getStringWC(String command, String columnname)
        {
            try
            {
                String ret = "";
                openDB(_connection);
                mainCommand.CommandType = CommandType.Text;
                mainCommand.CommandText = command;
                SqlDataReader rdr = mainCommand.ExecuteReader();
                if (rdr.Read())
                {
                    ret = rdr[columnname].ToString();
                }
                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                return null;
            }
            finally
            {
                closeDB(_connection);
            }
        }
        public void addExcel(String tablename, DataTable dtExcelData)
        {

            try
            {
                List<string> columnli = new List<string>();
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(_connection);
                sqlBulkCopy.DestinationTableName = tablename;
                String comm = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + tablename + "';";
                columnli = getList(comm, "COLUMN_NAME");
                columnli.RemoveAt(0);
                for (int i = 0; i < columnli.Count; i++)
                {
                    sqlBulkCopy.ColumnMappings.Add(columnli[i], columnli[i]);
                }
                openDB(_connection);
                sqlBulkCopy.WriteToServer(dtExcelData);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
            finally
            {
                closeDB(_connection);
            }
        }
        public void Begin_Transaction()
        {
            try
            {
                SqlTransaction trans;
                openDB(_connection);
                trans = _connection.BeginTransaction();
                this.transaction = trans;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
            finally
            {
                closeDB(_connection);
            }

        }
        public void Commit_Transaction()
        {
            try
            {

                openDB(_connection);

                this.transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
            finally
            {
                closeDB(_connection);
            }
        }
        public void Rollback_Transaction()
        {
            try
            {

                openDB(_connection);

                this.transaction.Rollback();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
            }
            finally
            {
                closeDB(_connection);
            }
        }
        public void Add_Parameter(String param, String value)
        {
            try
            {
                if (_connection != null && _connection.State == ConnectionState.Closed)
                    openDB(_connection);

                mainCommand.Parameters.AddWithValue("@" + param, value);
            }
            catch (Exception myExp)
            {
                throw myExp;
            }
        }

    }


}
