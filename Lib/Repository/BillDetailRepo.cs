using Lib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repository
{
    public static class BillDetilRepo
    {
        public static ServiceResponse<BillDetailGrid_Response> GetBillDetail(BillDetailGrid_Request req)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(req.PageSize))
                    para.Add(db.CreateParam("PageSize", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.PageSize)));
                else
                    para.Add(db.CreateParam("PageSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PageNo))
                    para.Add(db.CreateParam("PageNo", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(req.PageNo)));
                else
                    para.Add(db.CreateParam("PageNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, req.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Search))
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, req.Search));
                else
                    para.Add(db.CreateParam("Search", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("SalePurchase_Select", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<BillDetailGrid_Response> list = new List<BillDetailGrid_Response>();
                    list = DataTableExtension.ToList<BillDetailGrid_Response>(dt);

                    return (new ServiceResponse<BillDetailGrid_Response>
                    {
                        Data = list,
                        Message = "Success",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<BillDetailGrid_Response>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<BillDetailGrid_Response>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
    }
}

