using System;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace iGOLD
{
    public class paymentdb_Class : dbClass
    {
        public string customerName;
        public string convert;
        public string newConvert;
        public int paymentTypeInt;
        public int newType;
        public int primaryType;

        public string paymentTypeString;
        public string userName;
        public string itemName;
        public int paymentId;
        public int item21Id;
        public int item18Id;
        public int item14Id;
        public int item21Count;
        public int item18Count;
        public int item14Count;
        public int primary21Count;
        public int primary18Count;
        public int primary14Count;


        public decimal dailyCash;
        public decimal daily21;
        public decimal daily18;
        public decimal daily14;
        public decimal realNewPaymentCash;
        public decimal realNewPayment21;
        public decimal realNewPayment18;
        public decimal realNewPayment14;
        public decimal paymentCash;
        public decimal payment21;
        public decimal payment18;
        public decimal payment14;
        public decimal realPaymentCash;
        public decimal realPayment21;
        public decimal realPayment18;
        public decimal realPayment14;
        public decimal newPaymentCash;
        public decimal newPayment21;
        public decimal newPayment18;
        public decimal newPayment14;
        public DateTime paymentDateTime;
        public int firstBillId;
        public int lastBillId;
        public int firstPaymentId;
        public int lastPaymentId;
        public decimal cashAdd;
        public decimal gold21Add;
        public decimal gold18Add;
        public decimal gold14Add;
        public DateTime dateTim;
        public int customerId;
        public int userId;
        public decimal before;
        public decimal after;
        public decimal newBefore;
        public decimal newAfter;


        public string paymentNotice;
        public string paymentNotice1 { get => paymentNotice; set => paymentNotice = value; }

        public string paymentNo;
        public string paymentNo1 { get => paymentNo; set => paymentNo = value; }
        public int customerId1 { get => customerId; set => customerId = value; }
        public int userId1 { get => userId; set => userId = value; }
        public int item21Id1 { get => item21Id; set => item21Id = value; }
        public int item18Id1 { get => item18Id; set => item18Id = value; }
        public int item14Id1 { get => item14Id; set => item14Id = value; }
        public int item21Count1 { get => item21Count; set => item21Count = value; }
        public int item18Count1 { get => item18Count; set => item18Count = value; }
        public int item14Count1 { get => item14Count; set => item14Count = value; }
        public int primary21Count1 { get => primary21Count; set => primary21Count = value; }
        public int primary18Count1 { get => primary18Count; set => primary18Count = value; }
        public int primary14Count1 { get => primary14Count; set => primary14Count = value; }
        public string customerName1 { get => customerName; set => customerName = value; }
        public string convert1 { get => convert; set => convert = value; }
        public string newConvert1 { get => newConvert; set => newConvert = value; }
        public int paymentTypeInt1 { get => paymentTypeInt; set => paymentTypeInt = value; }
        public int newType1 { get => newType; set => newType = value; }
        public int primaryType1 { get => primaryType; set => primaryType = value; }
        public string paymentTypeString1 { get => paymentTypeString; set => paymentTypeString = value; }
        public string userName1 { get => userName; set => userName = value; }
        public string itemName1 { get => itemName; set => itemName = value; }
        public int paymentId1 { get => paymentId; set => paymentId = value; }
        public decimal paymentCash1 { get => paymentCash; set => paymentCash = value; }
        public decimal payment211 { get => payment21; set => payment21 = value; }
        public decimal payment181 { get => payment18; set => payment18 = value; }
        public decimal payment141 { get => payment14; set => payment14 = value; }
        public decimal realPaymentCash1 { get => realPaymentCash; set => realPaymentCash = value; }
        public decimal realPayment211 { get => realPayment21; set => realPayment21 = value; }
        public decimal realPayment181 { get => realPayment18; set => realPayment18 = value; }
        public decimal realPayment141 { get => realPayment14; set => realPayment14 = value; }
        public decimal before1 { get => before; set => before = value; }
        public decimal after1 { get => after; set => after = value; }
        public decimal newBefore1 { get => newBefore; set => newBefore = value; }
        public decimal newAfter1 { get => newAfter; set => newAfter = value; }
        public decimal newPaymentCash1 { get => newPaymentCash; set => newPaymentCash = value; }
        public decimal newPayment211 { get => newPayment21; set => newPayment21 = value; }
        public decimal newPayment181 { get => newPayment18; set => newPayment18 = value; }
        public decimal newPayment141 { get => newPayment14; set => newPayment14 = value; }

        public decimal realNewpaymentCash1 { get => realNewPaymentCash; set => realNewPaymentCash = value; }
        public decimal realNewpayment211 { get => realNewPayment21; set => realNewPayment21 = value; }
        public decimal realNewpayment181 { get => realNewPayment18; set => realNewPayment18 = value; }
        public decimal realNewpayment141 { get => realNewPayment14; set => realNewPayment14 = value; }

        public DateTime dateTim1 { get => dateTim; set => dateTim = value; }
        public DateTime paymentDateTime1 { get => paymentDateTime; set => paymentDateTime = value; }
        public decimal dailyCash1 { get => dailyCash; set => dailyCash = value; }
        public decimal daily211 { get => daily21; set => daily21 = value; }
        public decimal daily181 { get => daily18; set => daily18 = value; }
        public decimal daily141 { get => daily14; set => daily14 = value; }
        public int firstBillId1 { get => firstBillId; set => firstBillId = value; }
        public int lastBillId1 { get => lastBillId; set => lastBillId = value; }
        public int firstPaymentId1 { get => firstPaymentId; set => firstPaymentId = value; }
        public int lastPaymentId1 { get => lastPaymentId; set => lastPaymentId = value; }
        public decimal cashAdd1 { get => cashAdd; set => cashAdd = value; }
        public decimal gold21Add1 { get => gold21Add; set => gold21Add = value; }
        public decimal gold18Add1 { get => gold18Add; set => gold18Add = value; }
        public decimal gold14Add1 { get => gold14Add; set => gold14Add = value; }
        //22
        public string insertPayment()
        {

            cmd.Parameters.Clear();
           
            cmd.CommandText = "insertPayment";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@realPaymentCash", realPaymentCash);
            cmd.Parameters.AddWithValue("@realPayment21", realPayment21);
            cmd.Parameters.AddWithValue("@realPayment18", realPayment18);
            cmd.Parameters.AddWithValue("@realPayment14", realPayment14);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@paymentNo", paymentNo);
            cmd.Parameters.AddWithValue("@item21Id", item21Id);
            cmd.Parameters.AddWithValue("@item18Id", item18Id);
            cmd.Parameters.AddWithValue("@item14Id", item14Id);
            cmd.Parameters.AddWithValue("@item21Count", item21Count);
            cmd.Parameters.AddWithValue("@item18Count", item18Count);
            cmd.Parameters.AddWithValue("@item14Count", item14Count);

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

        public string addPayment1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "addPayment1";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return " تم اضافة الدفعة في حساب الصائغ ";
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }

        }

        public string addPayment()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "addPayment";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@item21Id", item21Id);
            cmd.Parameters.AddWithValue("@item18Id", item18Id);
            cmd.Parameters.AddWithValue("@item14Id", item14Id);
            cmd.Parameters.AddWithValue("@item21Count", item21Count);
            cmd.Parameters.AddWithValue("@item18Count", item18Count);
            cmd.Parameters.AddWithValue("@item14Count", item14Count);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return " تم اضافة الدفعة في حساب الصائغ " ;
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }

        }

        public string getPaymentId()
        {
            int count = 0;
            string str;
            cmd.Parameters.Clear();
            cmd.CommandText = "getPaymentId";

            try
            {
                con.Open();
                count = (int)cmd.ExecuteScalar();
                con.Close();
                return str = Convert.ToString(count);
            }

            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }

        }

        public string subPayment1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "subPayment1";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);

            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return " تم حذف الدفعة من حساب الصائغ ";
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }

        }

        public string getPaymentsCount()
        {
            int count = 0;
            string str;
            cmd.Parameters.Clear();
            cmd.CommandText = "getPaymentsCount";
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

        public string updatePayment()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updatePayment";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@realPaymentCash", realPaymentCash);
            cmd.Parameters.AddWithValue("@realPayment21", realPayment21);
            cmd.Parameters.AddWithValue("@realPayment18", realPayment18);
            cmd.Parameters.AddWithValue("@realPayment14", realPayment14);
            cmd.Parameters.AddWithValue("@paymentId", paymentId);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@paymentNo", paymentNo);
            cmd.Parameters.AddWithValue("@item21Count", item21Count);
            cmd.Parameters.AddWithValue("@item18Count", item18Count);
            cmd.Parameters.AddWithValue("@item14Count", item14Count);
            cmd.Parameters.AddWithValue("@item21Id", item21Id);
            cmd.Parameters.AddWithValue("@item18Id", item18Id);
            cmd.Parameters.AddWithValue("@item14Id", item14Id);

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

        public int GetCustomerid()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getCustomerid";
            cmd.Parameters.AddWithValue("@customerName", customerName);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                int a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = Convert.ToInt32(dr["customerId"].ToString());
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
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

        public decimal getDailyCash()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getDailyCash";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
                decimal a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = Convert.ToDecimal(dr[0]);
                }
                con.Close();
                return a;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                decimal a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = Convert.ToDecimal(dr[0]);
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
            }

        }

        public decimal getDaily21()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getDaily21";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                decimal a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = Convert.ToDecimal(dr[0]);
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
            }

        }

        public decimal getDaily18()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getDaily18";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
                decimal a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = Convert.ToDecimal(dr[0]);
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
            }

        }

        public decimal getDaily14()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getDaily14";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
                decimal a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = Convert.ToDecimal(dr[0]);
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
            }

        }

        public int getFirstBillId()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getFirstBillId";

            try
            {
                int a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {   
                        a = Convert.ToInt32(dr[0]) + 1;
                    }
                    else
                    {
                        a = 1;  
                    }               
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
            }

        }

        public int getFirstPaymentId()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getFirstPaymentId";

            try
            {
                int a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        try
                        {
                            a = Convert.ToInt32(dr[0]) + 1;
                        }
                        catch(Exception ex) { MessageBox.Show(ex.Message); }
                    }
                    else
                    {
                        a = 1;
                    }
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
            }

        }

        public string openDaily()
        {
            cmd.CommandText = "openDaily";
            try
            {
                cmd.Parameters.AddWithValue("@dateTim", dateTim);
                cmd.Parameters.AddWithValue("@dailyCash", dailyCash);
                cmd.Parameters.AddWithValue("@daily21", daily21);
                cmd.Parameters.AddWithValue("@daily18", daily18);
                cmd.Parameters.AddWithValue("@daily14", daily14);
                cmd.Parameters.AddWithValue("@firstBillId", firstBillId);
                cmd.Parameters.AddWithValue("@firstPaymentId", firstPaymentId);
                cmd.Parameters.AddWithValue("@lastBillId", lastBillId);
                cmd.Parameters.AddWithValue("@lastPaymentId", lastPaymentId);
                cmd.Parameters.AddWithValue("@cashAdd", cashAdd);
                cmd.Parameters.AddWithValue("@gold21Add", gold21Add);
                cmd.Parameters.AddWithValue("@gold18Add", gold18Add);
                cmd.Parameters.AddWithValue("@gold14Add", gold14Add);
                cmd.Parameters.AddWithValue("@id", GlobalVar.id);
                con.Open();
                cmd.ExecuteNonQuery().ToString();
                return "تم الافتتاح";
            }
            catch
            {
                con.Close();
                return "تم الافتتاح سابقاً";
            }
            finally
            {
                con.Close();
            }
        }

        public string checkTodayOpen()
        {
            string flag = "true";
            cmd.CommandText = "checkTodayOpen";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@dateTim", dateTim);
            cmd.Parameters.AddWithValue("@userId", userId);
            try
            {
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (Convert.ToDateTime(rd["datedate"]) == dateTim)
                    {
                        flag = "false";
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

        public string deletePaymentDetails()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "deletePaymentDetails";
            cmd.Parameters.AddWithValue("@paymentId", paymentId);

            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "تم الحذف";
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string insertEditPayment()
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "insertEditPayment";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentId", paymentId);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@paymentNo", paymentNo);
            cmd.Parameters.AddWithValue("@primaryType", primaryType);
            cmd.Parameters.AddWithValue("@newType", newType);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@newPaymentCash", newPaymentCash);
            cmd.Parameters.AddWithValue("@newPayment21", newPayment21);
            cmd.Parameters.AddWithValue("@newPayment18", newPayment18);
            cmd.Parameters.AddWithValue("@newPayment14", newPayment14);
            cmd.Parameters.AddWithValue("@realPaymentCash", realPaymentCash);
            cmd.Parameters.AddWithValue("@realPayment21", realPayment21);
            cmd.Parameters.AddWithValue("@realPayment18", realPayment18);
            cmd.Parameters.AddWithValue("@realPayment14", realPayment14);
            cmd.Parameters.AddWithValue("@realNewPaymentCash", realNewPaymentCash);
            cmd.Parameters.AddWithValue("@realNewPayment21", realNewPayment21);
            cmd.Parameters.AddWithValue("@realNewPayment18", realNewPayment18);
            cmd.Parameters.AddWithValue("@realNewPayment14", realNewPayment14);
            cmd.Parameters.AddWithValue("@item21Id", item21Id);
            cmd.Parameters.AddWithValue("@item18Id", item18Id);
            cmd.Parameters.AddWithValue("@item14Id", item14Id);
            cmd.Parameters.AddWithValue("@item21Count", item21Count);
            cmd.Parameters.AddWithValue("@item18Count", item18Count);
            cmd.Parameters.AddWithValue("@item14Count", item14Count);
            cmd.Parameters.AddWithValue("@primary21Count", primary21Count);
            cmd.Parameters.AddWithValue("@primary18Count", primary18Count);
            cmd.Parameters.AddWithValue("@primary14Count", primary14Count);

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

        public int getItemId()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getItemId";
            cmd.Parameters.AddWithValue("@itemName", itemName);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
                int a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = Convert.ToInt32(dr["itemId"].ToString());
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
            }

        }

        public string insertPaymentDetails()
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "insertPaymentDetails";
            cmd.Parameters.AddWithValue("@paymentId", paymentId);
            cmd.Parameters.AddWithValue("@before", before);
            cmd.Parameters.AddWithValue("@after", after);
            cmd.Parameters.AddWithValue("@convert", convert);
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

        public string insertBillConvertDetails()
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "insertBillConvertDetails";
            cmd.Parameters.AddWithValue("@paymentId", paymentId);
            cmd.Parameters.AddWithValue("@before", before);
            cmd.Parameters.AddWithValue("@after", after);
            cmd.Parameters.AddWithValue("@convert", convert);
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

        public string insertEditPaymentDetails()
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "insertEditPaymentDetails";
            cmd.Parameters.AddWithValue("@paymentId", paymentId);
            cmd.Parameters.AddWithValue("@newBefore", newBefore);
            cmd.Parameters.AddWithValue("@newAfter", newAfter);
            cmd.Parameters.AddWithValue("@newConvert", newConvert);
            cmd.Parameters.AddWithValue("@before", before);
            cmd.Parameters.AddWithValue("@after", after);
            cmd.Parameters.AddWithValue("@convert", convert);
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

        public string insertEditBillConvertDetails()
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "insertEditBillConvertDetails";
            cmd.Parameters.AddWithValue("@paymentId", paymentId);
            cmd.Parameters.AddWithValue("@newBefore", newBefore);
            cmd.Parameters.AddWithValue("@newAfter", newAfter);
            cmd.Parameters.AddWithValue("@newConvert", newConvert);
            cmd.Parameters.AddWithValue("@before", before);
            cmd.Parameters.AddWithValue("@after", after);
            cmd.Parameters.AddWithValue("@convert", convert);
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

        public string insertDailyCustomer()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "insertDailyCustomer";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@userId", GlobalVar.id);
            cmd.Parameters.AddWithValue("@dailyCash", dailyCash);
            cmd.Parameters.AddWithValue("@daily21", daily21);
            cmd.Parameters.AddWithValue("@daily18", daily18);
            cmd.Parameters.AddWithValue("@daily14", daily14);
            cmd.Parameters.AddWithValue("@dat", dateTim);
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

        public string checkDailyCustomer()
        {
            string flag = "true";
            cmd.CommandText = "checkDailyCustomer";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@dateTim", dateTim);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            try
            {
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (Convert.ToDateTime(rd["date"]) == dateTim1 && Convert.ToInt32(rd["customerId"]) == customerId)
                    {
                        flag = "false";
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

        public string insertUDailyCustomer()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "insertUDailyCustomer";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@userId", GlobalVar.id);
            cmd.Parameters.AddWithValue("@dailyCash", dailyCash);
            cmd.Parameters.AddWithValue("@daily21", daily21);      
            cmd.Parameters.AddWithValue("@dat", dateTim);
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

        public string checkUDailyCustomer()
        {
            string flag = "true";
            cmd.CommandText = "checkUDailyCustomer";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@dateTim", dateTim);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            try
            {
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (Convert.ToDateTime(rd["date"]) == dateTim1 && Convert.ToInt32(rd["customerId"]) == customerId)
                    {
                        flag = "false";
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

        public string updateCustomerAccount()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateCustomerAccount";
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@cash", dailyCash1);
            cmd.Parameters.AddWithValue("@c21", daily211);
            cmd.Parameters.AddWithValue("@c14", daily141);
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
    }
}
