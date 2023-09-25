using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWebsite.DataAccess
{
    public class DataQuery
    {
        #region "Sql Method"
        
        public static string WebConnString = "";
        public static void ExecuteQuery(string Query)
        {
            try
            {
                SqlHelper Sql = new SqlHelper(WebConnString);
                Sql.ExecuteQuary(Query);
                Sql = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static object ExecuteScalar(string Query)
        {
            try
            {
                SqlHelper Sql = new SqlHelper(WebConnString);
                return Sql.ExecuteMyScalar(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static System.Data.SqlClient.SqlDataReader GetSqlReader(string Query)
        {
            try
            {
                return (new SqlHelper(WebConnString)).ExecuteMyReader(Query);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static DataSet GetDataSet(string Query)
        {
            try
            {
                return (new SqlHelper(WebConnString)).GetDataset(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetDataTable(string Query)
        {
            try
            {
                return (new SqlHelper(WebConnString)).GetData(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

    }
}
