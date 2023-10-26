using Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Sunrise.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Xml;

namespace Lib.Repository
{
    public static class CustomerRepo
    {
        public static int TotCount = 0;
        public static ServiceResponse<Get_API_StockFilter_Response> Get_API_StockFilter()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("API_StockFilter", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_API_StockFilter_Response> list = new List<Get_API_StockFilter_Response>();
                    list = DataTableExtension.ToList<Get_API_StockFilter_Response>(dt);

                    return (new ServiceResponse<Get_API_StockFilter_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<Get_API_StockFilter_Response>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<Get_API_StockFilter_Response>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static ServiceResponse<Get_API_ColumnMas_Response> Get_API_ColumnMas(int iTransId)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, iTransId));

                DataTable dt = db.ExecuteSP("API_ColumnMas", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_API_ColumnMas_Response> list = new List<Get_API_ColumnMas_Response>();
                    list = DataTableExtension.ToList<Get_API_ColumnMas_Response>(dt);

                    return (new ServiceResponse<Get_API_ColumnMas_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<Get_API_ColumnMas_Response>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<Get_API_ColumnMas_Response>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static ServiceResponse<Get_ApiUploadMst_Response> Get_ApiUploadMst(Get_ApiUploadMst_Request get_apiuploadmst)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (get_apiuploadmst.TransId > 0)
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.TransId));
                else
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.sSearch))
                    para.Add(db.CreateParam("sSearch", DbType.String, ParameterDirection.Input, get_apiuploadmst.sSearch));
                else
                    para.Add(db.CreateParam("sSearch", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.UserId > 0)
                    para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.UserId));
                else
                    para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.dtFromDate))
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, get_apiuploadmst.dtFromDate));
                else
                    para.Add(db.CreateParam("dtFromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.dtToDate))
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, get_apiuploadmst.dtToDate));
                else
                    para.Add(db.CreateParam("dtToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.iPgNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.iPgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, get_apiuploadmst.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_ApiUploadMst_Response> list = new List<Get_ApiUploadMst_Response>();
                    list = DataTableExtension.ToList<Get_ApiUploadMst_Response>(dt);

                    return (new ServiceResponse<Get_ApiUploadMst_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<Get_ApiUploadMst_Response>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<Get_ApiUploadMst_Response>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static ServiceResponse<APIFiltersSettings> Get_ApiFilter(Get_ApiUploadMst_Request get_apiuploadmst)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (get_apiuploadmst.TransId > 0)
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.TransId));
                else
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_Filters_Select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<APIFiltersSettings> list = new List<APIFiltersSettings>();
                    list = DataTableExtension.ToList<APIFiltersSettings>(dt);

                    return (new ServiceResponse<APIFiltersSettings>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<APIFiltersSettings>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<APIFiltersSettings>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static ServiceResponse<CommonResponse> SaveUploadMethod(ApiUploadMethod apiuploadmethod)
        {
            try
            {
                Database db = new Database();

                CommonResponse resp = new CommonResponse();
                DataTable _dt = ApiUploadMethod_viewAll(1, 1000, 0);
                DataView _dv = new DataView(_dt);
                _dv.RowFilter = "APIName = '" + apiuploadmethod.APIName.Trim() + "'";
                if (apiuploadmethod.iTransId > 0)
                    _dv.RowFilter += " AND iTransId <> '" + apiuploadmethod.iTransId + "'";
                _dt = _dv.ToTable();
                if (_dt.Rows.Count > 0)
                {
                    return (new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = "File Name Same " + apiuploadmethod.APIName.Trim(),
                        Status = "-1"
                    });
                }

                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                if (!string.IsNullOrEmpty(apiuploadmethod.ApiMethod))
                    para.Add(db.CreateParam("ApiMethod", DbType.String, ParameterDirection.Input, apiuploadmethod.ApiMethod));
                else
                    para.Add(db.CreateParam("ApiMethod", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.WebAPIUserName))
                    para.Add(db.CreateParam("WebAPIUserName", DbType.String, ParameterDirection.Input, apiuploadmethod.WebAPIUserName));
                else
                    para.Add(db.CreateParam("WebAPIUserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.WebAPIPassword))
                    para.Add(db.CreateParam("WebAPIPassword", DbType.String, ParameterDirection.Input, apiuploadmethod.WebAPIPassword));
                else
                    para.Add(db.CreateParam("WebAPIPassword", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FTPHost))
                    para.Add(db.CreateParam("FTPHost", DbType.String, ParameterDirection.Input, apiuploadmethod.FTPHost));
                else
                    para.Add(db.CreateParam("FTPHost", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FTPUser))
                    para.Add(db.CreateParam("FTPUser", DbType.String, ParameterDirection.Input, apiuploadmethod.FTPUser));
                else
                    para.Add(db.CreateParam("FTPUser", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FTPUser))
                    para.Add(db.CreateParam("FTPPass", DbType.String, ParameterDirection.Input, apiuploadmethod.FTPPass));
                else
                    para.Add(db.CreateParam("FTPPass", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FTPType))
                    para.Add(db.CreateParam("FtpType", DbType.String, ParameterDirection.Input, apiuploadmethod.FTPType));
                else
                    para.Add(db.CreateParam("FtpType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FTPExportType))
                    para.Add(db.CreateParam("FTPExportType", DbType.String, ParameterDirection.Input, apiuploadmethod.FTPExportType));
                else
                    para.Add(db.CreateParam("FTPExportType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.URLUserName))
                    para.Add(db.CreateParam("URLUserName", DbType.String, ParameterDirection.Input, apiuploadmethod.URLUserName));
                else
                    para.Add(db.CreateParam("URLUserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.URLPassword))
                    para.Add(db.CreateParam("URLPassword", DbType.String, ParameterDirection.Input, apiuploadmethod.URLPassword));
                else
                    para.Add(db.CreateParam("URLPassword", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.URLExportType))
                    para.Add(db.CreateParam("URLExportType", DbType.String, ParameterDirection.Input, apiuploadmethod.URLExportType));
                else
                    para.Add(db.CreateParam("URLExportType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.FileLocation))
                    para.Add(db.CreateParam("FileLocation", DbType.String, ParameterDirection.Input, apiuploadmethod.FileLocation));
                else
                    para.Add(db.CreateParam("FileLocation", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.LocationExportType))
                    para.Add(db.CreateParam("LocationExportType", DbType.String, ParameterDirection.Input, apiuploadmethod.LocationExportType));
                else
                    para.Add(db.CreateParam("LocationExportType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.LocationTransType))
                    para.Add(db.CreateParam("LocationTransType", DbType.String, ParameterDirection.Input, apiuploadmethod.LocationTransType));
                else
                    para.Add(db.CreateParam("LocationTransType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.LocationTransType) && apiuploadmethod.LocationTransType.ToUpper() == "ONETIME")
                    para.Add(db.CreateParam("OnetimeDate", DbType.DateTime, ParameterDirection.Input, apiuploadmethod.OnetimeDate));
                else
                    para.Add(db.CreateParam("OnetimeDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.Onetime))
                    para.Add(db.CreateParam("Onetime", DbType.String, ParameterDirection.Input, apiuploadmethod.Onetime));
                else
                    para.Add(db.CreateParam("Onetime", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.RepeateveryType))
                    para.Add(db.CreateParam("RepeateveryType", DbType.String, ParameterDirection.Input, apiuploadmethod.RepeateveryType));
                else
                    para.Add(db.CreateParam("RepeateveryType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.LocationTransType) && apiuploadmethod.LocationTransType.ToUpper() == "REPEATEVERY")
                    para.Add(db.CreateParam("Repeatevery", DbType.Decimal, ParameterDirection.Input, apiuploadmethod.Repeatevery));
                else
                    para.Add(db.CreateParam("Repeatevery", DbType.Decimal, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.APIName))
                    para.Add(db.CreateParam("APIName", DbType.String, ParameterDirection.Input, apiuploadmethod.APIName));
                else
                    para.Add(db.CreateParam("APIName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(apiuploadmethod.APIStatus.ToString()))
                    para.Add(db.CreateParam("APIStatus", DbType.String, ParameterDirection.Input, apiuploadmethod.APIStatus));
                else
                    para.Add(db.CreateParam("APIStatus", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (apiuploadmethod.For_iUserId > 0)
                    para.Add(db.CreateParam("For_iUserId", DbType.Int64, ParameterDirection.Input, apiuploadmethod.For_iUserId));
                else
                    para.Add(db.CreateParam("For_iUserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                string columnssettingsvalue = IPadCommon.ToXML<List<ApiColumnsSettings>>(apiuploadmethod.ColumnsSettings);
                para.Add(db.CreateParam("columnsDefs", DbType.String, ParameterDirection.Input, columnssettingsvalue));

                string apifilterssettingsvalue = IPadCommon.ToXML<List<APIFiltersSettings>>(apiuploadmethod.APIFilters);
                para.Add(db.CreateParam("APIFilters", DbType.String, ParameterDirection.Input, apifilterssettingsvalue));

                para.Add(db.CreateParam("iTransId", DbType.Int64, ParameterDirection.Input, apiuploadmethod.iTransId));

                para.Add(db.CreateParam("APIUrl", DbType.String, ParameterDirection.Input, apiuploadmethod.APIUrl));

                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_InsUpd", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int iTransId = 0;
                    string Message = dt.Rows[0]["Message"].ToString();
                    if ((dt.Rows[0]["iTransId"].ToString() != "" ? Int32.Parse(dt.Rows[0]["iTransId"].ToString()) : 0) > 0)
                    {
                        iTransId = Convert.ToInt32(dt.Rows[0]["iTransId"]);
                    }
                    return (new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = (iTransId == 0 ? Message : Convert.ToString(iTransId)),
                        Status = (iTransId == 0 ? "-1" : "1")
                    });
                }
                else
                {
                    return (new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = "No Data Found",
                        Status = "-1"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<CommonResponse>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static DataTable ApiUploadMethod_viewAll(int iPgNo, int iPgSize, int TransId = 0)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (TransId > 0)
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, TransId));
                else
                    para.Add(db.CreateParam("TransId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dtFromDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("dtToDate", DbType.DateTime, ParameterDirection.Input, DBNull.Value));
                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iPgNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                if (iPgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                DataTable dt = db.ExecuteSP("ApiUploadMethodMst_select", para.ToArray(), false);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ServiceResponse<KeyToSymbolResponse> Get_KeyToSymbol()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("get_key_to_symbol", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<KeyToSymbolResponse> list = new List<KeyToSymbolResponse>();
                    list = DataTableExtension.ToList<KeyToSymbolResponse>(dt);

                    return (new ServiceResponse<KeyToSymbolResponse>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<KeyToSymbolResponse>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<KeyToSymbolResponse>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static ServiceResponse<Get_APIMst_Response> Get_APIMst(Get_APIMst_Request get_apiuploadmst)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (get_apiuploadmst.Id > 0)
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.Id));
                else
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.Search))
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, get_apiuploadmst.Search));
                else
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, get_apiuploadmst.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, get_apiuploadmst.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.iPgNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (get_apiuploadmst.iPgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, get_apiuploadmst.iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(get_apiuploadmst.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, get_apiuploadmst.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("APIMst_select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_APIMst_Response> list = new List<Get_APIMst_Response>();
                    list = DataTableExtension.ToList<Get_APIMst_Response>(dt);

                    return (new ServiceResponse<Get_APIMst_Response>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<Get_APIMst_Response>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<Get_APIMst_Response>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static ServiceResponse<CommonResponse> SaveAPIMst(Save_APIMst_Request saveapimst)
        {
            try
            {
                Database db = new Database();
                CommonResponse resp = new CommonResponse();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, saveapimst.Id));
                para.Add(db.CreateParam("APIURL", DbType.String, ParameterDirection.Input, saveapimst.APIURL));
                para.Add(db.CreateParam("APIName", DbType.String, ParameterDirection.Input, saveapimst.APIName));

                para.Add(db.CreateParam("APIResponseFormat", DbType.String, ParameterDirection.Input, saveapimst.APIResponseFormat));
                para.Add(db.CreateParam("FileLocation", DbType.String, ParameterDirection.Input, saveapimst.FileLocation));
                para.Add(db.CreateParam("LocationExportType", DbType.String, ParameterDirection.Input, saveapimst.LocationExportType));
                para.Add(db.CreateParam("RepeateveryType", DbType.String, ParameterDirection.Input, saveapimst.RepeateveryType));
                para.Add(db.CreateParam("Repeatevery", DbType.String, ParameterDirection.Input, saveapimst.Repeatevery));
                para.Add(db.CreateParam("Active", DbType.Boolean, ParameterDirection.Input, saveapimst.Active));

                if (!string.IsNullOrEmpty(saveapimst.APIMethod))
                    para.Add(db.CreateParam("APIMethod", DbType.String, ParameterDirection.Input, saveapimst.APIMethod));
                else
                    para.Add(db.CreateParam("APIMethod", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(saveapimst.APIHitUrl))
                    para.Add(db.CreateParam("APIHitUrl", DbType.String, ParameterDirection.Input, saveapimst.APIHitUrl));
                else
                    para.Add(db.CreateParam("APIHitUrl", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(saveapimst.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, saveapimst.UserName));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(saveapimst.Password))
                    para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, saveapimst.Password));
                else
                    para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("APIMst_CRUD", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Id"].ToString() != "0")
                {
                    return (new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = dt.Rows[0]["Id"].ToString(),
                        Status = "1"
                    });
                }
                else if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Id"].ToString() == "0")
                {
                    return (new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = dt.Rows[0]["Message"].ToString(),
                        Status = "0"
                    });
                }
                else
                {
                    return (new ServiceResponse<CommonResponse>
                    {
                        Data = null,
                        Message = "No Data Found",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<CommonResponse>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static ServiceResponse<CommonResponse> APIGet()
        {
            Int64 APIMst_Id = 0;
            long dt_APIRes_COUNT = 0;
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();
                DataTable dtAPI = db.ExecuteSP("APIScheduler_select", para.ToArray(), false);

                if (dtAPI != null && dtAPI.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Id", typeof(int));

                    for (int i = 0; i < dtAPI.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Id"] = Convert.ToInt32(dtAPI.Rows[i]["Id"].ToString());
                        dt.Rows.Add(dr);
                    }

                    db = new Database();
                    List<SqlParameter> dtpara = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                    param.Value = dt;
                    dtpara.Add(param);

                    DataTable dtData = db.ExecuteSP("API_Running_Insert", dtpara.ToArray(), false);


                    TotCount = dtAPI.Rows.Count;
                    for (int i = 0; i < dtAPI.Rows.Count; i++)
                    {
                        try
                        {
                            APIMst_Id = Convert.ToInt32(dtAPI.Rows[i]["Id"].ToString());

                            Api_Start_End(APIMst_Id, "Start");

                            string tempPath = dtAPI.Rows[i]["FileLocation"].ToString(),
                                APIFileName = dtAPI.Rows[i]["APIName"].ToString(),
                            _API = String.Empty, UserName = String.Empty, Password = String.Empty, filename = String.Empty, filefullpath = String.Empty;

                            DataTable dt_APIRes = new DataTable();

                            if (!Directory.Exists(tempPath))
                            {
                                Directory.CreateDirectory(tempPath);
                            }

                            if (dtAPI.Rows[i]["APIResponseFormat"].ToString().ToUpper() == "XML")
                            {
                                WebClient client = new WebClient();
                                client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                client.Encoding = Encoding.UTF8;
                                ServicePointManager.Expect100Continue = false;
                                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://61.93.195.114/KRISHDIAMONDSERVICE/KRISHDIAMONDSERVICE.ASMX/GETSTONEDATA?USERNAME=SUNRISE&PASSWORD=04321")
                                {
                                    _API = dtAPI.Rows[i]["APIURL"].ToString();
                                    string[] words = _API.Split('?');
                                    String InputPara = string.Empty;
                                    if (words.Length == 2)
                                    {
                                        InputPara = words[1].ToString();
                                    }

                                    string xml = client.UploadString(_API, InputPara);
                                    client.Dispose();
                                    API_Response_Insert(APIMst_Id, xml, "", ".xml");

                                    ConvertXmlStringToDataTable xDt = new ConvertXmlStringToDataTable();
                                    XmlDocument doc = new XmlDocument();
                                    doc.LoadXml(xml);
                                    XmlElement root = doc.DocumentElement;
                                    XmlNodeList elemList = root.GetElementsByTagName("Row");
                                    dt_APIRes = xDt.ConvertXmlNodeListToDataTable(elemList);

                                }
                            }
                            else if (dtAPI.Rows[i]["APIResponseFormat"].ToString().ToUpper() == "JSON")
                            {
                                if (dtAPI.Rows[i]["APIMethod"].ToString().ToUpper() == "POST")
                                {
                                    string json = string.Empty, Token = string.Empty;
                                    _API = dtAPI.Rows[i]["APIURL"].ToString();
                                    string[] words = _API.Split('?');
                                    String InputPara = string.Empty;
                                    if (words.Length == 2)
                                    {
                                        InputPara = words[1].ToString();
                                    }

                                    if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://136.243.67.27/KAVANPROVIDESTOCK.SVC/GETSTOCK")
                                    {
                                        WebRequest request1 = WebRequest.Create("http://136.243.67.27/KavanProvideStock.svc/GetStock");
                                        request1.Method = "POST";
                                        request1.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray = Encoding.UTF8.GetBytes("");
                                        request1.ContentType = "application/json";
                                        //request1.Headers.Add("Authorization", "Bearer " + Token);
                                        request1.ContentLength = byteArray.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream1 = request1.GetRequestStream();
                                        dataStream1.Write(byteArray, 0, byteArray.Length);
                                        dataStream1.Close();
                                        try
                                        {
                                            //and here is the response.
                                            WebResponse response1 = request1.GetResponse();

                                            Console.WriteLine(((HttpWebResponse)response1).StatusDescription);
                                            dataStream1 = response1.GetResponseStream();
                                            StreamReader reader1 = new StreamReader(dataStream1);
                                            json = reader1.ReadToEnd();
                                            Console.WriteLine(json);
                                            reader1.Close();
                                            dataStream1.Close();
                                            response1.Close();
                                            request1.Abort();

                                            API_Response_Insert(APIMst_Id, json, "", ".json");

                                            JObject o = JObject.Parse(json);
                                            var t = string.Empty;
                                            if (o != null)
                                            {
                                                var test = o.First.First.First.First;
                                                if (test != null)
                                                {
                                                    var test2 = test.ToString();
                                                    t = test2;
                                                    //var test2 = test.First;
                                                    //if (test2 != null)
                                                    //{
                                                    //    Console.Write(test2);
                                                    //    t = test2.Root.Last.First.First.First.ToString();
                                                    //}
                                                }
                                            }
                                            var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                            json = JsonConvert.SerializeObject(json_1);
                                            json = json.Replace("[", "").Replace("]", "");
                                            json = json.Replace("null", "");

                                            API_Response_Insert(APIMst_Id, "", json, ".json");
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://LATTICE-APP.COM/USER-SERVICE/API/USER/LOGIN")
                                    {
                                        LATTICE_Login_Req l_req = new LATTICE_Login_Req();
                                        l_req.username = "Sunrise";
                                        l_req.password = "Sunrise";
                                        l_req.companyName = "mumbai";
                                        l_req.appName = "lattice";

                                        //string inputJson = string.Join("&", l_req.GetType()
                                        //                                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                        //                                                .Where(p => p.GetValue(l_req, null) != null)
                                        //                       .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(l_req).ToString())}"));

                                        string inputJson = JsonConvert.SerializeObject(l_req);

                                        WebRequest request = WebRequest.Create("https://lattice-app.com/user-service/api/user/login");
                                        request.Method = "POST";
                                        request.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray = Encoding.UTF8.GetBytes(inputJson);
                                        request.ContentType = "application/json;";
                                        //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                        request.ContentLength = byteArray.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream = request.GetRequestStream();
                                        dataStream.Write(byteArray, 0, byteArray.Length);
                                        dataStream.Close();
                                        try
                                        {
                                            //and here is the response.
                                            WebResponse response = request.GetResponse();

                                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                            dataStream = response.GetResponseStream();
                                            StreamReader reader = new StreamReader(dataStream);
                                            json = reader.ReadToEnd();
                                            Console.WriteLine(json);
                                            reader.Close();
                                            dataStream.Close();
                                            response.Close();
                                            request.Abort();

                                            LATTICE_Login_Res l_res = new LATTICE_Login_Res();
                                            l_res = (new JavaScriptSerializer()).Deserialize<LATTICE_Login_Res>(json);
                                            Token = l_res.data.access_token;


                                            WebRequest request1 = WebRequest.Create("https://lattice-app.com/master-service/api/template/findStockByExportTemplateIdAndUser");
                                            request1.Method = "POST";
                                            request1.Timeout = 7200000; //2 Hour in milliseconds
                                            byte[] byteArray1 = Encoding.UTF8.GetBytes(InputPara);
                                            request1.ContentType = "application/json";
                                            request1.Headers.Add("Authorization", "Bearer " + Token);
                                            request1.ContentLength = byteArray.Length;

                                            //Here is the Business end of the code...
                                            Stream dataStream1 = request1.GetRequestStream();
                                            dataStream1.Write(byteArray, 0, byteArray.Length);
                                            dataStream1.Close();
                                            try
                                            {
                                                //and here is the response.
                                                WebResponse response1 = request1.GetResponse();

                                                Console.WriteLine(((HttpWebResponse)response1).StatusDescription);
                                                dataStream1 = response1.GetResponseStream();
                                                StreamReader reader1 = new StreamReader(dataStream1);
                                                json = reader1.ReadToEnd();
                                                Console.WriteLine(json);
                                                reader1.Close();
                                                dataStream1.Close();
                                                response1.Close();
                                                request1.Abort();

                                                API_Response_Insert(APIMst_Id, json, "", ".json");
                                                JObject o = JObject.Parse(json);
                                                var t = string.Empty;
                                                if (o != null)
                                                {
                                                    var test = o.First;
                                                    if (test != null)
                                                    {
                                                        var test2 = test.Next.Next.First.ToString();
                                                        t = test2;
                                                        //var test2 = test.First;
                                                        //if (test2 != null)
                                                        //{
                                                        //    Console.Write(test2);
                                                        //    t = test2.Root.Last.First.First.First.ToString();
                                                        //}
                                                    }
                                                }
                                                var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                                json = JsonConvert.SerializeObject(json_1);
                                                json = json.Replace("[", "").Replace("]", "");
                                                json = json.Replace("null", "");

                                                API_Response_Insert(APIMst_Id, "", json, ".json");
                                            }
                                            catch (WebException ex)
                                            {
                                                Api_Start_End(APIMst_Id, "End");
                                                ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                Api_Start_End(APIMst_Id, "End");
                                                ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                            }




                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://WWW.JPDIAM.COM/PLUGIN/APITOOL")
                                    {
                                        JP_Login_Req lgn_req = new JP_Login_Req();
                                        lgn_req.action = "viplogin";
                                        lgn_req.vipid = "sunrise";
                                        //lgn_req.vippsd = "goodluck";
                                        lgn_req.vippsd = "Sunrise@1041";

                                        string inputJson = string.Join("&", lgn_req.GetType()
                                                                                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                                                        .Where(p => p.GetValue(lgn_req, null) != null)
                                                               .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(lgn_req).ToString())}"));

                                        try
                                        {
                                            WebClient client = new WebClient();
                                            client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                            client.Encoding = Encoding.UTF8;
                                            ServicePointManager.Expect100Continue = false;
                                            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                            json = client.UploadString("https://www.jpdiam.com/plugin/apitool", inputJson);
                                            client.Dispose();

                                            JP_Login_Res lgn_res = new JP_Login_Res();
                                            lgn_res = (new JavaScriptSerializer()).Deserialize<JP_Login_Res>(json);
                                            Token = lgn_res.msgdata.token;

                                            if (!string.IsNullOrEmpty(Token))
                                            {
                                                JP_Stock_Req stk_req = new JP_Stock_Req();
                                                stk_req.action = "queryalldiamondstock";
                                                stk_req.token = Token;
                                                stk_req.ispaged = 0;
                                                stk_req.pageindex = 1;

                                                inputJson = string.Join("&", stk_req.GetType()
                                                                                      .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                                                      .Where(p => p.GetValue(stk_req, null) != null)
                                                             .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(stk_req).ToString())}"));

                                                try
                                                {
                                                    client = new WebClient();
                                                    client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                                    client.Encoding = Encoding.UTF8;
                                                    ServicePointManager.Expect100Continue = false;
                                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                                    json = client.UploadString("https://www.jpdiam.com/plugin/apitool", inputJson);
                                                    client.Dispose();

                                                    API_Response_Insert(APIMst_Id, json, "", ".json");
                                                    JObject o = JObject.Parse(json);
                                                    var t = string.Empty;
                                                    if (o != null)
                                                    {
                                                        var test = o.Last;
                                                        if (test != null)
                                                        {
                                                            var test2 = test.Last.Last;
                                                            if (test2 != null)
                                                            {
                                                                t = test2.First.ToString();
                                                            }
                                                        }
                                                    }
                                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                                    json = JsonConvert.SerializeObject(json_1);

                                                    json = json.Replace("[", "").Replace("]", "");
                                                    json = json.Replace("null", "");
                                                    API_Response_Insert(APIMst_Id, "", json, ".json");
                                                }
                                                catch (WebException ex)
                                                {
                                                    Api_Start_End(APIMst_Id, "End");
                                                    ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                                }
                                                catch (Exception ex)
                                                {
                                                    Api_Start_End(APIMst_Id, "End");
                                                    ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                                }

                                            }
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://API1.ANKITGEMS.COM:4443/APIUSER/LOGINCHECK")
                                    {
                                        string Name = dtAPI.Rows[i]["UserName"].ToString();
                                        string password = dtAPI.Rows[i]["Password"].ToString();

                                        WebRequest request = WebRequest.Create("https://api1.ankitgems.com:4443/apiuser/logincheck?Name=" + Name + "&password=" + password);
                                        request.Method = "POST";
                                        request.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray = Encoding.UTF8.GetBytes(InputPara);
                                        request.ContentType = "application/x-www-form-urlencoded";
                                        //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                        request.ContentLength = byteArray.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream = request.GetRequestStream();
                                        dataStream.Write(byteArray, 0, byteArray.Length);
                                        dataStream.Close();
                                        try
                                        {
                                            //and here is the response.
                                            WebResponse response = request.GetResponse();

                                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                            dataStream = response.GetResponseStream();
                                            StreamReader reader = new StreamReader(dataStream);
                                            json = reader.ReadToEnd();
                                            Console.WriteLine(json);
                                            reader.Close();
                                            dataStream.Close();
                                            response.Close();
                                            request.Abort();

                                            AnkitGems _data = new AnkitGems();
                                            _data = (new JavaScriptSerializer()).Deserialize<AnkitGems>(json);
                                            Token = _data.data.accessToken;


                                            WebRequest request1 = WebRequest.Create("https://api1.ankitgems.com:4443/apistock/stockdetail?page=1&limit=100000");
                                            request1.Method = "POST";
                                            request1.Timeout = 7200000; //2 Hour in milliseconds
                                            byte[] byteArray1 = Encoding.UTF8.GetBytes(InputPara);
                                            request1.ContentType = "application/x-www-form-urlencoded";
                                            request1.Headers.Add("Authorization", "Bearer " + Token);
                                            request1.ContentLength = byteArray.Length;

                                            //Here is the Business end of the code...
                                            Stream dataStream1 = request1.GetRequestStream();
                                            dataStream1.Write(byteArray, 0, byteArray.Length);
                                            dataStream1.Close();
                                            try
                                            {
                                                //and here is the response.
                                                WebResponse response1 = request1.GetResponse();

                                                Console.WriteLine(((HttpWebResponse)response1).StatusDescription);
                                                dataStream1 = response1.GetResponseStream();
                                                StreamReader reader1 = new StreamReader(dataStream1);
                                                json = reader1.ReadToEnd();
                                                Console.WriteLine(json);
                                                reader1.Close();
                                                dataStream1.Close();
                                                response1.Close();
                                                request1.Abort();

                                                API_Response_Insert(APIMst_Id, json, "", ".json");
                                                JObject o = JObject.Parse(json);
                                                var t = string.Empty;
                                                if (o != null)
                                                {
                                                    var test = o.First;
                                                    if (test != null)
                                                    {
                                                        var test2 = test.First;
                                                        if (test2 != null)
                                                        {
                                                            Console.Write(test2);
                                                            t = test2.Root.Last.First.First.First.ToString();
                                                        }
                                                    }
                                                }
                                                var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                                json = JsonConvert.SerializeObject(json_1);
                                                json = json.Replace("[", "").Replace("]", "");
                                                json = json.Replace("null", "");

                                                API_Response_Insert(APIMst_Id, "", json, ".json");
                                            }
                                            catch (WebException ex)
                                            {
                                                Api_Start_End(APIMst_Id, "End");
                                                ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                Api_Start_End(APIMst_Id, "End");
                                                ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                            }
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }


                                        //string Name = dtAPI.Rows[i]["UserName"].ToString();
                                        //string password = dtAPI.Rows[i]["Password"].ToString();

                                        //WebClient client = new WebClient();
                                        //client.Headers.Add("Content-type", "application/json");
                                        //client.Encoding = Encoding.UTF8;
                                        //json = client.UploadString("https://api1.ankitgems.com:4443/apiuser/logincheck?Name=" + Name + "&password=" + password, "POST", "");

                                        //AnkitGems _data = new AnkitGems();
                                        //_data = (new JavaScriptSerializer()).Deserialize<AnkitGems>(json);
                                        //Token = _data.data.accessToken;

                                        //WebClient client1 = new WebClient();
                                        //client1.Headers.Add("Authorization", "Bearer " + Token);
                                        //client1.Headers.Add("Content-type", "application/json");
                                        //client1.Encoding = Encoding.UTF8;
                                        //json = client1.UploadString("https://api1.ankitgems.com:4443/apistock/stockdetail?page=1&limit=10000", "POST", "");

                                        //JObject o = JObject.Parse(json);
                                        //var t = string.Empty;
                                        //if (o != null)
                                        //{
                                        //    var test = o.First;
                                        //    if (test != null)
                                        //    {
                                        //        var test2 = test.First;
                                        //        if (test2 != null)
                                        //        {
                                        //            Console.Write(test2);
                                        //            t = test2.Root.Last.First.First.First.ToString();
                                        //        }
                                        //    }
                                        //}
                                        //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        //json = JsonConvert.SerializeObject(json_1);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATA")
                                    {
                                        string Name = dtAPI.Rows[i]["UserName"].ToString();
                                        string password = dtAPI.Rows[i]["Password"].ToString();

                                        SGLoginRequest sgl = new SGLoginRequest();
                                        sgl.UserName = Name;
                                        sgl.Password = password;

                                        String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                        WebClient client = new WebClient();
                                        client.Headers.Add("Content-type", "application/json");
                                        client.Encoding = Encoding.UTF8;
                                        json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                        client.Dispose();

                                        SGLoginResponse sglr = new SGLoginResponse();
                                        sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                        SGStockRequest sgr = new SGStockRequest();
                                        sgr.UserId = sglr.UserId;
                                        sgr.TokenId = sglr.TokenId;

                                        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockData", "POST", InputSRJson);
                                        client1.Dispose();

                                        API_Response_Insert(APIMst_Id, json, "", ".json");
                                        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                        //json_1=json_1.r
                                        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");
                                        API_Response_Insert(APIMst_Id, "", json, ".json");
                                        //try
                                        //{
                                        //    string UserId, TokenId;

                                        //    New_Shairu_Login_Req l_req = new New_Shairu_Login_Req();
                                        //    l_req.UserName = "samit_gandhi";
                                        //    l_req.Password = "missme@hk123";

                                        //    string inputJson = JsonConvert.SerializeObject(l_req);

                                        //    WebClient client = new WebClient();
                                        //    client.Headers.Add("Content-type", "application/json");
                                        //    client.Encoding = Encoding.UTF8;
                                        //    json = client.UploadString("https://shairugems.net:8011/api/buyerv2/login", "POST", inputJson);
                                        //    client.Dispose();

                                        //    New_Shairu_Login_Res l_res = new New_Shairu_Login_Res();
                                        //    l_res = (new JavaScriptSerializer()).Deserialize<New_Shairu_Login_Res>(json);
                                        //    UserId = l_res.UserId;
                                        //    TokenId = l_res.TokenId;

                                        //    New_Shairu_Stock_api_Req stock_req = new New_Shairu_Stock_api_Req();
                                        //    stock_req.UserId = UserId;
                                        //    stock_req.TokenId = TokenId;
                                        //    //stock_req.StoneId = "missme@hk123";

                                        //    inputJson = JsonConvert.SerializeObject(stock_req);

                                        //    WebClient client1 = new WebClient();
                                        //    client1.Headers.Add("Content-type", "application/json");
                                        //    client1.Encoding = Encoding.UTF8;
                                        //    json = client1.UploadString("https://shairugems.net:8011/api/buyerv2/getstockdata", "POST", inputJson);
                                        //    client1.Dispose();

                                        //    JObject o = JObject.Parse(json);
                                        //    var t = string.Empty;
                                        //    if (o != null)
                                        //    {
                                        //        var test = o.First;
                                        //        if (test != null)
                                        //        {
                                        //            var test2 = test.First.ToString();
                                        //            t = test2;
                                        //            //var test2 = test.First;
                                        //            //if (test2 != null)
                                        //            //{
                                        //            //    Console.Write(test2);
                                        //            //    t = test2.Root.Last.First.First.First.ToString();
                                        //            //}
                                        //        }
                                        //    }
                                        //    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        //    json = JsonConvert.SerializeObject(json_1);
                                        //    json = json.Replace("[", "").Replace("]", "");
                                        //    json = json.Replace("null", "");

                                        //}
                                        //catch (WebException ex)
                                        //{
                                        //    Api_Start_End(APIMst_Id, "End");
                                        //    ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    Api_Start_End(APIMst_Id, "End");
                                        //    ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        //}
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATAINDIA")
                                    {
                                        string Name = dtAPI.Rows[i]["UserName"].ToString();
                                        string password = dtAPI.Rows[i]["Password"].ToString();

                                        SGLoginRequest sgl = new SGLoginRequest();
                                        sgl.UserName = Name;
                                        sgl.Password = password;

                                        String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                        WebClient client = new WebClient();
                                        client.Headers.Add("Content-type", "application/json");
                                        client.Encoding = Encoding.UTF8;
                                        json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                        client.Dispose();

                                        SGLoginResponse sglr = new SGLoginResponse();
                                        sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                        SGStockRequest sgr = new SGStockRequest();
                                        sgr.UserId = sglr.UserId;
                                        sgr.TokenId = sglr.TokenId;

                                        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataIndia", "POST", InputSRJson);
                                        client1.Dispose();

                                        API_Response_Insert(APIMst_Id, json, "", ".json");

                                        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                        //json_1=json_1.r
                                        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        API_Response_Insert(APIMst_Id, "", json, ".json");
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATADUBAI")
                                    {
                                        string Name = dtAPI.Rows[i]["UserName"].ToString();
                                        string password = dtAPI.Rows[i]["Password"].ToString();

                                        SGLoginRequest sgl = new SGLoginRequest();
                                        sgl.UserName = Name;
                                        sgl.Password = password;

                                        String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                        WebClient client = new WebClient();
                                        client.Headers.Add("Content-type", "application/json");
                                        client.Encoding = Encoding.UTF8;
                                        json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                        client.Dispose();

                                        SGLoginResponse sglr = new SGLoginResponse();
                                        sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                        SGStockRequest sgr = new SGStockRequest();
                                        sgr.UserId = sglr.UserId;
                                        sgr.TokenId = sglr.TokenId;

                                        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataDubai", "POST", InputSRJson);
                                        client1.Dispose();

                                        API_Response_Insert(APIMst_Id, json, "", ".json");
                                        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                        //json_1=json_1.r
                                        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        API_Response_Insert(APIMst_Id, "", json, ".json");
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://PDHK.DIAMX.NET/API/STOCKSEARCH?APITOKEN=3C0DB41E-7B79-48C4-8CBD-1F718DB7263A")
                                    {
                                        WebRequest request = WebRequest.Create("http://pdhk.diamx.net/API/StockSearch?APIToken=3c0db41e-7b79-48c4-8cbd-1f718db7263a");
                                        request.Method = "POST";
                                        request.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray = Encoding.UTF8.GetBytes("");
                                        request.ContentType = "application/json";
                                        //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                        request.ContentLength = byteArray.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream = request.GetRequestStream();
                                        dataStream.Write(byteArray, 0, byteArray.Length);
                                        dataStream.Close();
                                        try
                                        {
                                            //and here is the response.
                                            WebResponse response = request.GetResponse();

                                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                            dataStream = response.GetResponseStream();
                                            StreamReader reader = new StreamReader(dataStream);
                                            json = reader.ReadToEnd();
                                            Console.WriteLine(json);
                                            reader.Close();
                                            dataStream.Close();
                                            response.Close();
                                            request.Abort();

                                            API_Response_Insert(APIMst_Id, json, "", ".json");
                                            JObject o = JObject.Parse(json);
                                            var t = string.Empty;
                                            if (o != null)
                                            {
                                                var test = o.First;
                                                if (test != null)
                                                {
                                                    var test2 = test.First;
                                                    if (test2 != null)
                                                    {
                                                        Console.Write(test2);
                                                        t = test2.Root.Last.First.ToString();
                                                    }
                                                }
                                            }
                                            var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                            json = JsonConvert.SerializeObject(json_1);
                                            json = json.Replace("[", "").Replace("]", "");
                                            json = json.Replace("null", "");
                                            API_Response_Insert(APIMst_Id, "", json, ".json");
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }



                                        //WebClient client = new WebClient();
                                        //client.Headers.Add("Content-type", "application/json");
                                        //client.Encoding = Encoding.UTF8;
                                        //json = client.UploadString("http://pdhk.diamx.net/API/StockSearch?APIToken=3c0db41e-7b79-48c4-8cbd-1f718db7263a", "POST", "");

                                        //JObject o = JObject.Parse(json);
                                        //var t = string.Empty;
                                        //if (o != null)
                                        //{
                                        //    var test = o.First;
                                        //    if (test != null)
                                        //    {
                                        //        var test2 = test.First;
                                        //        if (test2 != null)
                                        //        {
                                        //            Console.Write(test2);
                                        //            t = test2.Root.Last.First.ToString();
                                        //        }
                                        //    }
                                        //}
                                        //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        //json = JsonConvert.SerializeObject(json_1);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://STOCK.DDPL.COM/DHARAMWEBAPI/API/STOCKDISPAPI/GETDIAMONDDATA")
                                    {
                                        Dharam _data = new Dharam();
                                        _data.uniqID = 23835;
                                        _data.company = "SUNRISE DIAMONDS LTD";
                                        _data.actCode = "Su@D123#4nd23";
                                        _data.selectAll = "";
                                        _data.StartIndex = 1;
                                        _data.count = 80000;
                                        _data.columns = "";
                                        _data.finder = "";
                                        _data.sort = "";

                                        string inputJson = (new JavaScriptSerializer()).Serialize(_data);

                                        WebRequest request = WebRequest.Create("https://stock.ddpl.com/DharamWebApi/api/stockdispapi/getDiamondData");
                                        request.Method = "POST";
                                        request.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray = Encoding.UTF8.GetBytes(inputJson);
                                        request.ContentType = "application/json";
                                        //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                        request.ContentLength = byteArray.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream = request.GetRequestStream();
                                        dataStream.Write(byteArray, 0, byteArray.Length);
                                        dataStream.Close();
                                        try
                                        {
                                            //and here is the response.
                                            WebResponse response = request.GetResponse();

                                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                            dataStream = response.GetResponseStream();
                                            StreamReader reader = new StreamReader(dataStream);
                                            json = reader.ReadToEnd();
                                            Console.WriteLine(json);
                                            reader.Close();
                                            dataStream.Close();
                                            response.Close();
                                            request.Abort();

                                            API_Response_Insert(APIMst_Id, json, "", ".json");
                                            JObject o = JObject.Parse(json);
                                            var t = string.Empty;
                                            if (o != null)
                                            {
                                                var test = o.First;
                                                if (test != null)
                                                {
                                                    var test2 = test.First;
                                                    if (test2 != null)
                                                    {
                                                        Console.Write(test2);
                                                        t = test2.Root.Last.First.ToString();
                                                    }
                                                }
                                            }
                                            var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                            json = JsonConvert.SerializeObject(json_1);
                                            json = json.Replace("[", "").Replace("]", "");
                                            json = json.Replace("null", "");

                                            API_Response_Insert(APIMst_Id, "", json, ".json");
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }


                                        //Dharam _data = new Dharam();
                                        //_data.uniqID = 23835;
                                        //_data.company = "SUNRISE DIAMONDS LTD";
                                        //_data.actCode = "Su@D123#4nd23";
                                        //_data.selectAll = "";
                                        //_data.StartIndex = 1;
                                        //_data.count = 80000;
                                        //_data.columns = "";
                                        //_data.finder = "";
                                        //_data.sort = "";

                                        //string inputJson = (new JavaScriptSerializer()).Serialize(_data);

                                        //WebClient client = new WebClient();
                                        //client.Headers.Add("Content-type", "application/json");
                                        //client.Encoding = Encoding.UTF8;

                                        //json = client.UploadString("https://stock.ddpl.com/DharamWebApi/api/stockdispapi/getDiamondData", "POST", inputJson);

                                        //JObject o = JObject.Parse(json);
                                        //var t = string.Empty;
                                        //if (o != null)
                                        //{
                                        //    var test = o.First;
                                        //    if (test != null)
                                        //    {
                                        //        var test2 = test.First;
                                        //        if (test2 != null)
                                        //        {
                                        //            Console.Write(test2);
                                        //            t = test2.Root.Last.First.ToString();
                                        //        }
                                        //    }
                                        //}
                                        //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        //json = JsonConvert.SerializeObject(json_1);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://KRINALGEMS.DIAMX.NET/API/STOCKSEARCH")
                                    {
                                        WebRequest request = WebRequest.Create("http://krinalgems.diamx.net/API/StockSearch?APIToken=e161dd39-44ed-4b67-8a48-8406da883892");
                                        request.Method = "POST";
                                        request.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray = Encoding.UTF8.GetBytes("");
                                        request.ContentType = "application/json";
                                        //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                        request.ContentLength = byteArray.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream = request.GetRequestStream();
                                        dataStream.Write(byteArray, 0, byteArray.Length);
                                        dataStream.Close();
                                        try
                                        {
                                            //and here is the response.
                                            WebResponse response = request.GetResponse();

                                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                            dataStream = response.GetResponseStream();
                                            StreamReader reader = new StreamReader(dataStream);
                                            json = reader.ReadToEnd();
                                            Console.WriteLine(json);
                                            reader.Close();
                                            dataStream.Close();
                                            response.Close();
                                            request.Abort();

                                            API_Response_Insert(APIMst_Id, json, "", ".json");
                                            JObject o = JObject.Parse(json);
                                            var t = string.Empty;
                                            if (o != null)
                                            {
                                                var test = o.First;
                                                if (test != null)
                                                {
                                                    var test2 = test.Next.First.ToString();
                                                    t = test2;
                                                }
                                            }
                                            var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                            json = JsonConvert.SerializeObject(json_1);
                                            json = json.Replace("[", "").Replace("]", "");
                                            json = json.Replace("null", "");

                                            API_Response_Insert(APIMst_Id, "", json, ".json");
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }


                                        //WebClient client = new WebClient();
                                        //client.Headers.Add("Content-type", "application/json");
                                        //client.Encoding = Encoding.UTF8;
                                        //json = client.UploadString("http://krinalgems.diamx.net/API/StockSearch?APIToken=e161dd39-44ed-4b67-8a48-8406da883892", "POST", "");

                                        //JObject o = JObject.Parse(json);
                                        //var t = string.Empty;
                                        //if (o != null)
                                        //{
                                        //    var test = o.First;
                                        //    if (test != null)
                                        //    {
                                        //        var test2 = test.Next.First.ToString();
                                        //        t = test2;
                                        //    }
                                        //}
                                        //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        //json = JsonConvert.SerializeObject(json_1);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://VAIBHAVGEMS.CO/PROVIDESTOCK.SVC/GETSTOCK")
                                    {
                                        WebRequest request = WebRequest.Create(_API);
                                        request.Method = "POST";
                                        request.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray = Encoding.UTF8.GetBytes(InputPara);
                                        request.ContentType = "application/x-www-form-urlencoded";
                                        //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                        request.ContentLength = byteArray.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream = request.GetRequestStream();
                                        dataStream.Write(byteArray, 0, byteArray.Length);
                                        dataStream.Close();
                                        try
                                        {
                                            //and here is the response.
                                            WebResponse response = request.GetResponse();

                                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                            dataStream = response.GetResponseStream();
                                            StreamReader reader = new StreamReader(dataStream);
                                            json = reader.ReadToEnd();
                                            Console.WriteLine(json);
                                            reader.Close();
                                            dataStream.Close();
                                            response.Close();
                                            request.Abort();

                                            //if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://VAIBHAVGEMS.CO/PROVIDESTOCK.SVC/GETSTOCK")
                                            //{
                                            API_Response_Insert(APIMst_Id, json, "", ".json");
                                            JObject o = JObject.Parse(json);
                                            var t = string.Empty;
                                            if (o != null)
                                            {
                                                var test = o.First;
                                                if (test != null)
                                                {
                                                    var test2 = test.First;
                                                    if (test2 != null)
                                                    {
                                                        Console.Write(test2);
                                                        t = test2.First.First.ToString();
                                                    }
                                                }
                                            }
                                            var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                            json = JsonConvert.SerializeObject(json_1);
                                            json = json.Replace("[", "").Replace("]", "");
                                            json = json.Replace("null", "");
                                            API_Response_Insert(APIMst_Id, "", json, ".json");
                                            //}
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }



                                        //WebClient client = new WebClient();
                                        ////client.Headers.Add("Authorization", "Bearer " + Token);
                                        //client.Headers.Add("Content-type", "application/json");
                                        //client.Encoding = Encoding.UTF8;
                                        //json = client.UploadString(_API, "POST", InputPara);

                                        //if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://VAIBHAVGEMS.CO/PROVIDESTOCK.SVC/GETSTOCK")
                                        //{
                                        //    JObject o = JObject.Parse(json);
                                        //    var t = string.Empty;
                                        //    if (o != null)
                                        //    {
                                        //        var test = o.First;
                                        //        if (test != null)
                                        //        {
                                        //            var test2 = test.First;
                                        //            if (test2 != null)
                                        //            {
                                        //                Console.Write(test2);
                                        //                t = test2.First.First.ToString();
                                        //            }
                                        //        }
                                        //    }
                                        //    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        //    json = JsonConvert.SerializeObject(json_1);
                                        //}
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://JODHANI.IN/WEB_SERVICES/JODHANI.ASMX?OP=STOCK_API")
                                    {
                                        string xml = "<?xml version='1.0' encoding='utf-8'?>" +
                                        "<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>" +
                                          "<soap:Body>" +
                                            "<Stock_API xmlns='http://tempuri.org/'>" +
                                              "<user_name>sunrise</user_name>" +
                                              "<password>sun@123</password>" +
                                            "</Stock_API>" +
                                          "</soap:Body>" +
                                        "</soap:Envelope>";

                                        WebRequest request = WebRequest.Create("http://jodhani.in/web_services/jodhani.asmx?op=Stock_API");
                                        request.Method = "POST";
                                        request.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray = Encoding.UTF8.GetBytes(xml);
                                        request.ContentType = "text/xml;";
                                        //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                        request.ContentLength = byteArray.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream = request.GetRequestStream();
                                        dataStream.Write(byteArray, 0, byteArray.Length);
                                        dataStream.Close();
                                        try
                                        {
                                            //and here is the response.
                                            WebResponse response = request.GetResponse();

                                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                            dataStream = response.GetResponseStream();
                                            StreamReader reader = new StreamReader(dataStream);
                                            json = reader.ReadToEnd();
                                            Console.WriteLine(json);
                                            reader.Close();
                                            dataStream.Close();
                                            response.Close();
                                            request.Abort();

                                            API_Response_Insert(APIMst_Id, json, "", ".json");

                                            string xml1 = "<?xml version='1.0' encoding='utf-8'?><soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><soap:Body><Stock_APIResponse xmlns='http://tempuri.org/' /></soap:Body></soap:Envelope>";
                                            xml1 = xml1.Replace("'", "\"");
                                            json = json.Replace(xml1, "");

                                            JArray arrayList = JArray.Parse(json);

                                            var t1 = arrayList.Last.Last.First.First.Parent.First.Parent.ToString();

                                            var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t1);
                                            json = JsonConvert.SerializeObject(json_1);
                                            json = json.Replace("[", "").Replace("]", "");
                                            json = json.Replace("null", "");

                                            API_Response_Insert(APIMst_Id, "", json, ".json");
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }


                                        //WebClient client = new WebClient();
                                        //client.Headers.Add("Content-type", "text/xml;");
                                        //client.Encoding = Encoding.UTF8;
                                        //string xml = "<?xml version='1.0' encoding='utf-8'?>"+
                                        //"<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>"+
                                        //  "<soap:Body>" +
                                        //    "<Stock_API xmlns='http://tempuri.org/'>" +
                                        //      "<user_name>sunrise</user_name>" +
                                        //      "<password>sun@123</password>" +
                                        //    "</Stock_API>" +
                                        //  "</soap:Body>" +
                                        //"</soap:Envelope>";
                                        //json = client.UploadString("http://jodhani.in/web_services/jodhani.asmx?op=Stock_API", "POST", xml);

                                        //string xml1 = "<?xml version='1.0' encoding='utf-8'?><soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><soap:Body><Stock_APIResponse xmlns='http://tempuri.org/' /></soap:Body></soap:Envelope>";
                                        //xml1 = xml1.Replace("'", "\"");
                                        //json = json.Replace(xml1, "");

                                        //JArray arrayList = JArray.Parse(json);

                                        //var t1 = arrayList.Last.Last.First.First.Parent.First.Parent.ToString();

                                        ////JObject o = JObject.Parse(json);
                                        ////var t = string.Empty;
                                        ////if (o != null)
                                        ////{
                                        ////    var test = o.First;
                                        ////    if (test != null)
                                        ////    {
                                        ////        var test2 = test.Next.First.ToString();
                                        ////        t = test2;
                                        ////    }
                                        ////}
                                        //var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t1);
                                        //json = JsonConvert.SerializeObject(json_1);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://API.EVENUSJEWEL.COM/API/GETSTOCK")
                                    {
                                        VenusJewel_Login_Req lgn_req = new VenusJewel_Login_Req();
                                        lgn_req.User_Name = "sunriseapi";
                                        lgn_req.Password = "sunriseapi290220";

                                        string inputJson = (new JavaScriptSerializer()).Serialize(lgn_req);

                                        WebRequest request = WebRequest.Create("https://api.evenusjewel.com/api/login");
                                        request.Method = "POST";
                                        request.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray = Encoding.UTF8.GetBytes(inputJson);
                                        request.ContentType = "application/json";
                                        request.ContentLength = byteArray.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream = request.GetRequestStream();
                                        dataStream.Write(byteArray, 0, byteArray.Length);
                                        dataStream.Close();


                                        try
                                        {
                                            //and here is the response.
                                            WebResponse response = request.GetResponse();

                                            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                            dataStream = response.GetResponseStream();
                                            StreamReader reader = new StreamReader(dataStream);
                                            json = reader.ReadToEnd();
                                            Console.WriteLine(json);
                                            reader.Close();
                                            dataStream.Close();
                                            response.Close();
                                            request.Abort();

                                            VenusJewel_Login_Res lgn_res = new VenusJewel_Login_Res();
                                            lgn_res = (new JavaScriptSerializer()).Deserialize<VenusJewel_Login_Res>(json);
                                            Token = lgn_res.Token_Id;

                                            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://api.evenusjewel.com/api/GetStock");
                                            request1.Method = "GET";
                                            request1.Timeout = 7200000; //2 Hour in milliseconds
                                            request1.ContentType = "application/json";
                                            request1.Headers.Add("Authorization", Token);
                                            request1.Headers.Add("api_version", "Version = 2");
                                            try
                                            {
                                                WebResponse response1 = request1.GetResponse();
                                                using (var reader1 = new StreamReader(response1.GetResponseStream()))
                                                {
                                                    json = reader1.ReadToEnd();
                                                    API_Response_Insert(APIMst_Id, json, "", ".json");

                                                    JObject o = JObject.Parse(json);
                                                    var t = string.Empty;
                                                    if (o != null)
                                                    {
                                                        var test = o.Last;
                                                        if (test != null)
                                                        {
                                                            var test2 = test.First;
                                                            if (test2 != null)
                                                            {
                                                                t = test2.ToString();
                                                            }
                                                        }
                                                    }
                                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                                    json = JsonConvert.SerializeObject(json_1);
                                                    json = json.Replace("[", "").Replace("]", "");
                                                    json = json.Replace("null", "");
                                                    API_Response_Insert(APIMst_Id, "", json, ".json");
                                                }
                                            }
                                            catch (WebException ex)
                                            {
                                                Api_Start_End(APIMst_Id, "End");
                                                ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                Api_Start_End(APIMst_Id, "End");
                                                ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                            }
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://KBSHK.FEEDCENTER.NET:7788/TOKEN")
                                    {
                                        var input = new KBS_LoginRequest
                                        {
                                            grant_type = "password",
                                            username = "ShairuGems",
                                            password = "skT6#4dilkeu&@"

                                        };
                                        string InputPara1 = string.Join("&", input.GetType()
                                                                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                                                    .Where(p => p.GetValue(input, null) != null)
                                                           .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(input).ToString())}"));

                                        WebClient client = new WebClient();
                                        client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                        client.Encoding = Encoding.UTF8;
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                        json = client.UploadString("http://kbshk.feedcenter.net:7788/token", InputPara1);

                                        KBS_LoginResponse PGLoginRes = new KBS_LoginResponse();
                                        PGLoginRes = (new JavaScriptSerializer()).Deserialize<KBS_LoginResponse>(json);
                                        Token = PGLoginRes.access_token;

                                        WebClient client1 = new WebClient();
                                        client1.Headers["Authorization"] = "Bearer " + Token;
                                        client1.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                        client1.Encoding = Encoding.UTF8;
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                        json = client1.DownloadString("http://kbshk.feedcenter.net:7788/api/GetStonesBySIteID/1022");

                                        API_Response_Insert(APIMst_Id, json, "", ".json");

                                        json = json.Replace("[", "").Replace("]", "");
                                        API_Response_Insert(APIMst_Id, "", json, ".json");
                                    }
                                    //        else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "GIA")
                                    //        {
                                    //            string fpath = @"D:\Project\Rajeshri_Projects\Oracel_ Data_Update_GIA_Project\";
                                    //            if (!Directory.Exists(fpath))
                                    //            {
                                    //                Directory.CreateDirectory(fpath);
                                    //            }
                                    //            string FileName = fpath + "2458560511.png";
                                    //            if (File.Exists(FileName))
                                    //            {
                                    //                File.Delete(FileName);
                                    //            }
                                    //            //MessageBox.Show(FileName);
                                    //            WebClient client1 = new WebClient();
                                    //            //client1.Credentials = new NetworkCredential("administrator", "admin#Sun$1041");
                                    //            client1.DownloadFile("https://report-check-objects-ap-southeast-1-092721654985.s3.amazonaws.com/digital-cards/2458560511.png?AWSAccessKeyId=ASIARLFVEFTE7YNZRS4J&Signature=uDA8w5JV2afDWECv0o7Be01Fo%2Fg%3D&x-amz-security-token=IQoJb3JpZ2luX2VjEAwaDmFwLXNvdXRoZWFzdC0xIkcwRQIgK7U97L0%2BHQC0c73uHkie0EZxbYOYhphKd1%2FK0HXRI%2BMCIQCB0DT0L7YgokVtVqYoCt2ZyM%2Fllpvxq0ARMAdbVFYMqirGAwh1EAMaDDA5MjcyMTY1NDk4NSIMJ4zcGu4MfhbZQXKXKqMDw8u2v1e57ignxtqvKG3xXcOE40XHdWezaILNLZSH9578PCiZ%2BtNF01lAP8pkv4Kc5yw6yAWYymL4F9cmQjNsVNrO39foMPxtbEZr1oWbx%2FcSNobHduJDcVhaqAipHVWv%2BneSQKrE%2FIBWCd6Qdf2bMU98IE4CAcMyX%2FkVWykMtYs8lMUZv97bbt4a%2BeT9nUaL9KDeD1vO0bHBtZ6xBQY6oS0o6KlkGmaUiv12MXW4fx3GDifGj2W6aNq0LAbxKLfFZpjHNDeCzl2RzBk27PjGPaSVldvq3bjP2jeBdbJrXR9PJHmwxmwfm5501PDyLun409RTKQWe7YjNGEo8687gyMMgpaDoy%2BuxQSOcNH8Qsg7xyYWft3El6BWW4Zh2QWb2RW2YaCerKe%2BsTSiB16YY%2B0EohGSM0I7xOC8Dd1esxKeYNkH2VZvJUCpI4JHhLT%2FuNqQN16FilAdJ406y9B8AZ%2FjXVGjtsIoGkEKiqeCzJ8AVTfgEwCvv5azS3ZiTyNWZoMgF5eoOIxBbbuG4Xl5OLquGDu2ao%2F7%2BXab3IigikHym6Ssw%2F9WkngY6ngHJLXUb6AZIzoI0nJ2VGWQlCvpJDu8nJrXDqZI5ATmf8M73w%2FRyxlgSrOu1%2BWHQPTHUy49PT6A9oDD3FSkpcSE2XUdea7Ybh69nJLwUy59OW5hlMDjpYLN9yjxDGb1T2JDf8cHp%2FfIiAUybtKuNWKqBS99cF3j90f1Eiy0XVYMHV2Gf%2BNZtpCUV4JP8xjSODJReQYpgVN9m%2Bse5l6QbZw%3D%3D&Expires=1674133202", FileName);
                                    //            FileInfo PDFFile = new FileInfo(FileName);
                                    //            //MessageBox.Show("Certi Create Successfully");


                                    //            var query = @"
                                    // query ReportQuery($ReportNumber: String!) {
                                    //    getReport(report_number: $ReportNumber){
                                    //        report_number
                                    //        report_date
                                    //        results {
                                    //            __typename
                                    //            ... on DiamondGradingReportResults {
                                    //                 measurements
                                    //                 carat_weight
                                    //                 color_grade
                                    //                 color_origin
                                    //                 color_distribution
                                    //                 clarity_grade
                                    //                 cut_grade
                                    //                 polish
                                    //                 symmetry
                                    //                 fluorescence
                                    //                 clarity_characteristics
                                    //                 key_to_symbols
                                    //                 {
                                    //                    characteristic
                                    //                 }
                                    //                 inscriptions
                                    //                 report_comments
                                    //                 proportions {
                                    //                          depth_pct
                                    //                          table_pct
                                    //                          crown_angle
                                    //                          crown_height
                                    //                          pavilion_angle
                                    //                          pavilion_depth
                                    //                          star_length
                                    //                          lower_half
                                    //                          girdle
                                    //                          culet
                                    //                        }
                                    //                  data {
                                    //                        shape {
                                    //                            shape_category
                                    //                            shape_code
                                    //                            shape_group
                                    //                            shape_group_code
                                    //                            }
                                    //                            weight {
                                    //                                weight
                                    //                                weight_unit
                                    //                            }
                                    //                            color {
                                    //                                color_grade_code
                                    //                                color_modifier
                                    //                            }
                                    //                            clarity
                                    //                            cut
                                    //                            polish
                                    //                            symmetry
                                    //                            fluorescence {
                                    //                                fluorescence_intensity
                                    //                                fluorescence_color
                                    //                            }
                                    //                            girdle {
                                    //                                girdle_condition
                                    //                                girdle_condition_code
                                    //                                girdle_pct
                                    //                                girdle_size
                                    //                                girdle_size_code
                                    //                            }
                                    //                            culet {
                                    //                                culet_code
                                    //                            }
                                    //                        }
                                    //                    }
                                    //                }
                                    //                links {
                                    //                    pdf
                                    //                    proportions_diagram
                                    //                    plotting_diagram
                                    //                }
                                    //        quota {
                                    //            remaining
                                    //        }
                                    //    }
                                    //}
                                    //";

                                    //            var query_variables = new Dictionary<string, string>
                                    //            {
                                    //                { "ReportNumber", "2458560511"}
                                    //            };

                                    //            var body = new Dictionary<string, object>
                                    //            {
                                    //                { "query", query },
                                    //                { "variables", query_variables }
                                    //            };

                                    //            String json1 = (new JavaScriptSerializer()).Serialize(body);

                                    //            var client = new WebClient();

                                    //            string url = "https://api.reportresults.gia.edu";

                                    //            client.Headers.Add(HttpRequestHeader.Authorization, "80c51196-5610-4ccf-87fa-e26477126ec9");
                                    //            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                                    //            var response = client.UploadString(url, json1);

                                    //            Root obj = JsonConvert.DeserializeObject<Root>(response);


                                    //        }

                                    //else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "BHAVYAM")
                                    //{
                                    //    var input = new BHAVYAM_LoginRequest
                                    //    {
                                    //        UserName = "bhavyam_d",
                                    //        Password = "bhavyam_d",
                                    //        grant_type = "password",
                                    //        DeviceType = "Web",
                                    //        IpAddress = "123.253.12.214"

                                    //    };
                                    //    string InputPara1 = string.Join("&", input.GetType()
                                    //                                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                    //                                                .Where(p => p.GetValue(input, null) != null)
                                    //                       .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(input).ToString())}"));

                                    //    WebClient client = new WebClient();
                                    //    //client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                    //    client.Encoding = Encoding.UTF8;
                                    //    ServicePointManager.Expect100Continue = false;
                                    //    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                    //    json = client.UploadString("http://45.35.190.142:92/api/User/Login", InputPara1);
                                    //}


                                    ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                    dt_APIRes = jDt.JsonStringToDataTable(json);

                                }
                                else
                                {
                                    _API = dtAPI.Rows[i]["APIURL"].ToString();
                                    //string[] words = _API.Split('?');
                                    //String InputPara = string.Empty;
                                    //if (words.Length == 2)
                                    //{
                                    //    InputPara = words[1].ToString();
                                    //}

                                    //WebClient client = new WebClient();
                                    //client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    //ServicePointManager.Expect100Continue = false;
                                    //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    string json = "";
                                    //string json = client.DownloadString(_API);
                                    //client.Dispose();

                                    if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://PCK.SNJDIAM.COM/SHARESTOCK/API?LOGINNAME=SUN&PASSWORD=SNJ123")
                                    {
                                        try
                                        {
                                            WebClient client = new WebClient();
                                            client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                            ServicePointManager.Expect100Continue = false;
                                            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                            json = client.DownloadString("https://pck.snjdiam.com/ShareStock/Api?loginname=sun&password=snj123");

                                            API_Response_Insert(APIMst_Id, json, "", ".json");
                                            client.Dispose(); 


                                            JObject o = JObject.Parse(json);
                                            var t = string.Empty;
                                            if (o != null)
                                            {
                                                var test = o.Last.Last;
                                                if (test != null)
                                                {
                                                    var test2 = test.ToString();
                                                    t = test2;
                                                    //var test2 = test.First;
                                                    //if (test2 != null)
                                                    //{
                                                    //    Console.Write(test2);
                                                    //    t = test2.Root.Last.First.First.First.ToString();
                                                    //}
                                                }
                                            }
                                            var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                            json = JsonConvert.SerializeObject(json_1);
                                            json = json.Replace("[", "").Replace("]", "");
                                            json = json.Replace("null", "");

                                            API_Response_Insert(APIMst_Id, "", json, ".json");

                                            ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://WWW.STARLIGHTDIAMONDS.IN/API/GETSTOCK")
                                    {
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        json = client.DownloadString("http://www.starlightdiamonds.in/api/getstock?user=sFAnZcJtofnlrU/URZaYnj3R8yeB8nUOxp6LlFMC3X0=&key=EEQMOjwlGGmJSk4P9aRmgmfO6fuhJUU+NPC3UAjYaaI=&type=json");
                                        client.Dispose();

                                        API_Response_Insert(APIMst_Id, json, "", ".json");

                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");
                                        string str = "";

                                        json = json.Replace("<!DOCTYPE html>", "");

                                        str = "<html lang='en'>";
                                        str = str.Replace("'", "\"");
                                        json = json.Replace(str, "");

                                        json = json.Replace("<head><title>", "");
                                        json = json.Replace("</title></head>", "");
                                        json = json.Replace("<body>", "");
                                        json = json.Replace("<body>", "");
                                        json = json.Replace("<body>", "");

                                        str = "<form method='post' action='./getstock?user=sFAnZcJtofnlrU%2fURZaYnj3R8yeB8nUOxp6LlFMC3X0%3d&amp;key=EEQMOjwlGGmJSk4P9aRmgmfO6fuhJUU+NPC3UAjYaaI%3d&amp;type=json' id='form1'>";
                                        str = str.Replace("'", "\"");
                                        json = json.Replace(str, "");

                                        str = "<div class='aspNetHidden'>";
                                        str = str.Replace("'", "\"");
                                        json = json.Replace(str, "");

                                        str = "<input type='hidden' name='__VIEWSTATE' id='__VIEWSTATE' value='/wEPDwULLTE2MTY2ODcyMjlkZLlQNdj1yLtIK3dSkT5LRNbAx4SUWXDdXZ/TLBZck00E' />";
                                        str = str.Replace("'", "\"");
                                        json = json.Replace(str, "");

                                        json = json.Replace("</div>", "");

                                        str = "<div class='aspNetHidden'>";
                                        str = str.Replace("'", "\"");
                                        json = json.Replace(str, "");

                                        str = "<input type='hidden' name='__VIEWSTATEGENERATOR' id='__VIEWSTATEGENERATOR' value='ED942F36' />";
                                        str = str.Replace("'", "\"");
                                        json = json.Replace(str, "");

                                        json = json.Replace("</div>", "");
                                        json = json.Replace("<div>", "");
                                        json = json.Replace("</div>", "");
                                        json = json.Replace("</form>", "");
                                        json = json.Replace("</body>", "");
                                        json = json.Replace("</html>", "");

                                        API_Response_Insert(APIMst_Id, "", json, ".json");
                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://API.FINESTARDIAMONDS.COM/API/V1/DIAMOND/PAGINATE")
                                    {
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        json = client.DownloadString("https://api.finestardiamonds.com/api/v1/diamond/paginate?username=list@sunrisediam.com&password=Sunrise1041");
                                        client.Dispose();
                                        API_Response_Insert(APIMst_Id, json, "", ".json");

                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");
                                        API_Response_Insert(APIMst_Id, "", json, ".json");

                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://PB.PROLANCEIT.IN/API/USER/TOKEN")
                                    {
                                        WebClient client1 = new WebClient();
                                        //client1.Headers.Add("apiKey", "dhfasgdfksdiw232343fsdfchsdkhf==");   //testing key
                                        client1.Headers.Add("apiKey", "Sunadiw4thvihth32hf554u23LtdGerrjschcfnr==");   //live key
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.DownloadString("http://PB.prolanceit.in/api/user/token");
                                        client1.Dispose();

                                        JObject o = JObject.Parse(json);
                                        var token = string.Empty;

                                        if (o != null)
                                        {
                                            token = ((Newtonsoft.Json.Linq.JValue)o.Last.Last).Value.ToString();
                                        }
                                        if (!string.IsNullOrEmpty(token))
                                        {
                                            json = "";
                                            WebClient client2 = new WebClient();
                                            client2.Headers.Add("token", token);
                                            client2.Encoding = Encoding.UTF8;
                                            json = client2.DownloadString("http://PB.prolanceit.in/api/stones");
                                            API_Response_Insert(APIMst_Id, json, "", ".json");
                                            client2.Dispose();

                                            json = json.Replace("[", "").Replace("]", "");
                                            json = json.Replace("null", "");
                                            API_Response_Insert(APIMst_Id, "", json, ".json");
                                            ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://APICHN.NAROLA.IN/ADMIN/STOCKSHARE/STOCKSHAREAPIRESULT?USERNAME=SUNRISEDIAMONDS&ACCESS_KEY=IXL8-1KGS-SA3C-E6HW-BRBA-IW4G-DSTU")
                                    {
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        json = client.DownloadString(_API);
                                        API_Response_Insert(APIMst_Id, json, "", ".json");
                                        client.Dispose();

                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            t = o.Last.Last.First.Parent.ToString();
                                        }

                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);

                                        json = json.Substring(1, json.Length - 2);

                                        API_Response_Insert(APIMst_Id, "", json, ".json");
                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://WWW.DIAMJOY.COM/API/USER/STOCK/11229/729F7B484FA22A5276B0CDADABC75147/?LANG=EN")
                                    {
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        json = client.DownloadString(_API);
                                        API_Response_Insert(APIMst_Id, json, "", ".json");
                                        client.Dispose();


                                        JOY _data = (new JavaScriptSerializer()).Deserialize<JOY>(json);
                                        ConvertJsonObjectToDataTable jodt = new ConvertJsonObjectToDataTable();
                                        dt_APIRes = jodt.StringArrayToDataTable(_data.keys, _data.rows);

                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://FRONTOFFICEAPI.DIAMANTO.CO/API/CHANNELPARTNER/GETINVENTORY/SUNRISE/SUNRISE@1401")
                                    {
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        json = client.DownloadString(_API);
                                        client.Dispose();

                                        JToken objectData = JToken.Parse(json);
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(objectData.ToString());
                                        json = JsonConvert.SerializeObject(json_1);
                                        API_Response_Insert(APIMst_Id, json, "", ".json");
                                        json = json.Replace("[", "").Replace("]", "");
                                        API_Response_Insert(APIMst_Id, "", json, ".json");

                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://SJWORLDAPI.AZUREWEBSITES.NET/SHARE/SJAPI.ASMX/GETDATA?LOGINNAME=SUNRISE&PASSWORD=SUNRISE321")
                                    {
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        json = client.DownloadString(_API);
                                        client.Dispose();

                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o.First;
                                            if (test != null)
                                            {
                                                var test2 = test.First;
                                                if (test2 != null)
                                                {
                                                    Console.Write(test2);
                                                    t = o.Last.Last.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);
                                        API_Response_Insert(APIMst_Id, json, "", ".json");
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        API_Response_Insert(APIMst_Id, "", json, ".json");
                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://API.RRAJESH.CO/API/V1/DIAMOND/PAGINATE?USERNAME=SUNRISE&PASSWORD=SUN@321&PAGE=1&LIMIT=99999")
                                    {
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        json = client.DownloadString(_API);
                                        client.Dispose();

                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o.First;
                                            if (test != null)
                                            {
                                                var test2 = test.Next.First.ToString();
                                                t = test2;
                                                //var test2 = test.First;
                                                //if (test2 != null)
                                                //{
                                                //    Console.Write(test2);
                                                //    t = test2.Root.Last.First.First.First.ToString();
                                                //}
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);
                                        API_Response_Insert(APIMst_Id, json, "", ".json");
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");
                                        API_Response_Insert(APIMst_Id, "", json, ".json");

                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTP://ANGELSTAR.HK/ANGELSTAR-STOCK")
                                    {
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        json = client.DownloadString(_API);
                                        client.Dispose();
                                        API_Response_Insert(APIMst_Id, json, "", ".json");
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        API_Response_Insert(APIMst_Id, "", json, ".json");
                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        json = client.DownloadString(_API);
                                        client.Dispose();

                                        API_Response_Insert(APIMst_Id, json, "", ".json");

                                        ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }

                                }

                            }
                            else if (dtAPI.Rows[i]["APIResponseFormat"].ToString().ToUpper() == "HTML")
                            {
                                if (dtAPI.Rows[i]["APIMethod"].ToString().ToUpper() == "GET")
                                {
                                    if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://WWW.1314PG.COM/API/USER/STOCK/11738/8789AE77D94A9CFB109C1BA5143ABAB6/")
                                    {
                                        _API = dtAPI.Rows[i]["APIURL"].ToString();
                                        WebClient client = new WebClient();
                                        client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                        string response = client.DownloadString(_API);
                                        API_Response_Insert(APIMst_Id, response, "", ".html");
                                        client.Dispose();

                                        string[] res = response.Split('\n');

                                        string[] columns = res.Where(w => w == res[0]).ToArray();

                                        string[] rows = res.Where(w => w != res[0]).ToArray();

                                        ConvertStringArrayToDatatable saDt = new ConvertStringArrayToDatatable();

                                        dt_APIRes = saDt.StringArrayToDataTable(columns, rows);
                                    }
                                }
                            }
                            else if (dtAPI.Rows[i]["APIResponseFormat"].ToString().ToUpper() == "TEXT")
                            {
                                if (dtAPI.Rows[i]["APIMethod"].ToString().ToUpper() == "POST")
                                {
                                    string json = "";
                                    if (dtAPI.Rows[i]["APIURL"].ToString().ToUpper() == "HTTPS://SS.SRK.BEST/V1/STOCKSHARING/SERVICES")
                                    {
                                        //WebClient client = new WebClient();
                                        //client.Headers["X-ACCESS-KEY"] = "627d44bd-c286-49cc-ab95-ce83fdb12934";
                                        //client.Headers["Content-Type"] = "text/plain";
                                        //client.Encoding = Encoding.UTF8;
                                        //ServicePointManager.Expect100Continue = false;
                                        //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                        //json = client.UploadString("https://ss.srk.best/v1/stockSharing/services/0e4d83a5-b4b0-46e3-859e-5e09e2f3b343", string.Empty);
                                        //client.Dispose();

                                        //WebClient client1 = new WebClient();
                                        //client1.Headers.Add("X-ACCESS-KEY", "627d44bd-c286-49cc-ab95-ce83fdb12934");
                                        //client1.Headers.Add("Content-Type", "text/plain");
                                        //client1.Encoding = Encoding.UTF8;
                                        //json = client1.UploadString("https://ss.srk.best/v1/stockSharing/services/0e4d83a5-b4b0-46e3-859e-5e09e2f3b343", "POST", "");
                                        //client1.Dispose();

                                        //WebClient client = new WebClient();
                                        //client.Headers.Add("Content-type", "text/plain");
                                        //client.Headers.Add("X-ACCESS-KEY", "627d44bd-c286-49cc-ab95-ce83fdb12934");
                                        //client.Encoding = Encoding.UTF8;
                                        //json = client.UploadString("https://ss.srk.best/v1/stockSharing/services/0e4d83a5-b4b0-46e3-859e-5e09e2f3b343", "POST", string.Empty);
                                        //client.Dispose();

                                        HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://ss.srk.best/v1/stockSharing/services/0e4d83a5-b4b0-46e3-859e-5e09e2f3b343");
                                        request1.Method = "POST";
                                        request1.Timeout = 7200000; //2 Hour in milliseconds
                                        request1.ContentType = "text/plain";
                                        request1.Headers.Add("X-ACCESS-KEY", "627d44bd-c286-49cc-ab95-ce83fdb12934");
                                        //request1.Headers.Add("Content-type", "text/plain");
                                        try
                                        {
                                            WebResponse response1 = request1.GetResponse();
                                            using (var reader1 = new StreamReader(response1.GetResponseStream()))
                                            {
                                                json = reader1.ReadToEnd();
                                            }
                                        }
                                        catch (WebException ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Api_Start_End(APIMst_Id, "End");
                                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                                        }

                                        //string filePath = @"D:\Project\Hardik_Files\----------------------------------WORK---------\31-07-2023\response.txt";

                                        //// Read data from the text file
                                        //string fileData = ReadTextFromFile(filePath);


                                        if (!string.IsNullOrEmpty(json))
                                        {
                                            API_Response_Insert(APIMst_Id, json, "", ".txt");
                                            ConvertCsvToDataTable saDt = new ConvertCsvToDataTable();
                                            dt_APIRes = saDt.CsvToDataTable(json);
                                        }
                                    }
                                }
                            }

                            if (dt_APIRes.Rows.Count > 0 && dt_APIRes != null)
                            {
                                dt_APIRes_COUNT = dt_APIRes.Rows.Count;

                                string _path = ConfigurationManager.AppSettings["data"];
                                string _tempPath = HostingEnvironment.MapPath("~/Temp/API/");
                                if (!Directory.Exists(_tempPath))
                                {
                                    Directory.CreateDirectory(_tempPath);
                                }

                                if (dtAPI.Rows[i]["LocationExportType"].ToString().ToUpper() == "XML")
                                {
                                    filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".xml";
                                    filefullpath = _tempPath + filename;
                                    APIFileName = APIFileName + ".xml";

                                    if (File.Exists(filefullpath))
                                    {
                                        File.Delete(filefullpath);
                                    }

                                    dt_APIRes.TableName = "Records";
                                    dt_APIRes.WriteXml(filefullpath);
                                }
                                else if (dtAPI.Rows[i]["LocationExportType"].ToString().ToUpper() == "CSV")
                                {
                                    filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".csv";
                                    filefullpath = _tempPath + filename;
                                    APIFileName = APIFileName + ".csv";

                                    if (File.Exists(filefullpath))
                                    {
                                        File.Delete(filefullpath);
                                    }

                                    StringBuilder sb = new StringBuilder();
                                    IEnumerable<string> columnNames = dt_APIRes.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                                    sb.AppendLine(string.Join(",", columnNames));

                                    foreach (DataRow row in dt_APIRes.Rows)
                                    {
                                        IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().Replace(",", " "));
                                        sb.AppendLine(string.Join(",", fields));
                                    }
                                    File.WriteAllText(filefullpath, sb.ToString());
                                }
                                else if (dtAPI.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLSX)" || dtAPI.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLS)")
                                {
                                    if (dtAPI.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLSX)")
                                    {
                                        filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".xlsx";
                                        filefullpath = _tempPath + filename;
                                        APIFileName = APIFileName + ".xlsx";
                                    }
                                    else if (dtAPI.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLS)")
                                    {
                                        filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".xls";
                                        filefullpath = _tempPath + filename;
                                        APIFileName = APIFileName + ".xls";
                                    }

                                    if (File.Exists(filefullpath))
                                    {
                                        File.Delete(filefullpath);
                                    }

                                    FileInfo newFile = new FileInfo(filefullpath);
                                    using (ExcelPackage pck = new ExcelPackage(newFile))
                                    {
                                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add(APIFileName);
                                        pck.Workbook.Properties.Title = "API";
                                        ws.Cells["A1"].LoadFromDataTable(dt_APIRes, true);

                                        ws.View.FreezePanes(2, 1);
                                        var allCells = ws.Cells[ws.Dimension.Address];
                                        allCells.AutoFilter = true;
                                        allCells.AutoFitColumns();

                                        int rowStart = ws.Dimension.Start.Row;
                                        int rowEnd = ws.Dimension.End.Row;
                                        removingGreenTagWarning(ws, ws.Cells[1, 1, rowEnd, 100].Address);

                                        var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                                        headerCells.Style.Font.Bold = true;
                                        headerCells.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                        headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                        headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                                        pck.Save();
                                    }
                                }
                                else if (dtAPI.Rows[i]["LocationExportType"].ToString().ToUpper() == "JSON (FILE)")
                                {
                                    filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".json";
                                    filefullpath = _tempPath + filename;
                                    APIFileName = APIFileName + ".json";

                                    if (File.Exists(filefullpath))
                                    {
                                        File.Delete(filefullpath);
                                    }
                                    string json = IPadCommon.DataTableToJSONWithStringBuilder(dt_APIRes);
                                    File.WriteAllText(filefullpath, json);
                                }
                                if (File.Exists(filefullpath))
                                {
                                    FileInfo fi = new FileInfo(filefullpath);
                                    long size = fi.Length / 1024;
                                    if (size > 1)
                                    {
                                        File.Copy(filefullpath, tempPath + "\\" + APIFileName, true);

                                        File.Delete(filefullpath);

                                        ApiLog(APIMst_Id, true, "Total Record " + dt_APIRes_COUNT + " Success");
                                    }
                                    else
                                    {
                                        ApiLog(APIMst_Id, false, "Total Record " + dt_APIRes_COUNT + ", File Created " + size + " KB");
                                    }

                                }
                            }
                            Api_Start_End(APIMst_Id, "End");
                        }
                        catch (Exception ex)
                        {
                            Api_Start_End(APIMst_Id, "End");
                            ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                        }
                    }
                }

                return (new ServiceResponse<CommonResponse>
                {
                    Data = null,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                //Api_Start_End(APIMst_Id, "End");
                //ApiLog(APIMst_Id, false, ex.Message.ToString() + ' ' + ex.StackTrace.ToString());
                return (new ServiceResponse<CommonResponse>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static string ReadTextFromFile(string filePath)
        {
            string text = "";

            try
            {
                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Read all text from the file
                    text = File.ReadAllText(filePath);
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }

            return text;
        }
        public static void removingGreenTagWarning(ExcelWorksheet template1, string address)
        {
            var xdoc = template1.WorksheetXml;
            //Create the import nodes (note the plural vs singular
            var ignoredErrors = xdoc.CreateNode(System.Xml.XmlNodeType.Element, "ignoredErrors", xdoc.DocumentElement.NamespaceURI);
            var ignoredError = xdoc.CreateNode(System.Xml.XmlNodeType.Element, "ignoredError", xdoc.DocumentElement.NamespaceURI);
            ignoredErrors.AppendChild(ignoredError);

            //Attributes for the INNER node
            var sqrefAtt = xdoc.CreateAttribute("sqref");
            sqrefAtt.Value = address;// Or whatever range is needed....

            var flagAtt = xdoc.CreateAttribute("numberStoredAsText");
            flagAtt.Value = "1";

            ignoredError.Attributes.Append(sqrefAtt);
            ignoredError.Attributes.Append(flagAtt);

            //Now put the OUTER node into the worksheet xml
            xdoc.LastChild.AppendChild(ignoredErrors);
        }
        public static void Api_Start_End(Int64 APIMst_Id, string type)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("APIMst_Id", DbType.Int64, ParameterDirection.Input, APIMst_Id));
                para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, type));

                DataTable dt = db.ExecuteSP("API_Get_Start_CRUD", para.ToArray(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ApiLog(Int64 APIMst_Id, bool FileTransfer, string message)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("APIMst_Id", DbType.Int64, ParameterDirection.Input, APIMst_Id));
                para.Add(db.CreateParam("FileTransfer", DbType.Boolean, ParameterDirection.Input, FileTransfer));
                para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, message));

                DataTable dt = db.ExecuteSP("ApiLog", para.ToArray(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void API_Response_Insert(Int64 APIMst_Id, string APIResponse, string OurResponse, string extension)
        {
            try
            {
                //string _tempPath = HostingEnvironment.MapPath("~/Temp/APIResponse/");
                //if (!Directory.Exists(_tempPath))
                //{
                //    Directory.CreateDirectory(_tempPath);
                //}
                //string filePath = _tempPath + APIMst_Id + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fff") + extension;

                //if (!string.IsNullOrEmpty(APIResponse))
                //{
                //    File.WriteAllText(filePath, APIResponse);
                //}
                //else if (!string.IsNullOrEmpty(OurResponse))
                //{
                //    File.WriteAllText(filePath, OurResponse);
                //}

                //Database db = new Database();
                //List<IDbDataParameter> para = new List<IDbDataParameter>();

                //para.Add(db.CreateParam("APIMst_Id", DbType.Int64, ParameterDirection.Input, APIMst_Id));

                //if (!string.IsNullOrEmpty(APIResponse))
                //    para.Add(db.CreateParam("APIResponse", DbType.String, ParameterDirection.Input, filePath));
                //else
                //    para.Add(db.CreateParam("APIResponse", DbType.String, ParameterDirection.Input, DBNull.Value));

                //if (!string.IsNullOrEmpty(OurResponse))
                //    para.Add(db.CreateParam("OurResponse", DbType.String, ParameterDirection.Input, filePath));
                //else
                //    para.Add(db.CreateParam("OurResponse", DbType.String, ParameterDirection.Input, DBNull.Value));

                //db.ExecuteSP("API_Response_Insert", para.ToArray(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ServiceResponse<CommonResponse> SpaceCheck()
        {
            Database db = new Database();
            CommonResponse resp = new CommonResponse();
            List<IDbDataParameter> para = new List<IDbDataParameter>();

            try
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                string msg = "";
                foreach (DriveInfo dDrive in allDrives)
                {
                    decimal AvailableFreeSpace_1 = Convert.ToDecimal(dDrive.AvailableFreeSpace / 1024);
                    decimal AvailableFreeSpace_2 = Convert.ToDecimal(AvailableFreeSpace_1 / 1024);
                    decimal AvailableFreeSpace_3 = Convert.ToDecimal(AvailableFreeSpace_2 / 1024);

                    decimal TotalSize_1 = Convert.ToDecimal(dDrive.TotalSize / 1024);
                    decimal TotalSize_2 = Convert.ToDecimal(TotalSize_1 / 1024);
                    decimal TotalSize_3 = Convert.ToDecimal(TotalSize_2 / 1024);

                    msg += "\n Drive Name : " + dDrive.Name;
                    msg += "\n Free space : " + String.Format("{0:0.##}", AvailableFreeSpace_3);
                    msg += "\n Total space : " + String.Format("{0:0.##}", TotalSize_3);
                }

                para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, msg));
                DataTable dt = db.ExecuteSP("TotalSpaceCheck_Insert", para.ToArray(), false);

                return (new ServiceResponse<CommonResponse>
                {
                    Data = null,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, ex.Message.ToString() + ' ' + ex.StackTrace.ToString()));
                DataTable dt = db.ExecuteSP("TotalSpaceCheck_Insert", para.ToArray(), false);

                return (new ServiceResponse<CommonResponse>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }

        //public class KeyToSymbol
        //{
        //    public string characteristic { get; set; }
        //}
        //public class Proportions
        //{
        //    public string depth_pct { get; set; }
        //    public string table_pct { get; set; }
        //    public object crown_angle { get; set; }
        //    public object crown_height { get; set; }
        //    public object pavilion_angle { get; set; }
        //    public object pavilion_depth { get; set; }
        //    public object star_length { get; set; }
        //    public object lower_half { get; set; }
        //    public string girdle { get; set; }
        //    public string culet { get; set; }
        //}

        //public class Shape
        //{
        //    public string shape_category { get; set; }
        //    public string shape_code { get; set; }
        //    public string shape_group { get; set; }
        //    public string shape_group_code { get; set; }
        //}

        //public class Weight
        //{
        //    public string weight { get; set; }
        //    public string weight_unit { get; set; }

        //}

        //public class Color
        //{
        //    public string color_grade_code { get; set; }
        //    public string color_modifier { get; set; }
        //}

        //public class Fluorescence
        //{
        //    public string fluorescence_intensity { get; set; }
        //    public object fluorescence_color { get; set; }
        //}

        //public class Girdle
        //{
        //    public string girdle_condition { get; set; }
        //    public string girdle_condition_code { get; set; }
        //    public object girdle_pct { get; set; }
        //    public string girdle_size { get; set; }
        //    public string girdle_size_code { get; set; }
        //}

        //public class Culet
        //{
        //    public string culet_code { get; set; }
        //}

        //public class Data2
        //{
        //    public Shape shape { get; set; }
        //    public Weight weight { get; set; }
        //    public Color color { get; set; }
        //    public string clarity { get; set; }
        //    public object cut { get; set; }
        //    public string polish { get; set; }
        //    public string symmetry { get; set; }
        //    public Fluorescence fluorescence { get; set; }
        //    public Girdle girdle { get; set; }
        //    public Culet culet { get; set; }
        //}

        //public class Results
        //{
        //    public string __typename { get; set; }
        //    public string measurements { get; set; }
        //    public string carat_weight { get; set; }
        //    public string color_grade { get; set; }
        //    public string color_origin { get; set; }
        //    public string color_distribution { get; set; }
        //    public string clarity_grade { get; set; }
        //    public object cut_grade { get; set; }
        //    public string polish { get; set; }
        //    public string symmetry { get; set; }
        //    public string fluorescence { get; set; }
        //    public string clarity_characteristics { get; set; }

        //    public List<KeyToSymbol> key_to_symbols { get; set; }
        //    public string inscriptions { get; set; }
        //    public string report_comments { get; set; }
        //    public Proportions proportions { get; set; }
        //    public Data2 data { get; set; }

        //}

        //public class Quota
        //{
        //    public int remaining { get; set; }
        //}
        //public class Links
        //{
        //    public string pdf { get; set; }
        //    public string proportions_diagram { get; set; }
        //    public string plotting_diagram { get; set; }
        //}
        //public class GetReport
        //{
        //    public string report_number { get; set; }
        //    public string report_date { get; set; }
        //    public Results results { get; set; }
        //    public Links links { get; set; }
        //    public Quota quota { get; set; }

        //}

        //public class Data
        //{
        //    public GetReport getReport { get; set; }
        //}

        //public class Root
        //{
        //    public Data data { get; set; }
        //}
    }
}
