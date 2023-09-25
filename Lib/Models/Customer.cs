using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class Get_API_StockFilter_Response
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public int SORT_NO { get; set; }
        public string Type { get; set; }
        public bool isActive { get; set; }
    }
    public class Get_API_ColumnMas_Response
    {
        public long SEQ_NO { get; set; }
        public string COLUMN_NAME { get; set; }
        public string DISPLAY_NAME { get; set; }
        public string sCustMiseCaption { get; set; }
        public string PARA_SYNONYM { get; set; }
        public int SORT_NO { get; set; }
        public bool VISIBLE { get; set; }
    }
    public class ApiUploadMethod
    {
        public int? iTransId { get; set; }
        public int? iUserId { get; set; }
        public string ApiMethod { get; set; }
        public string WebAPIUserName { get; set; }
        public string WebAPIPassword { get; set; }
        public string FTPHost { get; set; }
        public string FTPUser { get; set; }
        public string FTPPass { get; set; }
        public string FTPType { get; set; }
        public string FTPExportType { get; set; }
        public string URLUserName { get; set; }
        public string URLPassword { get; set; }
        public string URLExportType { get; set; }
        public string FileLocation { get; set; }
        public string LocationExportType { get; set; }
        public string LocationTransType { get; set; }
        public DateTime OnetimeDate { get; set; }
        public string Onetime { get; set; }
        public string RepeateveryType { get; set; }
        public decimal Repeatevery { get; set; }
        public string APIName { get; set; }
        public string APIUrl { get; set; }
        public bool APIStatus { get; set; }
        public int For_iUserId { get; set; }
        public List<APIFiltersSettings> APIFilters { get; set; }
        public List<ApiColumnsSettings> ColumnsSettings { get; set; }
        public ApiUploadMethod()
        {
            APIFilters = new List<APIFiltersSettings>();
            ColumnsSettings = new List<ApiColumnsSettings>();
        }
    }
    public class APIFiltersSettings
    {
        public long? Id { get; set; }
        public string Sr { get; set; }
        public long? iTransId { get; set; }
        public string iSupplier { get; set; }
        public string iLocation { get; set; }
        public string sShape { get; set; }
        public string sPointer { get; set; }
        public string sColor { get; set; }
        public string sClarity { get; set; }
        public string sCut { get; set; }
        public string sPolish { get; set; }
        public string sSymm { get; set; }
        public string sFls { get; set; }
        public string sLab { get; set; }
        public Single? dFromLength { get; set; }
        public Single? dToLength { get; set; }
        public Single? dFromWidth { get; set; }
        public Single? dToWidth { get; set; }
        public Single? dFromDepth { get; set; }
        public Single? dToDepth { get; set; }
        public Single? dFromDepthPer { get; set; }
        public Single? dToDepthPer { get; set; }
        public Single? dFromTablePer { get; set; }
        public Single? dToTablePer { get; set; }
        public Single? dFromCrAng { get; set; }
        public Single? dToCrAng { get; set; }
        public Single? dFromCrHt { get; set; }
        public Single? dToCrHt { get; set; }
        public Single? dFromPavAng { get; set; }
        public Single? dToPavAng { get; set; }
        public Single? dFromPavHt { get; set; }
        public Single? dToPavHt { get; set; }
        public string dKeyToSymbol { get; set; }
        public string dCheckKTS { get; set; }
        public string dUNCheckKTS { get; set; }
        public string sBGM { get; set; }
        public string sCrownBlack { get; set; }
        public string sTableBlack { get; set; }
        public string sCrownWhite { get; set; }
        public string sTableWhite { get; set; }
        public string Img { get; set; }
        public string Vdo { get; set; }
        public string PriceMethod { get; set; }
        public double? PricePer { get; set; }
    }
    public class ApiColumnsSettings
    {
        public int iTransId { get; set; }
        public int icolumnId { get; set; }
        public string sUser_ColumnName { get; set; }
        public string sCustMiseCaption { get; set; }
        public int iPriority { get; set; }
        public bool IsActive { get; set; }
        public string sColumnName { get; set; }
        public string sCaption { get; set; }
        public string sUserCaption { get; set; }
        public int iSeqNo { get; set; }
    }
    public class Get_ApiUploadMst_Request
    {
        public long TransId { get; set; }
        public string sSearch { get; set; }
        public int UserId { get; set; }
        public string dtFromDate { get; set; }
        public string dtToDate { get; set; }
        public int iPgNo { get; set; }
        public int iPgSize { get; set; }
        public string OrderBy { get; set; }
    }
    public class Get_ApiUploadMst_Response
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public long iTransId { get; set; }
        public string dCreationDate { get; set; }
        public string dTransDate { get; set; }
        public string ApiMethod { get; set; }
        public string ApiMethodName { get; set; }
        public string WebAPIUserName { get; set; }
        public string WebAPIPassword { get; set; }
        public string FTPHost { get; set; }
        public string FTPUser { get; set; }
        public string FTPPass { get; set; }
        public string FTPType { get; set; }
        public string FTPExportType { get; set; }
        public string URLUserName { get; set; }
        public string URLPassword { get; set; }
        public string URLExportType { get; set; }
        public string FileLocation { get; set; }
        public string LocationExportType { get; set; }
        public string LocationTransType { get; set; }
        public string OnetimeDate { get; set; }
        public string Onetime { get; set; }
        public string RepeateveryType { get; set; }
        public Decimal Repeatevery { get; set; }
        public string APIName { get; set; }
        public string APIUrl { get; set; }
        public bool APIStatus { get; set; }
        public int For_iUserId { get; set; }
    }
    public class KeyToSymbolResponse
    {
        public string sSymbol { get; set; }
    }
    public class Get_APIMst_Request
    {
        public long Id { get; set; }
        public string Search { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int iPgNo { get; set; }
        public int iPgSize { get; set; }
        public string OrderBy { get; set; }
    }
    public class Get_APIMst_Response
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public long Id { get; set; }
        public string APIURL { get; set; }
        public string APIName { get; set; }
        public string APIHitUrl { get; set; }
        public string APIResponseFormat { get; set; }
        public string FileLocation { get; set; }
        public string LocationExportType { get; set; }
        public string RepeateveryType { get; set; }
        public string Repeatevery { get; set; }
        public string APIMethod { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
    }
    public class Save_APIMst_Request
    {
        public long Id { get; set; }
        public string APIURL { get; set; }
        public string APIName { get; set; }
        public string APIHitUrl { get; set; }
        public string APIResponseFormat { get; set; }
        public string APIMethod { get; set; }
        public string FileLocation { get; set; }
        public string LocationExportType { get; set; }
        public string RepeateveryType { get; set; }
        public string Repeatevery { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class AnkitGems
    {
        public string code { get; set; }
        public bool flag { get; set; }
        public string message { get; set; }
        public string ref_id { get; set; }
        public AnkitGems_Inner_1 data { get; set; }
    }
    public class AnkitGems_Inner_1
    {
        public AnkitGems_Inner_2 user { get; set; }
        public string accessToken { get; set; }
    }
    public class AnkitGems_Inner_2
    {
        public string name { get; set; }
        public string day_terms { get; set; }
        public string account_name { get; set; }
        public string account_short_code { get; set; }
        public string business_type { get; set; }
        public string registration_date { get; set; }
        public string is_active { get; set; }
    }

    //By Dhruv Patel-01-12-2021
    public class Dharam
    {
        public int uniqID { get; set; }
        public string company { get; set; }
        public string actCode { get; set; }
        public string selectAll { get; set; }
        public int StartIndex { get; set; }
        public int count { get; set; }
        public string columns { get; set; }
        public string finder { get; set; }
        public string sort { get; set; }

    }

    //By Dhruv Patel-02-12-2021
    public class JOY
    {
        public List<string> keys { get; set; }
        public List<List<object>> rows { get; set; }

    }

    //By Dhruv Patel-15-12-2021
    public class SGLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class SGLoginResponse
    {
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public string TokenId { get; set; }
    }

    public class SGStockRequest
    {
        public string UserId { get; set; }
        public string TokenId { get; set; }
    }

    public class SGStockResponse
    {
        public List<data> Data { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
    }
    public class data
    {
        public string Stock_ID { get; set; }
        public string Shape { get; set; }
        public double Cts { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public double Rep_Price { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double Depth_Per { get; set; }
        public double Table_Per { get; set; }
        public double Cr_Ang { get; set; }
        public double Cr_Ht { get; set; }
        public double Pav_Ang { get; set; }
        public double Pav_Ht { get; set; }
        public string Certi_No { get; set; }
        public string Girdle { get; set; }
        public double Disc { get; set; }
        public string Lab { get; set; }
        public string Pointer { get; set; }
        public string Status { get; set; }
        public string Shade { get; set; }
        public string Luster { get; set; }
        public string Table_Natts { get; set; }
        public string Girdle_Type { get; set; }
        public string Culet { get; set; }
        public object Table_Depth { get; set; }
        public string Inclusion { get; set; }
        public string HNA { get; set; }
        public string Side_Natts { get; set; }
        public string Table_Open { get; set; }
        public string Crown_Open { get; set; }
        public string Comments { get; set; }
        public string Key_To_Symbol { get; set; }
        public double Disc_By_Date { get; set; }
        public string Inscription { get; set; }
        public double Girdle_Per { get; set; }
        public object Revise_Disc_Flag { get; set; }
        public string Crown_Natts { get; set; }
        public string Crown_Inclusion { get; set; }
        public string Certi_Date { get; set; }
        public string BGM { get; set; }
        public string UserComments { get; set; }
        public double Group_Disc { get; set; }
        public double Rap_Amount { get; set; }
        public double Net_Price { get; set; }
        public string Table_White { get; set; }
        public string Side_White { get; set; }
        public string Milky_Grade { get; set; }
        public string Source { get; set; }
        public string Location { get; set; }
        public string Fls_Color { get; set; }
        public DateTime? Lab_Date { get; set; }
        public double Price_Per_Cts { get; set; }
        public string View_Image { get; set; }
        public string View_Video { get; set; }
        public string View_Certi { get; set; }
        public string TableOpen { get; set; }
        public string CrownOpen { get; set; }
        public string PavillionOpen { get; set; }
        public string GirdleOpen { get; set; }
    }

    public class DiamartResponse
    {
        public string Loat_NO { get; set; }
        //public string Status { get; set; }
        //public string Shape { get; set; }
        //public double Weight { get; set; }
        //public string Color { get; set; }
        //public string Clarity { get; set; }
        //public string Cut { get; set; }
        //public string Polish { get; set; }
        //public string Symmetry { get; set; }
        //public string Fluorescence { get; set; }
        //public double Length { get; set; }
        //public double Width { get; set; }
        //public double Depth { get; set; }
        //public double TotalDepth { get; set; }
        //public double Table { get; set; }
        //public double Discount { get; set; }
        //public double Rap { get; set; }
        //public string Lab { get; set; }
        //public string CertiNo { get; set; }
        //public string Inscription { get; set; }
        //public double CrownAngle { get; set; }
        //public double CrownHeight { get; set; }
        //public double PavAngle { get; set; }
        //public double PavDepth { get; set; }
        //public string KeytoSymbols { get; set; }
        //public string Natts { get; set; }
        //public string Comment { get; set; }
        //public string HNA { get; set; }
        //public string EyeClean { get; set; }
        //public string Girdle { get; set; }
        //public double GirdlePerc { get; set; }
        //public string Culet { get; set; }
        //public string GirdleCondition { get; set; }
        //public string Location { get; set; }
        //public string IMG_URL { get; set; }
        //public string VID_URL { get; set; }
        //public string CERTI_URL { get; set; }
        //public string Shade { get; set; }
        //public string Milky { get; set; }
        //public string Brown { get; set; }
        //public string Green { get; set; }
        //public string CenterBlack { get; set; }
        //public string SideBlack { get; set; }
        //public string OpenTable { get; set; }
        //public string OpenCrown { get; set; }
        //public string OpenGirdle { get; set; }
        //public string OpenPavilion { get; set; }
        //public string NaturalOnCrown { get; set; }
        //public string NaturalOnGirdle { get; set; }
        //public string NaturalOnPavillion { get; set; }
        //public string EFOC { get; set; }
        //public string EFOP { get; set; }
        //public string Bowtie { get; set; }
        //public string StarLength { get; set; }
        //public string LowerHalf { get; set; }
        //public double NetDollar { get; set; }
        //public double Dcaret { get; set; }
    }

    public class KBS_LoginRequest
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class KBS_LoginResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string userName { get; set; }
    }
    public class BHAVYAM_LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string grant_type { get; set; }
        public string DeviceType { get; set; }
        public string IpAddress { get; set; }
    }
    public class VenusJewel_Login_Req
    {
        public string User_Name { get; set; }
        public string Password { get; set; }

    }
    public class VenusJewel_Login_Res
    {
        public string Token_Id { get; set; }
        public string Status { get; set; }
        public string Session_Time_out { get; set; }
        public string Status_Cd { get; set; }
    }
    public class JP_Login_Req
    {
        public string action { get; set; }
        public string vipid { get; set; }
        public string vippsd { get; set; }

    }
    public class JP_Login_Res
    {
        public string status { get; set; }
        public string message { get; set; }
        public obj_JP_Login_Res msgdata { get; set; }

    }
    public class obj_JP_Login_Res
    {
        public int id { get; set; }
        public string token { get; set; }
        public string vipaccount { get; set; }
        public string name { get; set; }
        public string creattime { get; set; }
        public string gender { get; set; }
        public string tellphone { get; set; }
        public string skype { get; set; }
        public string qq { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string ischeckname { get; set; }
        public string remark { get; set; }
        public string companyname { get; set; }
    }

    public class JP_Stock_Req
    {
        public string action { get; set; }
        public string token { get; set; }
        public int stockstatus { get; set; }
        public string address { get; set; }
        public string reportnos { get; set; }
        public int ispaged { get; set; }
        public int pageindex { get; set; }
    }
    public class JP_Stock_Res
    {
        public int status { get; set; }
        public string message { get; set; }
        public obj_JP_Stock_Res msgdata { get; set; }
    }
    public class obj_JP_Stock_Res
    {
        public long total { get; set; }
        public List<obj_JP_Stock_inner_Res> rows { get; set; }
    }
    public class obj_JP_Stock_inner_Res
    {
        public string stoneid { get; set; }
        public string productid { get; set; }
        public string shape { get; set; }
        public decimal carat { get; set; }
        public string color { get; set; }
        public string clarity { get; set; }
        public string cut { get; set; }
        public string polish { get; set; }
        public string symmetry { get; set; }
        public string fluorescence { get; set; }
        public string milky { get; set; }
        public string green { get; set; }
        public string black { get; set; }
        public int qcculet { get; set; }
        public string othertinge { get; set; }
        public string eyeclean { get; set; }
        public string measurement { get; set; }
        public string report { get; set; }
        public string reportno { get; set; }
        public string address { get; set; }
        public decimal rapprice { get; set; }
        public decimal saleback { get; set; }
        public decimal saledollorprice { get; set; }
        public string depth_scale { get; set; }
        public string table_scale { get; set; }
        public int stockstatus { get; set; }
        public int discountType { get; set; }
    }
    public class LATTICE_Login_Req
    {
        public string username { get; set; }
        public string password { get; set; }
        public string companyName { get; set; }
        public string appName { get; set; }
    }
    public class LATTICE_Login_Res
    {
        public string status { get; set; }
        public string message { get; set; }
        public obj_LATTICE_Login_Res data { get; set; }
        public string path { get; set; }
    }
    public class obj_LATTICE_Login_Res
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string userId { get; set; }
        public string departmentName { get; set; }
        public string departmentId { get; set; }
        public string username { get; set; }
        public string fullName { get; set; }
        public string companyId { get; set; }
        public string email { get; set; }
    }
    public class New_Shairu_Login_Req
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class New_Shairu_Login_Res
    {
        public string Username { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public string TokenId { get; set; }
    }
    public class New_Shairu_Stock_api_Req
    {
        public string UserId { get; set; }
        public string TokenId { get; set; }
        public string StoneId { get; set; }
    }

    //public interface ILogger
    //{
    //    void Info(string info);
    //    void Debug(string info);
    //    void Error(string message, Exception ex);
    //}
    //public class Logger : ILogger
    //{
    //    public Logger() { }
    //    public void Info(string info) { }
    //    public void Debug(string info) { }
    //    public void Error(string message, Exception ex) { }
    //}
    //public class MailSender
    //{
    //    public string EMailFrom { get; set; }
    //    public string EMailTo { get; set; }
    //    public string EMailSubject { get; set; }
    //    public string EMailBody { get; set; }
    //    public void SendEmail() { }
    //}
    //public class Invoice
    //{
    //    public long InvAmount { get; set; }
    //    public DateTime InvDate { get; set; }
    //    private ILogger fileLogger;
    //    private MailSender emailSender;
    //    public Invoice()
    //    {
    //        fileLogger = new Logger();
    //        emailSender = new MailSender();
    //    }
    //    public void AddInvoice()
    //    {
    //        try
    //        {
    //            fileLogger.Info("Add method Start");
    //            // Here we need to write the Code for adding invoice
    //            // Once the Invoice has been added, then send the  mail
    //            emailSender.EMailFrom = "emailfrom@xyz.com";
    //            emailSender.EMailTo = "emailto@xyz.com";
    //            emailSender.EMailSubject = "Single Responsibility Princile";
    //            emailSender.EMailBody = "A class should have only one reason to change";
    //            emailSender.SendEmail();
    //        }
    //        catch (Exception ex)
    //        {
    //            fileLogger.Error("Error Occurred while Generating Invoice", ex);
    //        }
    //    }
    //    public void DeleteInvoice()
    //    {
    //        try
    //        {
    //            //Here we need to write the Code for Deleting the already generated invoice
    //            fileLogger.Info("Delete Invoice Start at @" + DateTime.Now);
    //        }
    //        catch (Exception ex)
    //        {
    //            fileLogger.Error("Error Occurred while Deleting Invoice", ex);
    //        }
    //    }
    //}





    //    public class Invoice_1
    //    {
    //        public virtual double GetInvoiceDiscount(double amount)
    //        {
    //            return amount - 10;
    //        }
    //    }

    //    public class FinalInvoice : Invoice_1
    //    {
    //        public override double GetInvoiceDiscount(double amount)
    //        {
    //            return base.GetInvoiceDiscount(amount) - 50;
    //        }
    //    }
    //    public class ProposedInvoice : Invoice_1
    //    {
    //        public override double GetInvoiceDiscount(double amount)
    //        {
    //            return base.GetInvoiceDiscount(amount) - 40;
    //        }
    //    }
    //    public class RecurringInvoice : Invoice_1
    //    {
    //        public override double GetInvoiceDiscount(double amount)
    //        {
    //            return base.GetInvoiceDiscount(amount) - 30;
    //        }
    //    }
    //    class Program
    //    {
    //        static void Main(string[] args)
    //        {
    //            Console.WriteLine("Invoice Amount: 10000");
    //            Invoice_1 FInvoice = new FinalInvoice();
    //            double FInvoiceAmount = FInvoice.GetInvoiceDiscount(10000);
    //            Console.WriteLine($"Final Invoive : {FInvoiceAmount}");
    //            Invoice_1 PInvoice = new ProposedInvoice();
    //            double PInvoiceAmount = PInvoice.GetInvoiceDiscount(10000);
    //            Console.WriteLine($"Proposed Invoive : {PInvoiceAmount}");
    //            Invoice_1 RInvoice = new RecurringInvoice();
    //            double RInvoiceAmount = RInvoice.GetInvoiceDiscount(10000);
    //            Console.WriteLine($"Recurring Invoive : {RInvoiceAmount}");
    //            Console.ReadKey();
    //        }
    //    }




    //    public interface IFruit
    //    {
    //        string GetColor();
    //    }
    //    public class Apple : IFruit
    //    {
    //        public string GetColor()
    //        {
    //            return "Red";
    //        }
    //    }
    //    public class Orange : IFruit
    //    {
    //        public string GetColor()
    //        {
    //            return "Orange";
    //        }
    //    }
    //    class Program_1
    //    {
    //        static void Main(string[] args)
    //        {
    //            IFruit fruit = new Orange();
    //            Console.WriteLine($"Color of Orange: {fruit.GetColor()}");
    //            fruit = new Apple();
    //            Console.WriteLine($"Color of Apple: {fruit.GetColor()}");
    //            Console.ReadKey();
    //        }
    //    }





    //    public interface IPrinterTasks
    //    {
    //        void Print(string PrintContent);
    //        void Scan(string ScanContent);
    //    }
    //    interface IFaxTasks
    //    {
    //        void Fax(string content);
    //    }
    //    interface IPrintDuplexTasks
    //    {
    //        void PrintDuplex(string content);
    //    }

    //    public class HPLaserJetPrinter : IPrinterTasks, IFaxTasks, IPrintDuplexTasks
    //    {
    //        public void Print(string PrintContent)
    //        {
    //            Console.WriteLine("Print Done");
    //        }
    //        public void Scan(string ScanContent)
    //        {
    //            Console.WriteLine("Scan content");
    //        }
    //        public void Fax(string FaxContent)
    //        {
    //            Console.WriteLine("Fax content");
    //        }
    //        public void PrintDuplex(string PrintDuplexContent)
    //        {
    //            Console.WriteLine("Print Duplex content");
    //        }
    //    }
    //    class LiquidInkjetPrinter : IPrinterTasks
    //    {
    //        public void Print(string PrintContent)
    //        {
    //            Console.WriteLine("Print Done");
    //        }
    //        public void Scan(string ScanContent)
    //        {
    //            Console.WriteLine("Scan content");
    //        }
    //    }
    //    public class Program_3
    //    {
    //        static void Main(string[] args)
    //        {
    //            //Using HPLaserJetPrinter we can access all Printer Services
    //            HPLaserJetPrinter hPLaserJetPrinter = new HPLaserJetPrinter();
    //            hPLaserJetPrinter.Print("Printing");
    //            hPLaserJetPrinter.Scan("Scanning");
    //            hPLaserJetPrinter.Fax("Faxing");
    //            hPLaserJetPrinter.PrintDuplex("PrintDuplex");
    //            //Using LiquidInkjetPrinter we can only Access Print and Scan Printer Services
    //            LiquidInkjetPrinter liquidInkjetPrinter = new LiquidInkjetPrinter();
    //            liquidInkjetPrinter.Print("Printing");
    //            liquidInkjetPrinter.Scan("Scanning");
    //            Console.ReadKey();
    //        }
    //    }





    //    public class Employee
    //    {
    //        public int ID { get; set; }
    //        public string Name { get; set; }
    //        public string Department { get; set; }
    //        public int Salary { get; set; }
    //    }
    //    public interface IEmployeeDataAccessLogic
    //    {
    //        Employee GetEmployeeDetails(int id);
    //    }
    //    public class EmployeeDataAccessLogic : IEmployeeDataAccessLogic
    //    {
    //        public Employee GetEmployeeDetails(int id)
    //        {
    //            Employee emp = new Employee()
    //            {
    //                ID = id,
    //                Name = "Pranaya",
    //                Department = "IT",
    //                Salary = 10000
    //            };
    //            return emp;
    //        }
    //    }
    //    public class EmployeeBusinessLogic
    //    {
    //        IEmployeeDataAccessLogic _IEmployeeDataAccessLogic;
    //        public Employee GetEmployeeDetails(int id)
    //        {
    //            return _IEmployeeDataAccessLogic.GetEmployeeDetails(id);
    //        }
    //    }
    //    public class Program_4
    //    {
    //        static void Main(string[] args)
    //        {
    //            EmployeeBusinessLogic employeeBusinessLogic = new EmployeeBusinessLogic();
    //            Employee emp = employeeBusinessLogic.GetEmployeeDetails(1001);
    //            Console.WriteLine($"ID: {emp.ID}, Name: {emp.Name}, Department: {emp.Department}, Salary: {emp.Salary}");
    //            Console.ReadKey();
    //        }
    //    }




    //    public class Employee_1
    //    {
    //        public int ID { get; set; }
    //        public string Name { get; set; }
    //        public string Department { get; set; }
    //    }
    //    public interface IEmployeeDAL
    //    {
    //        List<Employee_1> SelectAllEmployees();
    //    }
    //    public class EmployeeDAL : IEmployeeDAL
    //    {
    //        public List<Employee_1> SelectAllEmployees()
    //        {
    //            List<Employee_1> ListEmployees = new List<Employee_1>();
    //            ListEmployees.Add(new Employee_1() { ID = 1, Name = "Pranaya", Department = "IT" });
    //            ListEmployees.Add(new Employee_1() { ID = 2, Name = "Kumar", Department = "HR" });
    //            ListEmployees.Add(new Employee_1() { ID = 3, Name = "Rout", Department = "Payroll" });
    //            return ListEmployees;
    //        }
    //    }
    //    public class EmployeeBL
    //    {
    //        public IEmployeeDAL employeeDAL;
    //        public EmployeeBL(IEmployeeDAL employeeDAL)
    //        {
    //            this.employeeDAL = employeeDAL;
    //        }
    //        public List<Employee_1> GetAllEmployees()
    //        {
    //            return employeeDAL.SelectAllEmployees();
    //        }
    //    }
    //    class Program_5
    //    {
    //        static void Main(string[] args)
    //        {
    //            EmployeeBL employeeBL = new EmployeeBL(new EmployeeDAL());
    //            List<Employee_1> ListEmployee = employeeBL.GetAllEmployees();
    //            foreach (Employee_1 emp in ListEmployee)
    //            {
    //                Console.WriteLine("ID = {0}, Name = {1}, Department = {2}", emp.ID, emp.Name, emp.Department);
    //            }
    //            Console.ReadKey();
    //        }
    //    }
    //}
}
