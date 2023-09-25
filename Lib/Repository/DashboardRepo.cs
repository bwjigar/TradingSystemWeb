using Lib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repository
{
    public static class DashboardRepo
    {
        public static ServiceResponse<DashboardCount_Response> GetDashboardCount()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("Dashboard_Count", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<DashboardCount_Response> list = new List<DashboardCount_Response>();
                    list = DataTableExtension.ToList<DashboardCount_Response>(dt);

                    return (new ServiceResponse<DashboardCount_Response>
                    {
                        Data = list,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<DashboardCount_Response>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<DashboardCount_Response>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static ServiceResponse<Last_10_Sevice_Bill_Response> GetLast_10_Sevice_Bill()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("Last_10_Sevice_Bill", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Last_10_Sevice_Bill_Response> list = new List<Last_10_Sevice_Bill_Response>();
                    list = DataTableExtension.ToList<Last_10_Sevice_Bill_Response>(dt);

                    return (new ServiceResponse<Last_10_Sevice_Bill_Response>
                    {
                        Data = list,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<Last_10_Sevice_Bill_Response>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<Last_10_Sevice_Bill_Response>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        public static ServiceResponse<Last_10_Party_Response> GetLast_10_Party_Response()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("Last_10_Party", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Last_10_Party_Response> list = new List<Last_10_Party_Response>();
                    list = DataTableExtension.ToList<Last_10_Party_Response>(dt);

                    return (new ServiceResponse<Last_10_Party_Response>
                    {
                        Data = list,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<Last_10_Party_Response>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<Last_10_Party_Response>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
    }
}
