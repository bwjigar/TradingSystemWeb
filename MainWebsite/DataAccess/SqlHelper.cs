using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWebsite.DataAccess
{
    public enum QueryType
    {
        StoredProcedure = 0,
        SQL = 1
    }
    public enum SqlParaDirection
    {
        In,
        InOut,
        Out,
        Return
    }
    public enum SqlDataType
    {
        BigInt = 1,
        Double = 2,
        Boolean = 3,
        Char = 4,
        DateTime = 5,
        Float = 6,
        Int = 7,
        Money = 8,
        Varchar = 9,
        Int64 = 10,
        Image = 11,
        SmallInt = 12,
        Date = 13,
        Time = 14,
        TinyInt = 15,
        Decimal = 16,
        Bit = 17,
        NVarChar = 18,
        DataTable = 19
    }

    public class SqlHelper
    {
        static string ConString = "";
        public SqlConnection con = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        public SqlTransaction SqlTr;
        public SqlHelper(string ConnectionString)
        {
            try
            {
                ConString = ConnectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void OpenCon()
        {
            if (con.State != ConnectionState.Open)
            {
                con.ConnectionString = ConString;
                con.Open();
            }
        }
        public void CloseCon()
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
        public void BeginTransection(Boolean TransectionStarter)
        {
            if (TransectionStarter == false)
            {
                return;
            }
            OpenCon();
            SqlTr = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = SqlTr;
        }
        public void CommitTransection(Boolean TransectionStarter)
        {
            if (TransectionStarter == false)
            {
                return;
            }
            SqlTr.Commit();
            CloseCon();
        }
        public void RollBackTransection(Boolean TransectionStarter)
        {
            if (TransectionStarter == false)
            {
                return;
            }
            SqlTr.Rollback();
            CloseCon();
        }
        public void ExecuteQuary(string Strque)
        {
            try
            {
                OpenCon();
                SqlCommand cmd = new SqlCommand(Strque, con);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseCon();
            }
        }
        public DataTable GetData(string strSql)
        {
            SqlDataAdapter DA;
            try
            {
                OpenCon();
                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandTimeout = 300;
                DataTable objDT = new DataTable();
                DA = new SqlDataAdapter(cmd);
                DA.Fill(objDT);

                DA.Dispose();
                return objDT;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                CloseCon();
            }
        }
        public DataSet GetDataset(string strSql)
        {
            SqlDataAdapter DA;
            try
            {
                OpenCon();
                SqlCommand cmd = new SqlCommand(strSql, con);
                cmd.CommandTimeout = 1000;
                DataSet objDT = new DataSet();
                DA = new SqlDataAdapter();
                DA.SelectCommand = cmd;
                DA.Fill(objDT);

                DA.Dispose();
                return objDT;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                CloseCon();
            }
        }
        public DataSet FillData(string strSql)
        {
            SqlDataAdapter DA;
            try
            {
                OpenCon();
                SqlCommand cmd = new SqlCommand(strSql, con);
                DataSet objDS = new DataSet();
                DA = new SqlDataAdapter(cmd);
                DA.Fill(objDS);
                DA.Dispose();
                return objDS;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                CloseCon();
            }
        }
        public object ExecuteMyScalar(string strSql)
        {
            try
            {
                object objData;
                OpenCon();
                SqlCommand cmd = new SqlCommand(strSql, con);
                objData = cmd.ExecuteScalar();
                return objData;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                CloseCon();
            }
        }
        public void Prepare(string CmdName, QueryType CmdType)
        {
            OpenCon();
            cmd = new SqlCommand(CmdName, con);
            if (CmdType == QueryType.StoredProcedure)
                cmd.CommandType = CommandType.StoredProcedure;
        }
        public SqlDataReader ExecuteMyReader(string strSql)
        {
            try
            {
                OpenCon();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand(strSql, con);
                dr = cmd.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddCmdParameter(string pName, SqlDataType dtype, Object val, SqlParaDirection Direction, bool isNull, int len = 0)
        {
            //len = 0;
            SqlDbType datatype = SqlDbType.VarChar;

            switch (dtype)
            {
                case SqlDataType.BigInt:
                    datatype = SqlDbType.BigInt;
                    break;
                case SqlDataType.Double:
                    datatype = SqlDbType.Float;
                    break;
                case SqlDataType.Boolean:
                    datatype = SqlDbType.Bit;
                    break;
                case SqlDataType.Char:
                    datatype = SqlDbType.Char;
                    break;
                case SqlDataType.DateTime:
                    datatype = SqlDbType.DateTime;
                    break;
                case SqlDataType.Float:
                    datatype = SqlDbType.Float;
                    break;
                case SqlDataType.Int:
                    datatype = SqlDbType.Int;
                    break;
                case SqlDataType.Money:
                    datatype = SqlDbType.Money;
                    break;
                case SqlDataType.Varchar:
                    datatype = SqlDbType.VarChar;
                    break;
                case SqlDataType.Image:
                    datatype = SqlDbType.Image;
                    break;
                case SqlDataType.SmallInt:
                    datatype = SqlDbType.SmallInt;
                    break;
                case SqlDataType.Time:
                    datatype = SqlDbType.Time;
                    break;
                case SqlDataType.Date:
                    datatype = SqlDbType.Date;
                    break;
                case SqlDataType.TinyInt:
                    datatype = SqlDbType.TinyInt;
                    break;
                case SqlDataType.Decimal:
                    datatype = SqlDbType.Decimal;
                    break;
                case SqlDataType.Bit:
                    datatype = SqlDbType.Bit;
                    break;
                case SqlDataType.NVarChar:
                    datatype = SqlDbType.NVarChar;
                    break;
                case SqlDataType.DataTable:
                    datatype = SqlDbType.Structured;
                    break;

            }
            if (val == null) { val = ""; }
            ParameterDirection dire = ParameterDirection.Input;
            switch (Direction)
            {
                case SqlParaDirection.In:
                    dire = ParameterDirection.Input;
                    break;
                case SqlParaDirection.InOut:
                    dire = ParameterDirection.InputOutput;
                    break;
                case SqlParaDirection.Out:
                    dire = ParameterDirection.Output;
                    break;
                case SqlParaDirection.Return:
                    dire = ParameterDirection.ReturnValue;
                    break;
            }
            SqlParameter sp = new SqlParameter(pName, datatype, len, dire, isNull, 0, 0, "", DataRowVersion.Current, val);
            cmd.Parameters.Add(sp);
        }
        public DataTable ExecuteCommandDataReturn()
        {
            try
            {
                OpenCon();
                DataTable objDT = new DataTable();
                cmd.CommandTimeout = 0;
                cmd.Transaction = SqlTr;
                SqlDataAdapter objDA = new SqlDataAdapter(cmd);
                objDA.Fill(objDT);
                return objDT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ExecuteCommandReturn(string rp)
        {
            try
            {
                OpenCon();
                cmd.CommandTimeout = 0;
                cmd.Transaction = SqlTr;
                cmd.ExecuteNonQuery();
                int a = Convert.ToInt32(cmd.Parameters[rp].Value.ToString());
                return a;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ExecuteCommand()
        {
            try
            {
                OpenCon();
                cmd.CommandTimeout = 0;
                cmd.Transaction = SqlTr;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ExecuteCommandDataSetReturn()
        {
            try
            {
                OpenCon();
                DataSet objDS = new DataSet();
                cmd.CommandTimeout = 0;
                cmd.Transaction = SqlTr;
                SqlDataAdapter objDA = new SqlDataAdapter(cmd);
                objDA.Fill(objDS);
                return objDS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   

        public string ValidString(string strValue)
        {
            return strValue.Replace("'", "").Trim();
        }
    }
}
