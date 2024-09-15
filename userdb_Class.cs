using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public class userdb_Class : dbClass
    {
        public string passWord;
        public int userId;
        public string name;
        public string userName;
        public string userName1 { get => userName; set => userName = value; }
        public string passWord1 { get => passWord; set => passWord = value; }
        public int userId1 { get => userId; set => userId = value; }
        public string name1 { get => name; set => name = value; }
        //10
        public string getDays()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getDays";
            cmd.Parameters.AddWithValue("@id", 1);
            try
            {
                string a = "";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr["days"].ToString();
                }
                con.Close();
                return a;
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string getOpenDate()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getOpenDate";
            cmd.Parameters.AddWithValue("@id", 1);
            try
            {
                string a = "";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr["startDay"].ToString();
                }
                con.Close();
                return a;
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string getPid()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getPid";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
                string a = "";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr["Pid"].ToString();
                }
                con.Close();
                return a;
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string insertUser()
        {
            DateTime a = Convert.ToDateTime(getOpenDate().Trim());
            int b = Convert.ToInt32(getDays().Trim());
            string c = getPid().Trim();
            cmd.Parameters.Clear();
            cmd.CommandText = "insertUser";
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@passWord", passWord);
            cmd.Parameters.AddWithValue("@printer", "Microsoft Print to PDF");
            cmd.Parameters.AddWithValue("@source", "F:");
            cmd.Parameters.AddWithValue("@startDay", a);
            cmd.Parameters.AddWithValue("@days", b);
            cmd.Parameters.AddWithValue("@Pid", c);
            cmd.Parameters.AddWithValue("@Logo", "آي جولد للمصوغات والمجوهرات");
            cmd.Parameters.AddWithValue("@all", "الكل");

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return " تم الحفظ ";
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }

        }

        public string updateUser()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateUser";
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@passWord", passWord);
            cmd.Parameters.AddWithValue("@userId", userId);
          
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return " تم التعديل ";
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string checkUserExist()
        {
            string flag = "false";
            cmd.Parameters.Clear();
            cmd.CommandText = "checkUserExist";

            try
            {
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd["userName"].ToString() == name)
                    {
                        flag = "true";
                        break;
                    }
                }
                con.Close();
                return flag;
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string getUserCount()
        {
            int count = 0;
            string str;
            cmd.Parameters.Clear();
            cmd.CommandText = "getUserCount";

            try
            {
                con.Open();
                count = (int)cmd.ExecuteScalar() + 1;
                con.Close();
                return str = Convert.ToString(count);
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }

        }

        public int getUserId()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getUserId";
            cmd.Parameters.AddWithValue("@userName", userName);

            try
            {
                int a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = Convert.ToInt32(dr["userId"].ToString());
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
            }

        }

        public string getuserName()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getUserName";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
                string a="";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr["userName"].ToString();
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
            }

        }

        public string getPassOfName()
        {
            string flag = "true";
            cmd.Parameters.Clear();
            cmd.CommandText = "getPassOfName";
            cmd.Parameters.AddWithValue("@userName", userName);

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    flag = dr["passWord"].ToString();
                }
                con.Close();
                return flag;
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }

        }

        public string getN()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getN";

            try
            {
                string flag = "false";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                      flag = dr["accountNo"].ToString();
                }
                con.Close();
                int count = Convert.ToInt32(getUserCount())-1;
                if (Convert.ToInt32(flag) < count)
                    flag = "true";
                else
                    flag = "false";
                return flag;
            }

            catch (Exception ee)
            {
                con.Close();
                return ee.Message;
            }
        }

        public string updateN()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateN";
            cmd.Parameters.AddWithValue("@a", Convert.ToInt32(name));
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return " تم التعديل ";
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string updateUserName()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateUserName";
            cmd.Parameters.AddWithValue("@a", name);
            cmd.Parameters.AddWithValue("@userId", GlobalVar.id);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return " تم التعديل ";
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string getNo()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getN";

            try
            {
                string flag = "false";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    flag = dr["accountNo"].ToString();
                }
                con.Close();
                return flag;
            }

            catch (Exception ee)
            {
                con.Close();
                return ee.Message;
            }
        }
    }
}





