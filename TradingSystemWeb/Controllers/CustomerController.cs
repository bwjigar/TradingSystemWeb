using System.IO;
using Lib.Models;
using Lib.Repository;
using System.Web.Mvc;
using System;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Net;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Linq;
using System.Xml;
using System.Web.Hosting;
using OfficeOpenXml;
using System.Web.Http;
using System.Threading;

namespace TradingSystemWeb.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult CustomerAPIDetail()
        {
            return View();
        }
        public ActionResult CustomerAPIList()
        {
            return View();
        }
        public JsonResult Get_API_StockFilter()
        {
            ServiceResponse<Get_API_StockFilter_Response> list = CustomerRepo.Get_API_StockFilter();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_API_ColumnMas(int iTransId)
        {
            ServiceResponse<Get_API_ColumnMas_Response> list = CustomerRepo.Get_API_ColumnMas(iTransId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_ApiUploadMst(Get_ApiUploadMst_Request get_apiuploadmst)
        {
            ServiceResponse<Get_ApiUploadMst_Response> list = CustomerRepo.Get_ApiUploadMst(get_apiuploadmst);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_ApiFilter(Get_ApiUploadMst_Request get_apiuploadmst)
        {
            ServiceResponse<APIFiltersSettings> list = CustomerRepo.Get_ApiFilter(get_apiuploadmst);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveUploadMethod(ApiUploadMethod apiuploadmethod)
        {
            if (apiuploadmethod != null)
            {
                CommonResponse _data = new CommonResponse();
                Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);

                string AbsoluteUri = url.AbsoluteUri;
                string AbsolutePath = url.AbsolutePath;
                string mainurl = AbsoluteUri.Replace(AbsolutePath, "");

                if (apiuploadmethod.ApiMethod == "URL")
                {
                    string DecodedUsername = EncodeServerName(apiuploadmethod.URLUserName);
                    string DecodedPassword = EncodeServerName(apiuploadmethod.URLPassword);

                    apiuploadmethod.APIUrl = mainurl + "/Customer/URL?UN=" + DecodedUsername + "&PD=" + DecodedPassword + "&TransId=";
                }
                else if (apiuploadmethod.ApiMethod == "WEBAPI")
                {
                    //apiuploadmethod.APIUrl = ConfigurationManager.AppSettings["APIURL"] + "/ApiSettings/BasicAuthLog?TransId=";
                    apiuploadmethod.APIUrl = mainurl + "/Customer/BasicAuthLog?TransId=";
                }
                else if (apiuploadmethod.ApiMethod == "FTP")
                {
                    apiuploadmethod.APIUrl = mainurl + "/Customer/DownloadAPIData?TransId=";
                }
                else if (apiuploadmethod.ApiMethod == "Location")
                {
                    apiuploadmethod.APIUrl = mainurl + "/Customer/Location?TransId=";
                }

                ServiceResponse<CommonResponse> list = CustomerRepo.SaveUploadMethod(apiuploadmethod);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }
        public JsonResult Get_KeyToSymbol()
        {
            ServiceResponse<KeyToSymbolResponse> list = CustomerRepo.Get_KeyToSymbol();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public static string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }
        public static string DecodeServerName(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }
        public ActionResult APIDetail()
        {
            return View();
        }
        public ActionResult APIList()
        {
            return View();
        }
        public JsonResult Get_APIMst(Get_APIMst_Request get_apimst)
        {
            ServiceResponse<Get_APIMst_Response> list = CustomerRepo.Get_APIMst(get_apimst);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveAPIMst(Save_APIMst_Request saveapimst)
        {
            if (saveapimst != null)
            {
                CommonResponse _data = new CommonResponse();
                Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                string mainurl = url.AbsoluteUri.Replace(url.AbsolutePath, "");

                if (saveapimst.Id == 0)
                {
                    saveapimst.APIHitUrl = mainurl + "/Customer/APIGet?Id=";
                }

                ServiceResponse<CommonResponse> list = CustomerRepo.SaveAPIMst(saveapimst);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }

        //public JsonResult CallAPI(string API, string UserName, string Password)
        //{
        //    String _API = API; //"http://61.93.195.114/KrishdiamondService/KrishDiamondService.asmx/GetStoneData";
        //    var input = new LoginRequest
        //    {
        //        UserName = UserName, //"sunrise",
        //        Password = Password  //"04321"
        //    };

        //    string InputPara = string.Join("&", input.GetType()
        //                                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //                                                .Where(p => p.GetValue(input, null) != null)
        //                       .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(input).ToString())}"));

        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/x-www-form-urlencoded";
        //    client.Encoding = Encoding.UTF8;
        //    ServicePointManager.Expect100Continue = false;
        //    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        //    string xml;

        //    try
        //    {
        //        xml = client.UploadString(_API, InputPara);
        //        DataTable dt = new DataTable();
        //        ConvertXmlStringToDataTable xDt = new ConvertXmlStringToDataTable();
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(xml);
        //        XmlElement root = doc.DocumentElement;
        //        XmlNodeList elemList = root.GetElementsByTagName("Row");
        //        dt = xDt.ConvertXmlNodeListToDataTable(elemList);

        //        string _path = ConfigurationManager.AppSettings["data"];
        //        string tempPath = HostingEnvironment.MapPath("~/Temp/API/");
        //        if (!Directory.Exists(tempPath))
        //        {
        //            Directory.CreateDirectory(tempPath);
        //        }
        //        string filename = DateTime.Now.ToString("dd-MM-yyyy HHmmss") + ".csv";
        //        FileInfo newFile = new FileInfo(tempPath + filename);
        //        if (newFile.Exists)
        //        {
        //            newFile.Delete();
        //            newFile = new FileInfo(tempPath + filename);
        //        }

        //        FileInfo _newFile = new FileInfo(tempPath + filename);
        //        using (ExcelPackage pck = new ExcelPackage(_newFile))
        //        {
        //            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(UserName);
        //            pck.Workbook.Properties.Title = UserName;
        //            ws.Cells["A1"].LoadFromDataTable(dt, true);

        //            ws.View.FreezePanes(2, 1);
        //            var allCells = ws.Cells[ws.Dimension.Address];
        //            allCells.AutoFilter = true;
        //            allCells.AutoFitColumns();

        //            removingGreenTagWarning(ws, ws.Cells[1, 1, 100, 100].Address);

        //            var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
        //            headerCells.Style.Font.Bold = true;
        //            headerCells.Style.Font.Color.SetColor(System.Drawing.Color.Black);
        //            headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        //            headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
        //            pck.Save();
        //        }

        //        filename = _path + filename;
        //        return Json(filename, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (WebException ex)
        //    {
        //        return Json(ex.Message, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //private void removingGreenTagWarning(ExcelWorksheet template1, string address)
        //{
        //    var xdoc = template1.WorksheetXml;
        //    //Create the import nodes (note the plural vs singular
        //    var ignoredErrors = xdoc.CreateNode(System.Xml.XmlNodeType.Element, "ignoredErrors", xdoc.DocumentElement.NamespaceURI);
        //    var ignoredError = xdoc.CreateNode(System.Xml.XmlNodeType.Element, "ignoredError", xdoc.DocumentElement.NamespaceURI);
        //    ignoredErrors.AppendChild(ignoredError);

        //    //Attributes for the INNER node
        //    var sqrefAtt = xdoc.CreateAttribute("sqref");
        //    sqrefAtt.Value = address;// Or whatever range is needed....

        //    var flagAtt = xdoc.CreateAttribute("numberStoredAsText");
        //    flagAtt.Value = "1";

        //    ignoredError.Attributes.Append(sqrefAtt);
        //    ignoredError.Attributes.Append(flagAtt);

        //    //Now put the OUTER node into the worksheet xml
        //    xdoc.LastChild.AppendChild(ignoredErrors);
        //}

        public JsonResult APIGet()
        {
            Thread APIGet = new Thread(APIGet_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void APIGet_Thread()
        {
            ServiceResponse<CommonResponse> list = CustomerRepo.APIGet();
        }

        public JsonResult SpaceCheck()
        {
            Thread SpaceCheck = new Thread(SpaceCheck_Thread);
            SpaceCheck.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void SpaceCheck_Thread()
        {
            ServiceResponse<CommonResponse> list = CustomerRepo.SpaceCheck();
        }
    }
}