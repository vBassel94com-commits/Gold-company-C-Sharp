using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public class Customerdb_Class : dbClass
    {
        public string customerName;
        public string mobile;
        public string customerName1 { get => customerName; set => customerName = value; }
        public string mobile1 { get => mobile; set => mobile = value; }
        public string name;
        public string name1 { get => name; set => name = value; }
        public string customerId;
        public string customerId1 { get => customerId; set => customerId = value; }
        public decimal cashAdd;
        public decimal gold21Add;
        public decimal gold18Add;
        public decimal gold14Add;
        public decimal cashAdd1 { get => cashAdd; set => cashAdd = value; }
        public decimal gold21Add1 { get => gold21Add; set => gold21Add = value; }
        public decimal gold18Add1 { get => gold18Add; set => gold18Add = value; }
        public decimal gold14Add1 { get => gold14Add; set => gold14Add = value; }
        public int days;
        public int days1 { get => days; set => days = value; }

        public int userId;
        public int userId1 { get => userId; set => userId = value; }

        public DateTime startDay;
        public DateTime startDay1 { get => startDay; set => startDay = value; }
        //22
        public string insertCustomer()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "insertCustomer";
            cmd.Parameters.AddWithValue("@customerName", customerName);
            cmd.Parameters.AddWithValue("@customerMobile", mobile);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
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

        public string getCustomerCount()
        {
            int count = 0;
            string str;
            cmd.Parameters.Clear();
            cmd.CommandText = "getCustomerCount";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

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
        
        public string CheckCustomerNameExist()

        {
            string flag = "true";
            cmd.Parameters.Clear();
            cmd.CommandText = "CheckCustomerNameExist";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd["customerName"].ToString() == customerName)
                    {
                        flag = getMobileOfName(); ;
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

        public string getMobileOfName()
        {
            string flag = "true";
            cmd.Parameters.Clear();
            cmd.CommandText = "getMobileOfName";
            cmd.Parameters.AddWithValue("@customerName", customerName);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    flag = dr["customerMobile"].ToString();
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

        public string getMobOfNam()
        {
            string flag = "09";
            cmd.Parameters.Clear();
            cmd.CommandText = "getMobOfNam";
            cmd.Parameters.AddWithValue("@customerName", customerName);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    flag = dr["customerMobile"].ToString();
                }
                con.Close();
                return flag;
            }

            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
                return "09";
            }

        }

        public string updateCustomer()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateCustomer";
            cmd.Parameters.AddWithValue("@customerName", customerName);
            cmd.Parameters.AddWithValue("@customerMobile", mobile);
            cmd.Parameters.AddWithValue("@customerId", customerId);
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

        public string checkCustomerExist()
        {
            string flag = "false";
            cmd.Parameters.Clear();
            cmd.CommandText = "checkCustomerExist";
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd["customerName"].ToString() == name)
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

        public string insertCustomerFull()
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "insertCustomerFull";
            cmd.Parameters.AddWithValue("@customerName", name);
            cmd.Parameters.AddWithValue("@customerMobile", mobile);
            cmd.Parameters.AddWithValue("@cashAdd", cashAdd);
            cmd.Parameters.AddWithValue("@gold21Add", gold21Add);
            cmd.Parameters.AddWithValue("@gold18Add", gold18Add);
            cmd.Parameters.AddWithValue("@gold14Add", gold14Add);
            cmd.Parameters.AddWithValue("@logo", 0);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);


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

        public string updateLicences()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateLicences";
            cmd.Parameters.AddWithValue("@param1", days);
            cmd.Parameters.AddWithValue("@param2", startDay);

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

        public string updatePrinter()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updatePrinter";
            cmd.Parameters.AddWithValue("@param1", name);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
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

        public string getDefaultPrinter()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getDefaultPrinter";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                string a = "";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr["printer"].ToString();
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

		public string updateFlash()
		{
			cmd.Parameters.Clear();
			cmd.CommandText = "updateFlash";
			cmd.Parameters.AddWithValue("@param1", name);
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

		public string updateLogo()
		{
			cmd.Parameters.Clear();
			cmd.CommandText = "updateLogo";
			cmd.Parameters.AddWithValue("@param1", name);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
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

		public string getLogo()
		{
			cmd.Parameters.Clear();
			cmd.CommandText = "getLogo";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
				string a = "";
				con.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					a = dr["Logo"].ToString();
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

        public string updateItems()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateItems";
            cmd.Parameters.AddWithValue("@param1", name);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

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

        public string getItems1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getItems1";
            cmd.Parameters.AddWithValue("@id",GlobalVar.id);

            try
            {
                string a = "";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr["Items"].ToString();
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

        public string updateDaily()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateDaily";
            cmd.Parameters.AddWithValue("@param1", name);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
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

        public string getDaily()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getDaily";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
                string a = "";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr["Daily"].ToString();
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

        public string getDays()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getDays";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
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

        public string updatePayment1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updatePayment1";
            cmd.Parameters.AddWithValue("@param1", name);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
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

        public string getPayment1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getPayment1";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
                string a = "";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr["Payment"].ToString();
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

        public string getLastDaily()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getLastDaily";
            cmd.Parameters.AddWithValue("@id", customerId);
            try
            {
                string a = DateTime.Now.Date.AddDays(-1).ToString();
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr[0].ToString();
                }
                con.Close();
                if (a == "")
                    return a = DateTime.Now.Date.AddDays(-1).ToString();
                else
                return a;
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string getLastDaily1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getLastDaily1";
            cmd.Parameters.AddWithValue("@id", customerId);
            try
            {
                string a = DateTime.Now.Date.AddDays(-1).ToString();
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr[0].ToString();
                }
                con.Close();
                if (a == "")
                    return a = DateTime.Now.Date.AddDays(-1).ToString();
                else
                    return a;
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

    }
}
