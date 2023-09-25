using MainWebsite.Data_Repositary;
using MainWebsite.DataInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MainWebsite.CommonClass;
using System.Data;
using System.Web.Routing;
using MainWebsite.DataAccess;
using MainWebsite.DTO;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using System.Globalization;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Configuration;
using WebGrease.Css.Extensions;

namespace TradingSystemWeb.Controllers
{
    public class HomeController : Controller
    {
        private IPartyWiseColumnSetting _PartyWiseRepository;
        DataTable tblMAPPING = new DataTable();
        DataTable Dt = new DataTable();
        string[] excelColumnList = null;
        public HomeController()
        {
            this._PartyWiseRepository = new PartyWiseColumnSetting(new TradingDBEntities());
        }
        protected override void Dispose(bool disposing)
        {
            _PartyWiseRepository.Dispose();

            base.Dispose(disposing);
        }

        #region :: Column Mapping ::
        /// <summary>
        /// Collumn Mapping from Api Or Excel Data
        /// </summary>
        /// <returns>Client Data Header list and Selection option</returns>
        [HttpGet]
        public ActionResult Index(int MasSeq = 0, string DataType = "")
        {
            List<SUPPLIER_MAS> SuplierList = new List<SUPPLIER_MAS>();
            SuplierList = _PartyWiseRepository.GetAllSupplier().Select(x => new SUPPLIER_MAS
            {
                SEQ_NO = x.SEQ_NO,
                FIRST_NAME = x.FIRST_NAME + " " + x.LAST_NAME

            }).ToList();
            if (SuplierList != null)
            {
                ViewBag.Suplier = new SelectList(SuplierList, "SEQ_NO", "FIRST_NAME");
            }
            var Typedatalist = new List<SelectListItem>
             {
             new SelectListItem{ Text="EXCEL", Value = "EXCEL" },
             new SelectListItem{ Text="JSON", Value = "JSON" },
             new SelectListItem{ Text="CSV", Value = "CSV"}
             };
            ViewBag.TypeData = Typedatalist;
            if (MasSeq != 0)
            {
                ExcelDemandMasDTO List = new ExcelDemandMasDTO();
                EXCEL_DEMAND_MAS obj = _PartyWiseRepository.getAllExcelMas().Where(x => x.SEQ_NO == MasSeq).FirstOrDefault();
                if (obj != null)
                {
                    List = new ExcelDemandMasDTO()
                    {
                        SEQ_NO = obj.SEQ_NO,
                        DATA_FROM = obj.DATA_FROM,
                        NARRATION = obj.NARRATION,
                        TYPE_OF_DATA = obj.TYPE_OF_DATA,
                        FILE_NAME = obj.FILE_NAME,
                        LINK = obj.LINK,
                        SUPP_SEQ = obj.SUPP_SEQ,
                        FTP_FILE_PATH=obj.FTP_FILE_PATH

                    };
                    if (DataType == "JSON")
                    {
                        if (obj.JSON_DATA_TYPE != "DIRECT DATA FROM LINK")
                        {
                            if (obj.PARAMETER == null)
                            {
                                GetTableWithPost(obj.LINK, "");
                            }
                            else
                            {                                                               
                                string JsonString = StringBuilder(obj.PARAMETER, obj.PARAMETER_VALUE);

                                GetTableWithPost(obj.LINK, JsonString.ToString());
                            }
                            ViewBag.JsonType = obj.JSON_DATA_TYPE;
                           
                        }
                        else
                        {
                            GetTableFrmoApiCommon(obj.LINK);
                        }
                    }
                    else
                    {
                        if (obj.JSON_DATA_TYPE == "DIRECT FROM FTP")
                        {
                            var FtpPath = obj.FTP_FILE_PATH + "\\" + obj.FILE_NAME;
                            string data = GetDatafromFTPFile(FtpPath.ToString());
                            ViewBag.FileType = obj.FTP_FILE_PATH;
                        }
                        else
                        {
                            GetExcelDatatable(obj.LINK);
                        }
                    }
                    TempData["CaptionList"] = _PartyWiseRepository.GetAllColumnDet().Where(x => x.MAS_SEQ == obj.SEQ_NO).ToList();
                    return View(List);
                }
            }
            else { TempData["excelColumnList"] = null; TempData["CaptionList"] = null; }
            return View();
        }
        [HttpPost]
        public JsonResult GetCaptionList()
        {
            string JsonTable = "";
            try
            {
                var table = TempData["CaptionList"];
                TempData["CaptionList"] = table;
                if (table == null)
                {
                    TempData["CaptionList"] = (from col in _PartyWiseRepository.GetAllColumnMas().Select(x => new ColumnMasDto
                    {
                        SEQ_NO = x.SEQ_NO,
                        DISPLAY_NAME = x.DISPLAY_NAME,
                        EXCEL_COLUMN = string.Empty
                    })
                                               select col).ToList();
                }
                var Tabledata = TempData["CaptionList"];
                var jsonSerialiser = new JavaScriptSerializer();
                JsonTable = jsonSerialiser.Serialize(Tabledata);
                return Json(JsonTable);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Client Table Get From Api
        /// </summary>
        /// <param name="ExcelMas"></param>
        /// <returns>Convert Api to Table And Pass Into Mapping Table</returns>
        [HttpPost]
        public JsonResult GetDataJsonApi(EXCEL_DEMAND_MAS ExcelMas, string JsonObj, PostParametarsClass Parametar)
        {
            ConvertJsonStringToDataTable ConvertTable = new ConvertJsonStringToDataTable();
            DataTable Dt = new DataTable();
            string Result = "";
            if (ExcelMas.DATA_FROM == "API FILE" && ExcelMas.TYPE_OF_DATA == "JSON")
            {
                try
                {
                    if (ExcelMas.JSON_DATA_TYPE != "USING POST METHOD")
                    {
                        GetTableFrmoApiCommon(ExcelMas.LINK.ToString());
                    }
                    else
                    {
                        GetTableWithPost(ExcelMas.LINK.ToString(), JsonObj);
                    }
                    Result = "SUCCESS";
                    return Json(Result);
                }
                catch (Exception)
                {
                    Result = "error";
                }
            }
            else
            {
                try
                {
                    GetExcelDatatable(ExcelMas.LINK.ToString());
                    Result = "SUCCESS";
                    return Json(Result);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            return Json(Result);

        }

        [HttpPost]
        public JsonResult GetDatafromFTP(string FtpPath)
        {
            string Result = string.Empty;
            try
            {
                Result = GetDatafromFTPFile(FtpPath);
            }
            catch (Exception)
            {
                Result = "FAIL";
                throw;
            }

            return Json(Result);
        }
        [HttpPost]
        public JsonResult GetExcelColumnList()
        {
            string ClientHeaderList = "";
            try
            {
                var table = TempData["excelColumnList"];
                TempData.Keep("excelColumnList");
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(table);
                ClientHeaderList = json;
                return Json(ClientHeaderList);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Save Column On Datatbase
        /// </summary>
        /// <param name="Mappindata"></param>
        /// <param name="exModaldata"></param>
        /// <param name="MAS_SEQ"></param>
        /// <returns> Save After Mapping All Table into DataBase EXCEL_DEMAND_MAS AND EXCEL_DEMAND_DET</returns>
        [HttpPost]
        public JsonResult SaveColamnMapping(IEnumerable<EXCEL_DEMAND_DET> Mappindata, EXCEL_DEMAND_MAS exModaldata, int MAS_SEQ = 0)
        {
            string Result = string.Empty;

            try
            {
                if (MAS_SEQ == 0)
                {
                    _PartyWiseRepository.IndertExcelColumnMas(exModaldata);
                    int MasSeq = exModaldata.SEQ_NO;
                    foreach (var item in Mappindata)
                    {
                        item.MAS_SEQ = MasSeq;
                    }
                    _PartyWiseRepository.IndertExcelColumnDet(Mappindata);
                }
                else
                {
                    // var id = TempData["PartyWiseList"].ToString();
                    _PartyWiseRepository.UpdateExcelColumnDet(Mappindata);
                    _PartyWiseRepository.UpdateExcelColumnMas(exModaldata, MAS_SEQ);

                }
                Result = "Success";
            }
            catch (Exception ex)
            {
                Result = ex.Message;
                throw;
            }
            return Json(Result);
        }
        #endregion

        #region :: ViewColumnMapping ::
        /// <summary>
        /// View Column Mapping Table
        /// </summary>
        /// <returns>List All Column Mapping Map By Client</returns>
        public ActionResult ViewApiData()
        {
            try
            {
                List<ExcelDemandMasDTO> List = new List<ExcelDemandMasDTO>();
                var AllExcelMas = _PartyWiseRepository.getAllExcelMas().ToList();

                foreach (var obj in AllExcelMas)
                {
                    var AllSuppMas = _PartyWiseRepository.GetAllSupplier().Where(x => x.SEQ_NO == obj.SUPP_SEQ).ToList();
                    foreach (var objSupp in AllSuppMas)
                    {
                        List.Add(new ExcelDemandMasDTO
                        {
                            SEQ_NO = obj.SEQ_NO,
                            DATA_FROM = obj.DATA_FROM,
                            SUPP_NAME = objSupp.FIRST_NAME + " " + objSupp.LAST_NAME,
                            NARRATION = obj.NARRATION,
                            TYPE_OF_DATA = obj.TYPE_OF_DATA,
                            FILE_NAME = obj.FILE_NAME,
                            LINK = obj.LINK,
                            SUPP_SEQ = obj.SUPP_SEQ,
                            FTP_FILENAME = obj.FILE_NAME,
                            FTP_FILE_PATH = obj.FTP_FILE_PATH
                        });
                    }
                }
                ViewBag.Message = "View All Api Data.";
                return View(List);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region :: Save Api Data ::
        public ActionResult SaveApiData()
        {
            return View();

        }
        [HttpPost]
        public JsonResult GetSaveApiList()
        {
            try
            {
                TempData["SaveApiList"] = (from col in _PartyWiseRepository.getAllExcelMas()
                                           join sp in _PartyWiseRepository.GetAllSupplier() on col.SUPP_SEQ equals sp.SEQ_NO
                                           select new ExcelDemandMasDTO
                                           {
                                               SEQ_NO = col.SEQ_NO,
                                               SUPP_SEQ = col.SUPP_SEQ,
                                               FILE_NAME = sp.FIRST_NAME + " " + sp.LAST_NAME,
                                               LINK = col.LINK,
                                               SUPP_NAME = sp.FIRST_NAME + " " + sp.LAST_NAME,
                                               ERROR = string.Empty,
                                               TYPE_OF_DATA = col.TYPE_OF_DATA,
                                               JSON_DATA_TYPE = col.JSON_DATA_TYPE,
                                               FTP_FILENAME = col.FILE_NAME,
                                               FTP_FILE_PATH = col.FTP_FILE_PATH
                                           }).ToList();
                var SaveApiList = TempData["SaveApiList"];
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(SaveApiList);
                SaveApiList = json;
                return Json(SaveApiList);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult VerifyApiData(ExcelDemandMasDTO SaveModal, string PartyName, string MasSeq)
        {
            ConvertJsonStringToDataTable ConvertTable = new ConvertJsonStringToDataTable();
            var ErrorData = "";
            List<CLIENT_STOCK_DTO> ClientStk = new List<CLIENT_STOCK_DTO>();
            List<MAIN_STOCK_DTO> Stock = new List<MAIN_STOCK_DTO>();
            DataSet DataStockMas = new DataSet();
            DataTable MainStock = CreateDataTable(Stock);
            DataTable ClientStock = CreateDataTable(ClientStk);
            string Result = string.Empty;
            try
            {
                DataSet DataSetMas = new DataSet();
                DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetShape().ToList()));
                DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetColor().ToList()));
                DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetPurity().ToList()));
                DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetCut().ToList()));
                DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetPolish().ToList()));
                DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetSymm().ToList()));
                DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetFls().ToList()));
                DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetLab().ToList()));
                DataTable FDt = ConvertTable.ToDataTable(_PartyWiseRepository.GetAllFencyColorMas().ToList());
                var ApiLinkdata = SaveModal.LINK.Split(',');
                var DataTypeData = SaveModal.TYPE_OF_DATA.Split(',');
                var MasSeqData = MasSeq.Split(',');
                var PartyNameData = PartyName.Split(',');
                var JsonTypeData = SaveModal.JSON_DATA_TYPE.Split(',');
                var FtpPath = SaveModal.FTP_FILE_PATH.Split(',');
                var FileName = SaveModal.FILE_NAME.Split(',');
                int ErrorCount = 0;
                int ScucessCount = 0;
                for (int y = 0; y < ApiLinkdata.Length; y++)
                {
                    DataTable tblMAPPING = ConvertTable.ToDataTable(_PartyWiseRepository.GetAllColumnDet().Where(x => x.MAS_SEQ == int.Parse(MasSeqData[y])).ToList());


                    Dt = GetClientTable(ApiLinkdata[y], DataTypeData[y], JsonTypeData[y], MasSeqData[y], FtpPath[y], FileName[y]);



                    int RowNo = 1;
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        DataRow ClientStkRow = ClientStock.NewRow();
                        DataRow MainStkRow = MainStock.NewRow();
                        ClientStkRow["SUPP_SEQ"] = Convert.ToInt16(PartyNameData[y]);
                        MainStkRow["SUPP_SEQ"] = Convert.ToInt16(PartyNameData[y]);
                        string ErrorRemark = string.Empty;
                        for (int j = 0; j < tblMAPPING.Rows.Count; j++)
                        {
                            if (Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"]) != "")
                            {
                                var ecelcol = tblMAPPING.Rows[j]["EXCEL_COLUMN"];

                                if (ClientStock.Columns.IndexOf(Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])) > 0 || Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"]).Contains("_SEQ") == true)
                                {
                                    if (Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"]).Contains("_SEQ"))
                                    {
                                        var dbcol = Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"]);
                                        string[] RemoveSeq = Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"]).Split('_');
                                        string DataSeq = "";
                                        if (dbcol == "COLOR_SEQ")
                                        {
                                            var ClinetColor = Convert.ToString(Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])]);

                                            if (ClinetColor.Length > 3)
                                            {
                                                if (ClinetColor.Contains(","))
                                                {
                                                    ClinetColor = ClinetColor.Replace(',', ' ');
                                                }
                                                var ColorData = ClinetColor.ToString().ToUpper().Split(' ');
                                                var MatchColor = "";
                                                foreach (DataRow Fdtcolumn in FDt.Rows)
                                                {
                                                    var MatchColumnName = "";
                                                    string[] SynonymsList = Fdtcolumn["SYNONYM_LIST"].ToString().ToUpper().Split(new string[] { "|", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                                                    for (int p = 0; p < ColorData.Length; p++)
                                                    {
                                                        if (SynonymsList.Contains(ColorData[p]))
                                                        {
                                                            MatchColor = ColorData[p];
                                                            MatchColumnName = Fdtcolumn["NAME"].ToString();
                                                            MainStkRow[MatchColumnName.ToString()] = MatchColor;
                                                            ClientStkRow[MatchColumnName.ToString()] = MatchColor;
                                                            break;
                                                        }
                                                    }
                                                }
                                                if (MatchColor != "")
                                                {
                                                    Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])] = "*";
                                                }
                                            }
                                        }
                                        DataTable TmpMas = DataSetMas.Tables[RemoveSeq[0] + "_MAS"];
                                        foreach (DataRow DrColumns in TmpMas.Rows)
                                        {
                                            string[] SynonymsList = DrColumns["SYNONYM_LIST"].ToString().ToUpper().Split(new string[] { "|", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                                            if (SynonymsList.Contains(Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])]))
                                            {
                                                DataSeq = DrColumns["SEQ_NO"].ToString();
                                                break;
                                            }
                                        }
                                        if (DataSeq == "")
                                        {
                                            ErrorRemark = ErrorRemark.Length == 0 ? RemoveSeq[0] + " : " + Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])] : ErrorRemark + "," + RemoveSeq[0] + " : " + Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])];
                                            MainStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = DBNull.Value;
                                            ClientStkRow[RemoveSeq[0]] = DBNull.Value;
                                        }
                                        else
                                        {
                                            MainStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = DataSeq;
                                            ClientStkRow[RemoveSeq[0]] = Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])];

                                        }
                                    }
                                    else
                                    {
                                        var Value = Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])];
                                        if (Value.ToString() != "")
                                        {
                                            if (Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"]) == "RESULT_DATE")
                                            {
                                                Value = DateParse(Value.ToString());
                                            }
                                            ClientStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = Value.ToString().Trim();
                                            MainStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = Value.ToString().Trim();
                                        }
                                        else
                                        {
                                            ClientStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = DBNull.Value;
                                            MainStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = DBNull.Value;
                                        }
                                    }
                                }
                            }
                        }
                        ClientStkRow["ERROR_REMARK"] = ErrorRemark;
                        ClientStkRow["TRANS_DATE"] = System.DateTime.Now;
                        ClientStkRow["FILE_NAME"] = FileName[y];
                        ClientStkRow["LINE_NUMBER"] = Convert.ToString(RowNo++);
                        MainStkRow["FILE_NAME"] = FileName[y];
                        MainStkRow["LINE_NUMBER"] = ClientStkRow["LINE_NUMBER"];

                        if (ErrorRemark != "")
                        {
                            ClientStkRow["ERROR_FLAG"] = 1;
                            MainStkRow["ERROR_FLAG"] = 1;
                            ErrorCount++;
                        }
                        else
                        {
                            ClientStkRow["ERROR_FLAG"] = 0;
                            MainStkRow["ERROR_FLAG"] = 0;
                            ScucessCount++;
                        }
                        ClientStock.Rows.Add(ClientStkRow);
                        MainStock.Rows.Add(MainStkRow);
                    }

                    var Error = ScucessCount + "$" + ErrorCount;//_PartyWiseRepository.GetAllClientStock().Where(x => x.SUPP_SEQ == int.Parse(PartyNameData[y]) && x.ERROR_FLAG == true).Count();
                    ErrorCount = 0; ScucessCount = 0;
                    ErrorData += (ErrorData.Length > 0 ? "," : "") + Error.ToString();
                    Result = "SUCCESS|" + ErrorData;
                }


            }
            catch (Exception ex)
            {
                Result = "FAIL";
                throw;
            }

            TempData["AllDataTempTable"] = ClientStock;
            TempData["MainStock"] = MainStock;
            // TempData["ErrorDatatemptable"] = DataTableToJson(TempTable);
            return Json(Result);
        }

        //[HttpPost]
        //public JsonResult SaveApiData(ExcelDemandMasDTO SaveModal,string PartyName, string MasSeq)
        //{
        //    ConvertJsonStringToDataTable ConvertTable = new ConvertJsonStringToDataTable();
        //    var ErrorData="";
        //    string Result = string.Empty;
        //    try
        //    {
        //        DataSet DataSetMas = new DataSet();
        //        DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetShape().ToList()));
        //        DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetColor().ToList()));
        //        DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetPurity().ToList()));
        //        DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetCut().ToList()));
        //        DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetPolish().ToList()));
        //        DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetSymm().ToList()));
        //        DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetFls().ToList()));
        //        DataSetMas.Tables.Add(ConvertTable.ToDataTable(_PartyWiseRepository.GetLab().ToList()));
        //        DataTable FDt = ConvertTable.ToDataTable(_PartyWiseRepository.GetAllFencyColorMas().ToList());
        //        var ApiLinkdata = SaveModal.LINK.Split(',');
        //        var DataTypeData = SaveModal.TYPE_OF_DATA.Split(',');
        //        var MasSeqData = MasSeq.Split(',');
        //        var PartyNameData = PartyName.Split(',');
        //        var JsonTypeData = SaveModal.JSON_DATA_TYPE.Split(',');
        //        for (int y = 0; y < ApiLinkdata.Length; y++)
        //        {
        //            DataTable tblMAPPING = ConvertTable.ToDataTable(_PartyWiseRepository.GetAllColumnDet().Where(x => x.MAS_SEQ == int.Parse(MasSeqData[y])).ToList());


        //            Dt = GetClientTable(ApiLinkdata[y], DataTypeData[y], JsonTypeData[y], MasSeqData[y]);

        //            List<CLIENT_STOCK> ClientStk = new List<CLIENT_STOCK>();

        //            List<MAIN_STOCK> Stock = new List<MAIN_STOCK>();
        //            DataTable ClientStock = CreateDataTable(ClientStk);
        //            DataTable MainStock = CreateDataTable(Stock);
        //            for (int i = 0; i < Dt.Rows.Count; i++)
        //            {
        //                DataRow ClientStkRow = ClientStock.NewRow();
        //                DataRow MainStkRow = MainStock.NewRow();
        //                ClientStkRow["SUPP_SEQ"] = Convert.ToInt16(PartyNameData[y]);
        //                MainStkRow["SUPP_SEQ"] = Convert.ToInt16(PartyNameData[y]);
        //                string ErrorRemark = string.Empty;
        //                for (int j = 0; j < tblMAPPING.Rows.Count; j++)
        //                {
        //                    if (Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"]) != "")
        //                    {
        //                        var ecelcol = tblMAPPING.Rows[j]["EXCEL_COLUMN"];

        //                        if (ClientStock.Columns.IndexOf(Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])) > 0 || Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"]).Contains("_SEQ") == true)
        //                        {
        //                            if (Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"]).Contains("_SEQ"))
        //                            {
        //                                var dbcol = Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"]);
        //                                string[] RemoveSeq = Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"]).Split('_');
        //                                string DataSeq = "";                                                                                
        //                                if (dbcol == "COLOR_SEQ")
        //                                {
        //                                    var ClinetColor = Convert.ToString(Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])]);

        //                                    if (ClinetColor.Length > 3)
        //                                    {
        //                                        if (ClinetColor.Contains(","))
        //                                        {
        //                                            ClinetColor = ClinetColor.Replace(',', ' ');
        //                                        }
        //                                        var ColorData = ClinetColor.ToString().ToUpper().Split(' ');
        //                                        var MatchColor = "";
        //                                        foreach (DataRow Fdtcolumn in FDt.Rows)
        //                                        {
        //                                            var MatchColumnName = "";
        //                                            string[] SynonymsList = Fdtcolumn["SYNONYM_LIST"].ToString().ToUpper().Split(new string[] { "|", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //                                            for (int p = 0; p < ColorData.Length; p++)
        //                                            {
        //                                                if (SynonymsList.Contains(ColorData[p]))
        //                                                {
        //                                                    MatchColor = ColorData[p];
        //                                                    MatchColumnName = Fdtcolumn["NAME"].ToString();
        //                                                    MainStkRow[MatchColumnName.ToString()] = MatchColor;
        //                                                    ClientStkRow[MatchColumnName.ToString()] = MatchColor;                                                           
        //                                                    break;
        //                                                }
        //                                            }
        //                                        }
        //                                        if(MatchColor!="")
        //                                        {
        //                                            Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])] = "*";
        //                                        }                                                                                             
        //                                    }
        //                                }
        //                                DataTable TmpMas = DataSetMas.Tables[RemoveSeq[0] + "_MAS"];
        //                                foreach (DataRow DrColumns in TmpMas.Rows)
        //                                {
        //                                    string[] SynonymsList = DrColumns["SYNONYM_LIST"].ToString().ToUpper().Split(new string[] { "|", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //                                    if (SynonymsList.Contains(Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])]))
        //                                    {
        //                                        DataSeq = DrColumns["SEQ_NO"].ToString();
        //                                        break;
        //                                    }                                                                                      
        //                                }                                                                        
        //                                if (DataSeq == "")
        //                                {
        //                                    ErrorRemark = ErrorRemark.Length == 0 ? RemoveSeq[0] + " : " + Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])] : ErrorRemark + "," + RemoveSeq[0] + " : " + Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])];
        //                                    MainStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = DBNull.Value;
        //                                    ClientStkRow[RemoveSeq[0]] = DBNull.Value;
        //                                }
        //                                else
        //                                {
        //                                    MainStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = DataSeq;
        //                                    ClientStkRow[RemoveSeq[0]] = Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])];
        //                                }
        //                                //var Value = Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])];
        //                                //if (Value.ToString() != "")
        //                                //{
        //                                //    ClientStkRow[RemoveSeq[0]] = Value;
        //                                //}
        //                                //else
        //                                //{
        //                                //    ClientStkRow[RemoveSeq[0]] = DBNull.Value;
        //                                //}
        //                            }
        //                            else
        //                            {
        //                                var Value = Dt.Rows[i][Convert.ToString(tblMAPPING.Rows[j]["EXCEL_COLUMN"])];                                              
        //                                if (Value.ToString() != "")
        //                                {
        //                                    if (Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"]) == "RESULT_DATE")
        //                                    {
        //                                        Value = DateParse(Value.ToString());
        //                                    }
        //                                    ClientStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = Value.ToString().Trim();
        //                                    MainStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = Value.ToString().Trim();
        //                                }
        //                                else
        //                                {
        //                                    ClientStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = DBNull.Value;
        //                                    MainStkRow[Convert.ToString(tblMAPPING.Rows[j]["DB_COLUMN"])] = DBNull.Value;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                ClientStkRow["ERROR_REMARK"] = ErrorRemark;
        //                ClientStkRow["TRANS_DATE"] = System.DateTime.Now;
        //                if (ErrorRemark != "")
        //                {
        //                    ClientStkRow["ERROR_FLAG"] = 1;
        //                }
        //                else
        //                {
        //                    ClientStkRow["ERROR_FLAG"] = 0;
        //                }
        //                ClientStock.Rows.Add(ClientStkRow);
        //                MainStock.Rows.Add(MainStkRow);
        //            }

        //            SqlBulkCopy bulkCopy = new SqlBulkCopy(@"Data Source=bwserver; Initial Catalog=TradingDB; User ID=sa; Password=P@ssw0rd;");
        //            bulkCopy.DestinationTableName = "CLIENT_STOCK";
        //            bulkCopy.WriteToServer(ClientStock);

        //            SqlBulkCopy bulkCopyMianStk = new SqlBulkCopy(@"Data Source=bwserver; Initial Catalog=TradingDB; User ID=sa; Password=P@ssw0rd;");
        //            bulkCopyMianStk.DestinationTableName = "STOCK";
        //            bulkCopyMianStk.WriteToServer(MainStock);

        //            var Error = _PartyWiseRepository.GetAllClientStock().Where(x => x.SUPP_SEQ == int.Parse(PartyNameData[y]) && x.ERROR_FLAG == true).Count();

        //            ErrorData += (ErrorData.Length > 0 ? "," : "") + Error.ToString();
        //            Result = "SUCCESS|" + ErrorData;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Result = "FAIL";
        //        throw;
        //    }


        //    return Json(Result);
        //}
        public static DateTime DateParse(string date)
        {
            date = date.Trim();
            if (!string.IsNullOrEmpty(date))
                return DateTime.Parse(date, new System.Globalization.CultureInfo("en-GB"));
            return new DateTime();
        }
        public DataTable GetClientTable(string ApiLink, string DataType, string JsonTypeData, string MasSeq, string FtpPath, string FtpFilename)
        {
            ConvertJsonStringToDataTable ConvertTable = new ConvertJsonStringToDataTable();

            var ErrorMsg = "";
            if (DataType == "JSON")
            {
                if (JsonTypeData != "USING POST METHOD" && JsonTypeData != "DIRECT FROM FTP")
                {
                    using (var webClient = new System.Net.WebClient())
                    {
                        var jsonData = "";
                        webClient.Encoding = Encoding.UTF8;
                        jsonData = webClient.DownloadString(ApiLink);
                        try
                        {
                            Dt = ConvertTable.JsonStringToDataTable(jsonData);

                        }
                        catch (Exception ex)
                        {
                            ErrorMsg = "FAIL";
                        }

                        if (ErrorMsg == "FAIL")
                        {
                            try
                            {
                                Dt = JsonConvert.DeserializeObject<DataTable>(jsonData);
                            }
                            catch (Exception ex)
                            {

                                ErrorMsg = "FAIL";
                            }
                        }
                    }

                }
                else
                {
                    EXCEL_DEMAND_MAS obj = _PartyWiseRepository.getAllExcelMas().Where(x => x.SEQ_NO == int.Parse(MasSeq)).FirstOrDefault();
                    if (obj.PARAMETER == null)
                    {
                        Dt = GetPostMethodTable(obj.LINK, "");
                    }
                    else
                    {
                        var JsonString = new StringBuilder();
                        var ParaMetar = obj.PARAMETER.Split('|');
                        var ParaMetarvalue = obj.PARAMETER_VALUE.Split('|');

                        JsonString.Append("{");
                        for (int i = 0; i < ParaMetar.Length; i++)
                        {
                            if (i > ParaMetarvalue.Length - 1)
                            {
                                JsonString.Append("\"" + ParaMetar[i] + "\":\"\",");
                            }
                            else
                            {
                                JsonString.Append("\"" + ParaMetar[i] + "\":\"" + ParaMetarvalue[i] + "\",");
                            }
                        }
                        JsonString.Append("}");
                        Dt = GetPostMethodTable(ApiLink, JsonString.ToString());
                    }

                }
                return Dt;
            }
            else
            {
                if (JsonTypeData == "DIRECT FROM FTP")
                {
                    FtpPath = FtpPath + "\\" + FtpFilename;
                    int index = FtpPath.LastIndexOf('\\');
                    if (index > 0)
                    {
                        string dir = FtpPath.Substring(0, index);
                        string file = FtpPath.Substring(index + 1);
                        string[] files = Directory.GetFiles(dir, file + ".*");
                        if (files.Any())
                        {
                            FtpPath = files[0];
                            Dt = MainWebsite.CommonClass.EPPlusHelper.LoadDataTable(FtpPath.ToString());
                        }
                        else
                        {
                            Dt = null;
                            return Dt;
                        }
                    }
                }
                else
                {
                    DataTable ExcelDt = new DataTable();
                    try
                    {
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ApiLink);
                        string partyname = "exce.xlsx";
                        string file_name = "";
                        HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                        using (Stream rstream = res.GetResponseStream())
                        {
                            file_name = res.Headers["Content-Disposition"] != null ?
                            res.Headers["Content-Disposition"].Replace("attachment; filename=", "").Replace("\"", "") :
                            res.Headers["Location"] != null ? Path.GetFileName(res.Headers["Location"]) :
                            Path.GetFileName(ApiLink).Contains('?') || Path.GetFileName(ApiLink).Contains('=') ?
                            Path.GetFileName(res.ResponseUri.ToString()) : partyname;
                        }
                        res.Close();
                        file_name = file_name.Split(new string[] { "filename=" }, StringSplitOptions.None).Last();
                        file_name = file_name.Replace(" ", "_");
                        var MapPath = System.IO.Path.GetTempPath() + file_name;
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(ApiLink, MapPath);
                        webClient.Dispose();

                        //r Msg = MainWebsite.CommonClass.EPPlusHelper.WorksheetToTable(MapPath);

                        Dt = MainWebsite.CommonClass.EPPlusHelper.LoadDataTable(MapPath);

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
                return Dt;
            }

        }
        #endregion

        #region :: Save Stock With Error Resolve ::
        public ActionResult ErrorResolveData(string FileName, string SuppSeq)
        {

            ExcelDemandMasDTO List = new ExcelDemandMasDTO();
            List = new ExcelDemandMasDTO()
            {
                FILE_NAME = FileName,

            };
            TempData["File_Name"] = FileName;
            TempData["Supp_Seq"] = SuppSeq;
            return View(List);
        }
        [HttpPost]
        public JsonResult GetErrorList()
        {
            var FileName = TempData["File_Name"];
            TempData.Keep("File_Name");
            var SuppSeq = TempData["Supp_Seq"];
            TempData.Keep("Supp_Seq");
            DataTable AllDataClientTable = (DataTable)TempData["AllDataTempTable"];
            TempData.Keep("AllDataTempTable");
            DataTable AllDataMainTable = (DataTable)TempData["MainStock"];
            TempData.Keep("MainStock");

            DataTable TempTable = null;

            var Table = AllDataClientTable.AsEnumerable()
                           .Where(r => r.Field<bool>("ERROR_FLAG") == true && r.Field<string>("FILE_NAME") == FileName.ToString());
            var ErrorList = "";
            if (Table.Any())
            {
                TempTable = Table.CopyToDataTable();
                ErrorList = DataTableToJson(TempTable);
            }
            else
            {
                ErrorList = "";
            }

            return Json(ErrorList);

        }
        [HttpPost]
        public JsonResult GetSeqList(string Table)
        {

            if (Table == "SHAPE")
            {
                TempData["SeqList"] = _PartyWiseRepository.GetShape().ToList();
            }
            else if (Table == "COLOR")
            {
                TempData["SeqList"] = _PartyWiseRepository.GetColor().ToList();
            }
            else if (Table == "PURITY")
            {
                TempData["SeqList"] = _PartyWiseRepository.GetPurity().ToList();
            }
            else if (Table == "CUT")
            {
                TempData["SeqList"] = _PartyWiseRepository.GetCut().ToList();
            }
            else if (Table == "POLISH")
            {
                TempData["SeqList"] = _PartyWiseRepository.GetPolish().ToList();
            }
            else if (Table == "SYMM")
            {
                TempData["SeqList"] = _PartyWiseRepository.GetSymm().ToList();
            }
            else if (Table == "FLS")
            {
                TempData["SeqList"] = _PartyWiseRepository.GetFls().ToList();
            }
            else if (Table == "LAB")
            {
                TempData["SeqList"] = _PartyWiseRepository.GetLab().ToList();
            }
            var List = TempData["SeqList"];
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(List);
            return Json(json);

        }

        [HttpPost]
        public JsonResult UpdateErrorData(string Value, string ReportNo, string flag, string ColumnName, string ErrorRemark)
        {
            var Result = "";
            var data = Value.Split('|');
            try
            {
                //Working Client Stock Start //Save in Client Stock
                DataTable AllDataClientTable = (DataTable)TempData["AllDataTempTable"];
                var FileName = TempData["File_Name"];
                TempData.Keep("File_Name");
                var SuppSeq = TempData["Supp_Seq"];
                TempData.Keep("Supp_Seq");
                if (AllDataClientTable != null)
                {
                    var CleintrowsToUpdate =
                               AllDataClientTable.AsEnumerable().Where(r => r.Field<string>("REPORT_NO") == ReportNo.ToString() && r.Field<string>("FILE_NAME") == FileName.ToString());

                    foreach (var row in CleintrowsToUpdate)
                    {
                        row.SetField(ColumnName.ToString(), data[0].ToString());
                        if (flag == "true")
                        {
                            row.SetField("ERROR_FLAG", false);
                            row.SetField("ERROR_REMARK", "");

                        }

                    }
                    AllDataClientTable.AcceptChanges();
                    //End              
                    TempData["AllDataTempTable"] = AllDataClientTable;
                    Result = "SUCCESS";

                }

                DataTable AllDataMainTable = (DataTable)TempData["MainStock"];
                if (AllDataMainTable != null)
                {
                    var MainrowsToUpdate =
                                  AllDataMainTable.AsEnumerable().Where(r => r.Field<string>("REPORT_NO") == ReportNo.ToString() && r.Field<string>("FILE_NAME") == FileName.ToString());
                    foreach (var row in MainrowsToUpdate)
                    {
                        row.SetField(ColumnName.ToString() + "_SEQ", data[1]);
                    }
                    AllDataMainTable.AcceptChanges();
                    TempData["MainStock"] = AllDataMainTable;
                    //End
                    Result = "SUCCESS";
                }

                SaveSyNomList(int.Parse(data[1]), ErrorRemark, ColumnName);//Save Client SyNom

            }
            catch (Exception ex)
            {
                Result = "FAIL";
                throw;
            }

            return Json(Result);
        }

        [HttpPost]
        public JsonResult RejectTempStockData(string ReportNo)
        {
            string Result = string.Empty;
            var _ReportNo = ReportNo.Split(',');
            try
            {
                DataTable AllDataClientTable = (DataTable)TempData["AllDataTempTable"];
                var FileName = TempData["File_Name"];
                TempData.Keep("File_Name");
                var SuppSeq = TempData["Supp_Seq"];
                TempData.Keep("Supp_Seq");
                if (AllDataClientTable != null)
                {
                    for (int i = 0; i < _ReportNo.Length; i++)
                    {

                        AllDataClientTable.AsEnumerable().Where(r => r.Field<string>("REPORT_NO") == _ReportNo[i].ToString() && r.Field<string>("FILE_NAME") == FileName.ToString()).ToList().ForEach(row => row.Delete());

                        AllDataClientTable.AcceptChanges();
                    }
                    TempData["AllDataTempTable"] = AllDataClientTable;
                    //  TempData.Keep("AllDataTempTable");
                    Result = "SUCCESS";
                }
                else
                {
                    Result = "FAIL";
                }
                DataTable AllDataMainTable = (DataTable)TempData["MainStock"];
                if (AllDataMainTable != null)
                {
                    for (int i = 0; i < _ReportNo.Length; i++)
                    {
                        AllDataMainTable.AsEnumerable().Where(r => r.Field<string>("REPORT_NO") == _ReportNo[i].ToString() && r.Field<string>("FILE_NAME") == FileName.ToString()).ToList().ForEach(row => row.Delete());

                        AllDataMainTable.AcceptChanges();
                    }
                    TempData["MainStock"] = AllDataMainTable;
                    // TempData.Keep("MainStock");
                    Result = "SUCCESS";
                }
                else
                {

                    Result = "FAIL";
                }
            }
            catch (Exception ex)
            {
                Result = "FAIL";
                throw;
            }

            return Json(Result);
        }

        [HttpPost]
        public JsonResult SaveTempStockData()
        {
            string Result = string.Empty;
            TempData.Keep("File_Name");
            var SuppSeq = TempData["Supp_Seq"];            
            try
            {
                DataTable AllDataClientTable = (DataTable)TempData["AllDataTempTable"];
                TempData.Keep("AllDataTempTable");

                var FileName = TempData["File_Name"];
                TempData.Keep("Supp_Seq");

                var RowCount = AllDataClientTable.AsEnumerable()
                           .Where(r => r.Field<bool>("ERROR_FLAG") == true && r.Field<string>("FILE_NAME") == FileName.ToString());
                if (RowCount.Any())
                {
                    Result = "FAIL";
                }
                else
                {
                    // TempTable = RowCount.CopyToDataTable();
                    DataTable Dt = AllDataClientTable.AsEnumerable()
                           .Where(r => r.Field<string>("FILE_NAME") == FileName.ToString())
                           .CopyToDataTable();

                    Dt.Columns.Remove("FILE_NAME");
                    Dt.Columns.Remove("LINE_NUMBER");
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(@"Data Source=bwserver; Initial Catalog=TradingDB; User ID=sa; Password=P@ssw0rd;");
                    bulkCopy.DestinationTableName = "CLIENT_STOCK";
                    bulkCopy.WriteToServer(Dt);
                    Result = "SUCCESS";

                    DataTable AllDataMainTable = (DataTable)TempData["MainStock"];
                    TempData.Keep("MainStock");

                    DataTable MDt = AllDataMainTable.AsEnumerable()
                           .Where(r => r.Field<string>("FILE_NAME") == FileName.ToString())
                           .CopyToDataTable();
                    MDt.Columns.Remove("FILE_NAME");
                    MDt.Columns.Remove("LINE_NUMBER");
                    MDt.Columns.Remove("ERROR_FLAG");

                    var MStock = _PartyWiseRepository.DeleteMainStock(SuppSeq.ToString());

                    if (MStock != "FAIL")
                    {
                        SqlBulkCopy bulkCopyMianStk = new SqlBulkCopy(@"Data Source=bwserver; Initial Catalog=TradingDB; User ID=sa; Password=P@ssw0rd;");
                        bulkCopyMianStk.DestinationTableName = "STOCK";
                        bulkCopyMianStk.WriteToServer(MDt);
                        Result = "SUCCESS";
                    }
                    else
                    {
                        Result = "FAIL";
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "FAIL";
                throw;
            }

            return Json(Result);
        }
        #endregion

        #region :: Save Stock With Ignore Error Resolve ::

        [HttpPost]
        public JsonResult IgnoreTempStockData(string FileName)
        {
            string Result = string.Empty;
            var _FileName = FileName.Split(',');
            try
            {
                DataTable AllDataClientTable = (DataTable)TempData["AllDataTempTable"];               

                if (AllDataClientTable != null)
                {
                    for (int i = 0; i < _FileName.Length; i++)
                    {
                        AllDataClientTable.AsEnumerable().Where(r => r.Field<bool>("ERROR_FLAG") == true && r.Field<string>("FILE_NAME") == _FileName[i].ToString()).ToList().ForEach(row => row.Delete());

                        AllDataClientTable.AcceptChanges();
                    }

                    TempData["AllDataTempTable"] = AllDataClientTable;
                    TempData.Keep("AllDataTempTable");
                    Result = "SUCCESS";
                }
                else
                {
                    Result = "FAIL";
                }
                DataTable AllDataMainTable = (DataTable)TempData["MainStock"];
                if (AllDataMainTable != null)
                {
                    for (int i = 0; i < _FileName.Length; i++)
                    {
                        AllDataMainTable.AsEnumerable().Where(r => r.Field<bool>("ERROR_FLAG") == true && r.Field<string>("FILE_NAME") == _FileName[i].ToString()).ToList().ForEach(row => row.Delete());

                        AllDataMainTable.AcceptChanges();
                    }
                    TempData["MainStock"] = AllDataMainTable;
                    TempData.Keep("MainStock");
                    Result = "SUCCESS";
                }
                else
                {

                    Result = "FAIL";
                }
            }
            catch (Exception ex)
            {
                Result = "FAIL";
                throw;
            }
            return Json(Result);
        }

        [HttpPost]
        public JsonResult SaveAllTempStockData(string AllFile = "", string AllSuppSeq = "")
        {
            string Result = string.Empty;
            var _AllFile = AllFile.Split(',');
            var _AllSuppSeq = AllSuppSeq.Split(',');
            try
            {
                DataTable AllDataClientTable = (DataTable)TempData["AllDataTempTable"];
                TempData.Keep("AllDataTempTable");

                for (int i = 0; i < _AllFile.Length; i++)
                {
                    DataTable Dt = null;
                    Dt = AllDataClientTable.AsEnumerable()
                        .Where(r => r.Field<string>("FILE_NAME") == _AllFile[i].ToString())
                        .CopyToDataTable();

                    Dt.Columns.Remove("FILE_NAME");
                    Dt.Columns.Remove("LINE_NUMBER");
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(@"Data Source=bwserver; Initial Catalog=TradingDB; User ID=sa; Password=P@ssw0rd;");
                    bulkCopy.DestinationTableName = "CLIENT_STOCK";
                    bulkCopy.WriteToServer(Dt);
                    Result = "SUCCESS";

                    DataTable AllDataMainTable = (DataTable)TempData["MainStock"];
                    TempData.Keep("MainStock");
                    DataTable MDt = null;
                    MDt = AllDataMainTable.AsEnumerable()
                          .Where(r => r.Field<string>("FILE_NAME") == _AllFile[i].ToString())
                          .CopyToDataTable();
                    MDt.Columns.Remove("FILE_NAME");
                    MDt.Columns.Remove("LINE_NUMBER");
                    MDt.Columns.Remove("ERROR_FLAG");

                    var MStock = _PartyWiseRepository.DeleteMainStock(_AllSuppSeq[i].ToString());

                    if (MStock != "FAIL")
                    {
                        SqlBulkCopy bulkCopyMianStk = new SqlBulkCopy(@"Data Source=bwserver; Initial Catalog=TradingDB; User ID=sa; Password=P@ssw0rd;");
                        bulkCopyMianStk.DestinationTableName = "STOCK";
                        bulkCopyMianStk.WriteToServer(MDt);
                        Result = "SUCCESS";
                    }
                    else
                    {
                        Result = "FAIL";
                    }
                }
            }
            catch (Exception ex)
            {
                Result = "FAIL";
                throw;
            }

            return Json(Result);
        }

        #endregion

        #region :: Common Method ::
        /// <summary>
        /// Common Collumn Mapping
        /// </summary>
        /// <param name="dtCOLS"></param>
        /// <returns>Pass Matching Column From Client Header list Our Database Column And Save</returns>
        private DataTable ColumnMapping(DataTable dtCOLS)
        {
            try
            {
                ConvertJsonStringToDataTable ConvertTable = new ConvertJsonStringToDataTable();

                excelColumnList = (from dc in dtCOLS.Columns.Cast<DataColumn>() select dc.ColumnName).ToArray();
                TempData["excelColumnList"] = excelColumnList;
                TempData.Keep("excelColumnList");
                string[] SynonymsList = null;
                string DbColumns = "", Caption = "", ExcelColumns = "";
                List<COLUMN_MAS> CaptionList = new List<COLUMN_MAS>();
                var Captiondata = _PartyWiseRepository.GetAllColumnMas().ToList();

                tblMAPPING = ConvertTable.ToDataTable(Captiondata);
                tblMAPPING.Columns.Add("EXCEL_COLUMN", typeof(string));
                foreach (DataRow DrColumns in tblMAPPING.Rows)
                {
                    Caption = DrColumns["DISPLAY_NAME"].ToString().ToUpper();
                    DbColumns = DrColumns["COLUMN_NAME"].ToString().ToUpper();
                    SynonymsList = DrColumns["PARA_SYNONYM"].ToString().ToUpper().Split(new string[] { "|", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    ExcelColumns = "";

                    if (excelColumnList.Contains(Caption))
                        ExcelColumns = Caption;
                    else if (excelColumnList.Contains(DbColumns))
                        ExcelColumns = DbColumns.Trim();
                    else
                    {
                        foreach (string excelCol in excelColumnList)
                        {
                            if (SynonymsList.Contains(excelCol.ToUpper()))
                                ExcelColumns = excelCol;
                        }
                    }
                    DrColumns["EXCEL_COLUMN"] = ExcelColumns;
                }
                return tblMAPPING;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Common Method Get Data From Api link and convert into List For Display
        /// </summary>
        /// <param name="ApiLink"></param>
        public void GetTableFrmoApiCommon(string ApiLink)
        {
            ConvertJsonStringToDataTable ConvertTable = new ConvertJsonStringToDataTable();
            DataTable Dt = new DataTable();
            var ErrorMsg = "";
            using (var webClient = new System.Net.WebClient())
            {
                var jsonData = "";
                webClient.Encoding = Encoding.UTF8;
                jsonData = webClient.DownloadString(ApiLink);
                try
                {
                    Dt = ConvertTable.JsonStringToDataTable(jsonData);
                }
                catch (Exception ex)
                {
                    ErrorMsg = "FAIL";
                }

                if (ErrorMsg == "FAIL")
                {
                    try
                    {
                        Dt = JsonConvert.DeserializeObject<DataTable>(jsonData);
                    }
                    catch (Exception ex)
                    {

                        ErrorMsg = "FAIL";
                    }
                }
                DataTable DtTable = new DataTable();

                DtTable = ColumnMapping(Dt);
                List<ColumnMasDto> ColumnMas = new List<ColumnMasDto>();
                foreach (DataRow row in DtTable.Rows)
                {
                    ColumnMas.Add(new ColumnMasDto
                    {
                        SEQ_NO = Convert.ToInt32(row["SEQ_NO"]),
                        COLUMN_NAME = row["COLUMN_NAME"].ToString(),
                        DISPLAY_NAME = row["DISPLAY_NAME"].ToString(),
                        PARA_SYNONYM = row["PARA_SYNONYM"].ToString(),
                        SORT_NO = Convert.ToInt16(row["SORT_NO"]),
                        EXCEL_COLUMN = row["EXCEL_COLUMN"].ToString(),
                    });
                }
                List<COLUMN_MAS> obj = _PartyWiseRepository.GetAllColumnMas().ToList();

                TempData["CaptionList"] = ColumnMas;
                TempData.Keep("CaptionList");

            }

        }
        public DataTable GetPostMethodTable(string ApiLink, string JsonObj)
        {
            ConvertJsonStringToDataTable ConvertTable = new ConvertJsonStringToDataTable();
            DataTable Dt = new DataTable();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiLink);
                httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonObj);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Dt = ConvertTable.JsonStringToDataTable(result);
                }
                return Dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void GetTableWithPost(string ApiLink, string JsonObj)
        {
            ConvertJsonStringToDataTable ConvertTable = new ConvertJsonStringToDataTable();
            DataTable Dt = new DataTable();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiLink);
                httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonObj);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Dt = ConvertTable.JsonStringToDataTable(result);
                }
                DataTable DtTable = new DataTable();

                DtTable = ColumnMapping(Dt);
                List<ColumnMasDto> ColumnMas = new List<ColumnMasDto>();
                foreach (DataRow row in DtTable.Rows)
                {
                    ColumnMas.Add(new ColumnMasDto
                    {
                        SEQ_NO = Convert.ToInt32(row["SEQ_NO"]),
                        COLUMN_NAME = row["COLUMN_NAME"].ToString(),
                        DISPLAY_NAME = row["DISPLAY_NAME"].ToString(),
                        PARA_SYNONYM = row["PARA_SYNONYM"].ToString(),
                        SORT_NO = Convert.ToInt16(row["SORT_NO"]),
                        EXCEL_COLUMN = row["EXCEL_COLUMN"].ToString(),
                    });
                }
                List<COLUMN_MAS> obj = _PartyWiseRepository.GetAllColumnMas().ToList();

                TempData["CaptionList"] = ColumnMas;
                TempData.Keep("CaptionList");

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public DataTable GetExcelDatatable(string Apilink)
        {
            DataTable ExcelDt = new DataTable();
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Apilink);
                string partyname = "exce.xlsx";
                string file_name = "";
                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                using (Stream rstream = res.GetResponseStream())
                {
                    file_name = res.Headers["Content-Disposition"] != null ?
                    res.Headers["Content-Disposition"].Replace("attachment; filename=", "").Replace("\"", "") :
                    res.Headers["Location"] != null ? Path.GetFileName(res.Headers["Location"]) :
                    Path.GetFileName(Apilink).Contains('?') || Path.GetFileName(Apilink).Contains('=') ?
                    Path.GetFileName(res.ResponseUri.ToString()) : partyname;
                }
                res.Close();
                file_name = file_name.Split(new string[] { "filename=" }, StringSplitOptions.None).Last();
                file_name = file_name.Replace(" ", "_");
                var MapPath = System.IO.Path.GetTempPath() + file_name;
                WebClient webClient = new WebClient();
                webClient.DownloadFile(Apilink, MapPath);
                webClient.Dispose();

                ExcelDt = MainWebsite.CommonClass.EPPlusHelper.LoadDataTable(MapPath);

                DataTable MapTable = new DataTable();
                MapTable = ColumnMapping(ExcelDt);
                List<ColumnMasDto> ColumnMas = new List<ColumnMasDto>();
                foreach (DataRow row in MapTable.Rows)
                {
                    ColumnMas.Add(new ColumnMasDto
                    {
                        SEQ_NO = Convert.ToInt32(row["SEQ_NO"]),
                        COLUMN_NAME = row["COLUMN_NAME"].ToString(),
                        DISPLAY_NAME = row["DISPLAY_NAME"].ToString(),
                        PARA_SYNONYM = row["PARA_SYNONYM"].ToString(),
                        SORT_NO = Convert.ToInt16(row["SORT_NO"]),
                        EXCEL_COLUMN = row["EXCEL_COLUMN"].ToString(),
                    });
                }

                TempData["CaptionList"] = ColumnMas;
                TempData.Keep("CaptionList");
            }
            catch (Exception ex)
            {

                throw;
            }
            return ExcelDt;
        }

        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            return dataTable;
        }
        public string DataTableToJson(DataTable table)
        {
            string JSONString = string.Empty;
            string jsonString = JsonConvert.SerializeObject(table);
            JSONString = jsonString.Replace("null", @"""""");
            return JSONString;
        }

        public void SaveSyNomList(int SeqNo, string ErrorRemark, string ColumnName)
        {
            if (ErrorRemark != "" && ErrorRemark != null)
            {
                if (ErrorRemark.Contains(ColumnName.ToString()))
                {
                    var ErrorArray = ErrorRemark.Split(',');
                    for (int i = 0; i < ErrorArray.Length; i++)
                    {
                        var ErrorColum = ErrorArray[i].Split(':');
                        for (int j = 0; j < 1; j++)
                        {
                            if (ErrorColum[j].Trim().ToUpper() == "SHAPE" && ColumnName.ToString() == "SHAPE")
                            {
                                if (ErrorColum[j + 1].Trim().ToString() != "")
                                {
                                    _PartyWiseRepository.UpdateShapeMas(SeqNo, ErrorColum[j + 1].Trim());
                                }
                            }
                            else if (ErrorColum[j].Trim().ToUpper() == "COLOR" && ColumnName.ToString() == "COLOR")
                            {
                                if (ErrorColum[j + 1].Trim().ToString() != "")
                                {
                                    _PartyWiseRepository.UpdateColorMas(SeqNo, ErrorColum[j + 1].Trim());
                                }
                            }
                            else if (ErrorColum[j].Trim().ToUpper() == "CUT" && ColumnName.ToString() == "CUT")
                            {
                                if (ErrorColum[j + 1].Trim().ToString() != "")
                                {
                                    _PartyWiseRepository.UpdateCutMas(SeqNo, ErrorColum[j + 1].Trim());
                                }
                            }
                            else if (ErrorColum[j].Trim().ToUpper() == "POLISH" && ColumnName.ToString() == "POLISH")
                            {
                                if (ErrorColum[j + 1].Trim().ToString() != "")
                                {
                                    _PartyWiseRepository.UpdatePolishMas(SeqNo, ErrorColum[j + 1].Trim());
                                }
                            }
                            else if (ErrorColum[j].Trim().ToUpper() == "SYMM" && ColumnName.ToString() == "SYMM")
                            {
                                if (ErrorColum[j + 1].Trim().ToString() != "")
                                {
                                    _PartyWiseRepository.UpdateSymmMas(SeqNo, ErrorColum[j + 1].Trim());
                                }
                            }
                            else if (ErrorColum[j].Trim().ToUpper() == "FLS" && ColumnName.ToString() == "FLS")
                            {
                                if (ErrorColum[j + 1].Trim().ToString() != "")
                                {
                                    _PartyWiseRepository.UpdateFlsMas(SeqNo, ErrorColum[j + 1].Trim());
                                }
                            }
                            else if (ErrorColum[j].Trim().ToUpper() == "LAB" && ColumnName.ToString() == "LAB")
                            {
                                if (ErrorColum[j + 1].Trim().ToString() != "")
                                {
                                    _PartyWiseRepository.UpdateLabMas(SeqNo, ErrorColum[j + 1].Trim());
                                }
                            }
                        }
                    }
                }

            }

        }
        public string GetDatafromFTPFile(string FtpPath)
        {
            DataTable ExcelDt = null;
            string Result = "";
            int index = FtpPath.LastIndexOf('\\');
            if (index > 0)
            {
                string dir = FtpPath.Substring(0, index);
                string file = FtpPath.Substring(index + 1);
                string[] files = Directory.GetFiles(dir, file + ".*");
                if (files.Any())
                {
                    FtpPath = files[0];
                }
                else
                {
                    Result = "File Not Found";
                    return Result;
                }
            }

            ExcelDt = MainWebsite.CommonClass.EPPlusHelper.LoadDataTable(FtpPath.ToString());

            DataTable MapTable = new DataTable();
            MapTable = ColumnMapping(ExcelDt);
            List<ColumnMasDto> ColumnMas = new List<ColumnMasDto>();
            foreach (DataRow row in MapTable.Rows)
            {
                ColumnMas.Add(new ColumnMasDto
                {
                    SEQ_NO = Convert.ToInt32(row["SEQ_NO"]),
                    COLUMN_NAME = row["COLUMN_NAME"].ToString(),
                    DISPLAY_NAME = row["DISPLAY_NAME"].ToString(),
                    PARA_SYNONYM = row["PARA_SYNONYM"].ToString(),
                    SORT_NO = Convert.ToInt16(row["SORT_NO"]),
                    EXCEL_COLUMN = row["EXCEL_COLUMN"].ToString(),
                });
            }

            TempData["CaptionList"] = ColumnMas;
            TempData.Keep("CaptionList");
            Result = "SUCCESS";
            return Result;
        }
       
        public string StringBuilder(string ParaMetar, string ParaMetarvalue)
        {
            string Json = string.Empty;
            var JsonString = new StringBuilder();
            var _ParaMetar = ParaMetar.Split('|');
            var _ParaMetarvalue = ParaMetarvalue.Split('|');
            JsonString.Append("{");
            for (int i = 0; i < _ParaMetar.Length; i++)
            {
                if (i > _ParaMetarvalue.Length - 1)
                {
                    JsonString.Append("\"" + _ParaMetar[i] + "\":\"\",");
                }
                else
                {
                    JsonString.Append("\"" + _ParaMetar[i] + "\":\"" + _ParaMetarvalue[i] + "\",");
                }
            }
            JsonString.Append("}");
            Json = JsonString.ToString();
            return Json;
        }
        #endregion

    }
}