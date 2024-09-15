using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace iGOLD
{
     public class GlobalVar
    {
        public static string userType = "";
        public static string defaultPrinter = "Microsoft Print to PDF";
        public static string pid = "7646493188B702";
        public static string Logo = "للمصوغات والمجوهرات iGOLD";
        public static string Items = "مصاغ";
        public static string Daily = "فاتورة";
        public static string Payment = "دفعة";
        public static string billStatus = "ادخال";

        public static bool flash = true;
        public static bool menu = false;
        public static bool load = true;
        public static bool paymentType = true;
        public static bool isMainMax = false;
        public static bool outcomeisMainMax = true;
        public static bool customerDetailsAccountisMainMax = true;
        public static bool customerEntryisMainMax = true;
        public static bool billsisMainMax = true;
        public static bool itemEntryisMainMax = true;
        public static bool PaymentisMainMax = true;        
        public static bool goldPriceIsMainMax = true;
        public static bool dailyPaymentisMainMax = true;
        public static bool userAccountisMainMax = true;
        public static bool companyTotalAccountisMainMax = true;
        public static bool customerTotalAccountisMainMax = true;
        public static bool editCustomerisMainMax = false;
        public static bool editItemisMainMax = true;
        public static bool editPaymentisMainMax = false;
        public static bool newDBisMainMax = true;
        public static bool detailsAccountingisMainMax = true;
        //public static string dataBaseLocation;// = GlobalVar.dataBaseLocation = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =d:\\iGOLD_DB.mdf;  Integrated Security = True;database=Db1";
        public static string name = "";
        public static string mob = "";
        public static string item = "";
        public static Color closeHoverColor = Color.Red;
        public static Color minMaxHoverColor = Color.DimGray;
        public static Color leaveColor = Color.FromArgb(32, 38, 35);
        public static Color barHoverColor = Color.Black;
        public static Color menuHoverColor = Color.Yellow;
        public static Color headerDgvColor = Color.Khaki;

        public static string editBackTo = "customerPayment";
        public static bool fromDetialsAccounting = false;
        public static string editPaymentId = "";
        public static string editBillId = "";
        public static string cash="0";
        public static string gold21="0";
        public static string gold18="0";
        public static string gold14 = "0";

        public static string dataBaseLocation = GlobalVar.dataBaseLocation = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =D:\\windows\\i\\iGOLD_DB.mdf;  Integrated Security = True;database=Db1";
        public static string userName = "mohammad";
        public static int id = 2;
        public static bool status_value = true;

        public static AutoCompleteStringCollection Auto_items;
        public static AutoCompleteStringCollection Auto_customers;
        //public static DaaTable dNames ;

        public static bool isGold21 = true;
        public static bool isGold18 = false;
        public static bool isGold14 = false;
        public static bool isCash = true;

        public static string gold21Label = "ذهب21";
        public static string gold18Label = "ذهب18";
        public static string gold14Label = "ذهب14";
        public static string cashLabel = "اجور";

        public static string gramLabel = "غرام";
        public static string currencyLabel = "ل.س";


    }
}
