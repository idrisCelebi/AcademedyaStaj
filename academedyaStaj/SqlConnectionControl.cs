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
        public SqlConnectionControl(String username = "AcademedyaStajMain")
        {

            SqlConnection conn;

            conn = new SqlConnection(@"data source=DESKTOP-AR7QPFE\CENGIZHAN;initial catalog=" + username + ";integrated security=True");


            _connection = conn;


        }

        public void openDB(SqlConnection conn)
        {
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
                SqlCommand isEmpty = new SqlCommand(command, _connection);
                SqlDataReader ch = isEmpty.ExecuteReader();
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
        public int loginUserType(String command)
        {
            try
            {
                openDB(_connection);
                SqlCommand sqlCmd = new SqlCommand(cmdText: command, _connection);
                SqlDataReader reader = sqlCmd.ExecuteReader();
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
                SqlCommand sqlCmd = new SqlCommand(command, _connection);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
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
        public int newidgenerator(String tableName, SqlConnection conname)
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
                    SqlCommand yeniduret = new SqlCommand(query.ToString(), conname);
                    yeniduret.Parameters.AddWithValue("@yenid", yenidsayi);
                    int count = Convert.ToInt32(yeniduret.ExecuteScalar());
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
                openDB(_connection);
                SqlCommand adduser = new SqlCommand(command, _connection);
                adduser.ExecuteNonQuery();
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
        public SqlDataAdapter getdataAdapter(String command)
        {
            try
            {
                openDB(_connection);
                SqlCommand bring = new SqlCommand(command, _connection);
                SqlDataAdapter da = new SqlDataAdapter(bring);
                return da;
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
        public List<string> getList(String command, String columnname)
        {
            try
            {
                List<string> columnlist = new List<string>();
                openDB(_connection);
                SqlCommand bring = new SqlCommand(command, _connection);
                SqlDataReader rdr = bring.ExecuteReader();
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
                String ret="";
                openDB(_connection);
                SqlCommand bring = new SqlCommand(command, _connection);
                SqlDataReader rdr = bring.ExecuteReader();
                if(rdr.Read())
                {
                    ret=rdr[columnname].ToString();
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
        public void addExcel(String tablename,DataTable dtExcelData)
        {
           
            try
            {
                List<string> columnli = new List<string>();
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(_connection);
                sqlBulkCopy.DestinationTableName = tablename;
                String comm = "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" +tablename + "';";
                columnli = getList(comm, "COLUMN_NAME");
                columnli.RemoveAt(0);
                for(int i=0;i<columnli.Count;i++)
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
    }
   

}
