using Lib.Models;
using Sunrise.Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repository
{
    public static class CustomerAPIRepo
    {
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
    }
}
