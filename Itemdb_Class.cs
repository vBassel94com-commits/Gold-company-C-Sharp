using System;
using System.Data.SqlClient;

namespace iGOLD
{
    public class Itemdb_Class : dbClass
    {
        public string itemName;
        public string itemName1 { get => itemName; set => itemName = value; }
        public decimal itemWeight;
        public decimal itemWeight1 { get => itemWeight; set => itemWeight = value; }
        public int itemCarat;
        public int itemCarat1 { get => itemCarat; set => itemCarat = value; }
        public decimal newitemWeight;
        public decimal newitemWeight1 { get => newitemWeight; set => newitemWeight = value; }
        public int newitemCarat;
        public int newitemCarat1 { get => newitemCarat; set => newitemCarat = value; }
        public decimal discountAmount;
        public decimal discountAmount1 { get => discountAmount; set => discountAmount = value; }
        public string discountType;
        public string discountType1 { get => discountType; set => discountType = value; }
        public decimal newdiscountAmount;
        public decimal newdiscountAmount1 { get => newdiscountAmount; set => newdiscountAmount = value; }
        public int itemCount;
        public int itemCount1 { get => itemCount; set => itemCount = value; }
        public decimal itemFees;
        public decimal itemFees1 { get => itemFees; set => itemFees = value; }
        public int newitemCount;
        public int newitemCount1 { get => newitemCount; set => newitemCount = value; }
        public decimal newitemFees;
        public decimal newitemFees1 { get => newitemFees; set => newitemFees = value; }
        public decimal openWeight;
        public decimal openWeight1 { get => openWeight; set => openWeight = value; }
        public decimal difWeight;
        public decimal difWeight1 { get => difWeight; set => difWeight = value; }
        public int openCount;
        public int openCount1 { get => openCount; set => openCount = value; }
        public decimal openFees;
        public decimal openFees1 { get => openFees; set => openFees = value; }
        public decimal cash;
        public decimal cash1 { get => cash; set => cash = value; }
        public decimal itemTotalFees;
        public decimal itemTotalFees1 { get => itemTotalFees; set => itemTotalFees = value; }
        public decimal newitemTotalFees;
        public decimal newitemTotalFees1 { get => newitemTotalFees; set => newitemTotalFees = value; }
        public int itemId;
        public int itemId1 { get => itemId; set => itemId = value; }
        public int billId;
        public int billId1 { get => billId; set => billId = value; }
        public string billNo;
        public string billNo1 { get => billNo; set => billNo = value; }
        public string billType;
        public string billType1 { get => billType; set => billType = value; }
        public int customerId;
        public int customerId1 { get => customerId; set => customerId = value; }
        public string customerName;
        public string customerName1 { get => customerName; set => customerName = value; }
        public int userId;
        public int userId1 { get => userId; set => userId = value; }
        public string userName;
        public string userName1 { get => userName; set => userName = value; }
        public decimal billTotalCash;
        public decimal billTotalCash1 { get => billTotalCash; set => billTotalCash = value; }
        public decimal billTotal21;
        public decimal billTotal211 { get => billTotal21; set => billTotal21 = value; }
        public decimal billTotal18;
        public decimal billTotal181 { get => billTotal18; set => billTotal18 = value; }
        public decimal billTotal14;
        public decimal billTotal141 { get => billTotal14; set => billTotal14 = value; }
        public decimal newbillTotalCash;
        public decimal newbillTotalCash1 { get => newbillTotalCash; set => newbillTotalCash = value; }
        public decimal newbillTotal21;
        public decimal newbillTotal211 { get => newbillTotal21; set => newbillTotal21 = value; }
        public decimal newbillTotal18;
        public decimal newbillTotal181 { get => newbillTotal18; set => newbillTotal18 = value; }
        public decimal newbillTotal14;
        public decimal newbillTotal141 { get => newbillTotal14; set => newbillTotal14 = value; }
        public DateTime dateT;
        public DateTime dateT1 { get => dateT; set => dateT = value; }
        public int paymentTypeInt;
        public string paymentTypeString;
        public string paymentId;
        public decimal dailyCash;
        public decimal daily21;
        public decimal daily18;
        public decimal daily14;
        public decimal paymentCash;
        public decimal payment21;
        public decimal payment18;
        public decimal payment14;
        public DateTime paymentDateTime;
        public int firstBillId;
        public int lastBillId;
        public int firstPaymentId;
        public int lastPaymentId;
        public decimal cashAdd;
        public decimal gold21Add;
        public decimal gold18Add;
        public decimal gold14Add;
        public int item21Id;
        public int item18Id;
        public int item14Id;
        public DateTime dateTim;
        public string paymentNotice;
        public string paymentNotice1 { get => paymentNotice; set => paymentNotice = value; }
        public string billNotice;
        public string billNotice1 { get => billNotice; set => billNotice = value; }
        public int paymentTypeInt1 { get => paymentTypeInt; set => paymentTypeInt = value; }
        public string paymentTypeString1 { get => paymentTypeString; set => paymentTypeString = value; }
        public string paymentId1 { get => paymentId; set => paymentId = value; }
        public decimal paymentCash1 { get => paymentCash; set => paymentCash = value; }
        public decimal payment211 { get => payment21; set => payment21 = value; }
        public decimal payment181 { get => payment18; set => payment18 = value; }
        public decimal payment141 { get => payment14; set => payment14 = value; }
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
        public int item21Id1 { get => item21Id; set => item21Id = value; }
        public int item18Id1 { get => item18Id; set => item18Id = value; }
        public int item14Id1 { get => item14Id; set => item14Id = value; }
        public string editBillNotice;
        public string editBillNotice1 { get => editBillNotice; set => editBillNotice = value; }


