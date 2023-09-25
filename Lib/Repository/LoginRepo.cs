using Lib.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Lib.Repository
{
    public static class LoginRepo
    {
        public static ServiceResponse<UserLogin> LoginCheck(LoginRequest req)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, req.UserName));
                para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, req.Password));

                DataTable dt = db.ExecuteSP("Admin_Detail_Login", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<UserLogin> list = new List<UserLogin>();
                    list = DataTableExtension.ToList<UserLogin>(dt);

                    return (new ServiceResponse<UserLogin>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return (new ServiceResponse<UserLogin>
                    {
                        Data = null,
                        Message = "Unauthorized Credentials",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                return (new ServiceResponse<UserLogin>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
    }
}
