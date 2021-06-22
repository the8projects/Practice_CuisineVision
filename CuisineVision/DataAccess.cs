using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace CuisineVision
{
    class DataAccess
    {
        public bool DbLogin(string account, string password)
        {
            using (MySqlConnection db = new MySqlConnection("server=localhost; port=3306; uid=root; database=cuisinevision; charset=utf8;"))
            {
                try
                {
                    db.Open();
                    string strSql = "SELECT * FROM account WHERE AccUsername = @account AND AccPassword = @password;";
                    MySqlCommand sqlCmd = new MySqlCommand(strSql, db);
                    sqlCmd.Parameters.AddWithValue("@account", account);
                    sqlCmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }          
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    db.Close();
                }
                return false;
            }
        }

        public DataTable GetData()
        {
            using (MySqlConnection db = new MySqlConnection("server=localhost; port=3306; uid=root; database=cuisinevision; charset=utf8;"))
            {
                try
                {
                    db.Open();
                    String strCmd = "SELECT Primary_Key AS Number, Text_Entry AS Name FROM MyTable;";
                    MySqlCommand cmdSelect = new MySqlCommand(strCmd, db);
                    cmdSelect.ExecuteNonQuery();

                    MySqlDataAdapter dataAdp = new MySqlDataAdapter(cmdSelect);
                    DataTable dt = new DataTable();
                    dataAdp.Fill(dt);
                    db.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
                finally
                {
                    db.Close();
                }
            }
        }
    }
}
