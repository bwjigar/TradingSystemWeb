using System;
using System.Collections.Generic;
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
}