        //46
        public string checkCustomerExist()
        {
            string flag = "false";
            cmd.Parameters.Clear();
            cmd.CommandText = "checkCustomerExist";
            cmd.Parameters.AddWithValue("@name", customerName);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd["customerName"].ToString() == customerName)
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

        public string insertItem21()
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "insertItem21";
            cmd.Parameters.AddWithValue("@itemName", itemName);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCarat", itemCarat);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@itemFees", itemFees);
            cmd.Parameters.AddWithValue("@openWeight", openWeight);
            cmd.Parameters.AddWithValue("@openCount", openCount);
            cmd.Parameters.AddWithValue("@openFees", openFees);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@item21Id", item21Id);
            cmd.Parameters.AddWithValue("@item18Id", item18Id);
            cmd.Parameters.AddWithValue("@item14Id", item14Id);

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

        public string insertItem18()
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "insertItem18";
            cmd.Parameters.AddWithValue("@itemName", itemName);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCarat", itemCarat);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@itemFees", itemFees);
            cmd.Parameters.AddWithValue("@openWeight", openWeight);
            cmd.Parameters.AddWithValue("@openCount", openCount);
            cmd.Parameters.AddWithValue("@openFees", openFees);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@item21Id", item21Id);
            cmd.Parameters.AddWithValue("@item18Id", item18Id);
            cmd.Parameters.AddWithValue("@item14Id", item14Id);
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

        public string insertItem14()
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "insertItem14";
            cmd.Parameters.AddWithValue("@itemName", itemName);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCarat", itemCarat);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@itemFees", itemFees);
            cmd.Parameters.AddWithValue("@openWeight", openWeight);
            cmd.Parameters.AddWithValue("@openCount", openCount);
            cmd.Parameters.AddWithValue("@openFees", openFees);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@item21Id", item21Id);
            cmd.Parameters.AddWithValue("@item18Id", item18Id);
            cmd.Parameters.AddWithValue("@item14Id", item14Id);
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

        public bool checkItemCaratExist()
        {
            bool flag = true;
            cmd.Parameters.Clear();
            cmd.CommandText = "checkItemCaratExist";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);

            try
            {
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd["itemName"].Equals(itemName) && rd["itemCarat"].Equals(itemCarat))
                    {
                        flag = false;
                        break;
                    }
                }
                con.Close();
                return flag;
            }
            catch
            {
                con.Close();
                return false;
            }
        }

        public string getItemsCount()
        {
            int count = 0;
            string str;
            cmd.Parameters.Clear();
            cmd.CommandText = "getItemsCount";

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

        public string updateItem21()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateItem21";
            cmd.Parameters.AddWithValue("@itemName", itemName);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCarat", itemCarat);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@itemFees", itemFees);
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@difWeight", difWeight);

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

        public string updateItem18()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateItem18";
            cmd.Parameters.AddWithValue("@itemName", itemName);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCarat", itemCarat);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@itemFees", itemFees);
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@difWeight", difWeight);

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

        public string updateItem14()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateItem14";
            cmd.Parameters.AddWithValue("@itemName", itemName);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCarat", itemCarat);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@itemFees", itemFees);
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@difWeight", difWeight);

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

        public string deleteItem21()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "deleteItem21";
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@difWeight", difWeight);

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

        public string deleteItem18()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "deleteItem18";
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@difWeight", difWeight);

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

        public string deleteItem14()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "deleteItem14";
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@paymentTypeInt", paymentTypeInt);
            cmd.Parameters.AddWithValue("@paymentTypeString", paymentTypeString);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@paymentCash", paymentCash);
            cmd.Parameters.AddWithValue("@payment21", payment21);
            cmd.Parameters.AddWithValue("@payment18", payment18);
            cmd.Parameters.AddWithValue("@payment14", payment14);
            cmd.Parameters.AddWithValue("@paymentDateTime", paymentDateTime);
            cmd.Parameters.AddWithValue("@paymentNotice", paymentNotice);
            cmd.Parameters.AddWithValue("@difWeight", difWeight);

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

        public string getBillsCount()
        {
            int count = 0;
            string str;
            cmd.Parameters.Clear();
            cmd.CommandText = "getBillsCount";

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

        public string getBillId()
        {
            int count = 0;
            string str;
            cmd.Parameters.Clear();
            cmd.CommandText = "getBillId";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
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

        public void insertBillDetails()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "insertBillDetails";
            cmd.Parameters.AddWithValue("@billId", billId);
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCarat", itemCarat);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@itemFees", itemFees);
            cmd.Parameters.AddWithValue("@itemTotalFees", itemTotalFees);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch
            {
                con.Close();

            }
        }

        public string insertBill()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "insertBill";
            cmd.Parameters.AddWithValue("@billType", billType);
            cmd.Parameters.AddWithValue("@billNo", billNo);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@billTotalCash", billTotalCash);
            cmd.Parameters.AddWithValue("@billTotal21", billTotal21);
            cmd.Parameters.AddWithValue("@billTotal18", billTotal18);
            cmd.Parameters.AddWithValue("@billTotal14", billTotal14);
            cmd.Parameters.AddWithValue("@dateT", dateT);
            cmd.Parameters.AddWithValue("@billDetail", billNotice);
            cmd.Parameters.AddWithValue("@discountAmount", discountAmount);
            cmd.Parameters.AddWithValue("@discountType", discountType);

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

        public int GetCustomerid()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getCustomerid";
            cmd.Parameters.AddWithValue("@customerName", customerName);
            cmd.Parameters.AddWithValue("id", GlobalVar.id);
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

        public void iItem()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "iItem";
            cmd.Parameters.AddWithValue("@billType", billType);
            if (billType == "بيع")
            {
                sellItem();
            }
            else
            {
                buyItem();
            }
        }

        public string buyItem()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "buyItem";
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@itemFees", itemFees);

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

        public string buyItem1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "buyItem1";
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);

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

        public string sellItem()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "sellItem";
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);

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

        public string checkitemExist()
        {
            string flag = "false";
            cmd.Parameters.Clear();
            cmd.CommandText = "checkItemExist";
            cmd.Parameters.AddWithValue("@name", itemName);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd["itemName"].ToString() == itemName)
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

        public string addCashToFonding()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "addCashToFonding";
            cmd.Parameters.AddWithValue("@cash", cash);
            cmd.Parameters.AddWithValue("id", GlobalVar.id);
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

        public string addGoldToFonding()
        {
            if (itemCarat == 14)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "addGold14ToFonding";
                cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
                cmd.Parameters.AddWithValue("id", GlobalVar.id);

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

            else if (itemCarat == 18)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "addGold18ToFonding";
                cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
                cmd.Parameters.AddWithValue("id", GlobalVar.id);

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

            else if (itemCarat == 21)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "addGold21ToFonding";
                cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
                cmd.Parameters.AddWithValue("id", GlobalVar.id);

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

            else { return "تأكد من عيار المصاغ"; }
        }

        public string subCashFromFonding()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "subCashFromFonding";
            cmd.Parameters.AddWithValue("@cash", cash);
            cmd.Parameters.AddWithValue("id", GlobalVar.id);

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

        public string subGoldFromFonding()
        {
            if (itemCarat == 14)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "subGold14FromFonding";
                cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
                cmd.Parameters.AddWithValue("id", GlobalVar.id);

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

            else if (itemCarat == 18)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "subGold18FromFonding";
                cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
                cmd.Parameters.AddWithValue("id", GlobalVar.id);

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

            else if (itemCarat == 21)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "subGold21FromFonding";
                cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
                cmd.Parameters.AddWithValue("id", GlobalVar.id);

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

            else { return "تأكد من عيار المصاغ"; }
        }

        public string insertDiscount()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "insertDiscount";
            cmd.Parameters.AddWithValue("@billType", billType);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@billId", billId);
            cmd.Parameters.AddWithValue("@discountAmount", discountAmount);
            cmd.Parameters.AddWithValue("@discountType", discountType);
            cmd.Parameters.AddWithValue("@discountDateTime", dateT);

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

        public string addMyDiscount()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "addMyDiscount";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            cmd.Parameters.AddWithValue("@discountAmount", discountAmount);
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

        public string addForDiscount()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "addForDiscount";
            cmd.Parameters.AddWithValue("@discountAmount", discountAmount);
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

        public decimal getItemCount()
        {
            decimal count = 0;
            cmd.Parameters.Clear();
            cmd.CommandText = "getItemCount";
            cmd.Parameters.AddWithValue("@itemId", itemId);
            try
            {

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    count = Convert.ToDecimal(dr["itemCount"].ToString());
                }
                con.Close();
                return count;
            }

            finally
            {
                con.Close();
            }
        }

        public decimal getItemFees()
        {
            decimal count = 0;
            cmd.Parameters.Clear();
            cmd.CommandText = "getItemFees";
            cmd.Parameters.AddWithValue("@itemId", itemId);

            try
            {

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    count = Convert.ToDecimal(dr["itemFees"].ToString());
                }
                con.Close();
                return count;
            }

            finally
            {
                con.Close();
            }
        }

        public decimal getItemWeight()
        {
            decimal count = 0;
            cmd.Parameters.Clear();
            cmd.CommandText = "getItemWeight";
            cmd.Parameters.AddWithValue("@itemId", itemId);

            try
            {

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    count = Convert.ToDecimal(dr["itemWeight"].ToString());
                }
                con.Close();
                return count;
            }

            finally
            {
                con.Close();
            }
        }

        public string deleteBill()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "deleteBill";
            cmd.Parameters.AddWithValue("@billId", billId);
            cmd.Parameters.AddWithValue("@billDateTime", dateT);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@billType", billType);
            cmd.Parameters.AddWithValue("@discountType", discountType);
            cmd.Parameters.AddWithValue("@billDetail", billNotice);
            cmd.Parameters.AddWithValue("@billNo", billNo);
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

        public string deleteBillDetails1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "deleteBillDetails";
            cmd.Parameters.AddWithValue("@billId", billId);

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

        public string deleteBillConvertDetails1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "deleteBillConvertDetails";
            cmd.Parameters.AddWithValue("@billId", billId);

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


        public int getItemId1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getItemId1";
            try
            {
                int a = 0;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.IsDBNull(0))
                    {
                        a = 0;
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
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

        public void insertEditBillDetails()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "insertEditBillDetails";
            cmd.Parameters.AddWithValue("@billId", billId);
            cmd.Parameters.AddWithValue("@itemId", itemId);
            cmd.Parameters.AddWithValue("@itemWeight", itemWeight);
            cmd.Parameters.AddWithValue("@itemCarat", itemCarat);
            cmd.Parameters.AddWithValue("@itemCount", itemCount);
            cmd.Parameters.AddWithValue("@itemFees", itemFees);
            cmd.Parameters.AddWithValue("@itemTotalFees", itemTotalFees);
            cmd.Parameters.AddWithValue("@newitemWeight", newitemWeight);
            cmd.Parameters.AddWithValue("@newitemCarat", newitemCarat);
            cmd.Parameters.AddWithValue("@newitemCount", newitemCount);
            cmd.Parameters.AddWithValue("@newitemFees", newitemFees);
            cmd.Parameters.AddWithValue("@newitemTotalFees", newitemTotalFees);
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                con.Close();
            }
        }

        public string insertEditBill()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "insertEditBill";
            cmd.Parameters.AddWithValue("@billId", billId);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@billType", billType);
            cmd.Parameters.AddWithValue("@billTotalCash", billTotalCash);
            cmd.Parameters.AddWithValue("@billTotal21", billTotal21);
            cmd.Parameters.AddWithValue("@billTotal18", billTotal18);
            cmd.Parameters.AddWithValue("@billTotal14", billTotal14);
            cmd.Parameters.AddWithValue("@discountAmount", discountAmount);
            cmd.Parameters.AddWithValue("@newbillTotalCash", newbillTotalCash);
            cmd.Parameters.AddWithValue("@newbillTotal21", newbillTotal21);
            cmd.Parameters.AddWithValue("@newbillTotal18", newbillTotal18);
            cmd.Parameters.AddWithValue("@newbillTotal14", newbillTotal14);
            cmd.Parameters.AddWithValue("@newdiscountAmount", newdiscountAmount);
            cmd.Parameters.AddWithValue("@dateT", dateT);
            cmd.Parameters.AddWithValue("@editBillNotice", editBillNotice);

            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "تم التعديل";
            }
            catch(Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string updateDiscount()
        {

            if ((billType == "بيع") && (discountType == "-1") || (billType == "شراء") && (discountType == "+1"))
            {
                addForDiscount();
            }
            else if ((billType == "شراء") && (discountType == "-1") || (billType == "بيع") && (discountType == "+1"))
            {
                addMyDiscount();
            }

            cmd.Parameters.Clear();
            cmd.CommandText = "updateDiscount";
            cmd.Parameters.AddWithValue("@billId", billId);
            cmd.Parameters.AddWithValue("@newdiscountAmount", newdiscountAmount);
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

        public string updateBill()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "updateBill";

            cmd.Parameters.AddWithValue("@billId", billId);
            cmd.Parameters.AddWithValue("@billNo", billNo);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@billDetail", billNotice);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@billTotalCash", billTotalCash);
            cmd.Parameters.AddWithValue("@billTotal21", billTotal21);
            cmd.Parameters.AddWithValue("@billTotal18", billTotal18);
            cmd.Parameters.AddWithValue("@billTotal14", billTotal14);
            cmd.Parameters.AddWithValue("@discountAmount", discountAmount);
            cmd.Parameters.AddWithValue("@discountType", discountType);
            cmd.Parameters.AddWithValue("@billType", billType);

            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "تم تعديل الفاتورة";
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string getItemName()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getItemName";
            cmd.Parameters.AddWithValue("@itemId", itemId);
            try
            {
                string a = "";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr["itemName"].ToString();
                }
                con.Close();
                return a;
            }

            finally
            {
                con.Close();
            }
        }

        public int getItemCarat()
        {
            int count = 0;
            cmd.Parameters.Clear();
            cmd.CommandText = "getItemCarat";
            cmd.Parameters.AddWithValue("@itemName", itemName);
            try
            {

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    count = Convert.ToInt32(dr["itemCarat"].ToString());
                }
                con.Close();
                return count;
            }

            finally
            {
                con.Close();
            }
        }

        public string getBillId1()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "getBillId1";
            cmd.Parameters.AddWithValue("@billNo", billNo);
            try
            {
                string a = "0";
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = dr["billId"].ToString().Trim();
                }
                con.Close();
                return a;

            }

            finally
            {
                con.Close();
            }
        }

        public string deleteAll()
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "deleteAll";
            cmd.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "تم افراغ قاعدة البيانات بالكامل";
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

        public string deleteAllUser()
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "deleteAllUser";
            cmd.Parameters.AddWithValue("@id",GlobalVar.id);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "تم افراغ قاعدة البيانات المستخدم";
            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

    }
}