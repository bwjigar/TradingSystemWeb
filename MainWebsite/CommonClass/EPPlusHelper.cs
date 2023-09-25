using Aspose.Cells;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
namespace MainWebsite.CommonClass
{
    public class EPPlusHelper
    {

        public static ExcelPackage xlsPackage;
        public static ExcelWorksheet WorkSheet;

        public static DataTable LoadDataTable(string FilePath)
        {
            DataTable tbl = new DataTable();
            try
            { 
                string fileExtension = Path.GetExtension(FilePath);
               
                switch (fileExtension.ToLower())
                {                   
                    case ".xlsx":                       
                        tbl = ConvertExcelToDataTable(FilePath);
                        break;
                    case ".xls":                        
                        tbl = ExcelToDataTableWithOleDb(FilePath);
                        break;
                    case ".csv":
                        tbl = ConvertCsvToDataTable(FilePath);
                        break;
                }                
                return tbl;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       
        public static DataTable ConvertExcelToDataTable(string FilePath)
        {
            DataTable tbl = new DataTable();
            FileStream stream = null;
            stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            xlsPackage = new ExcelPackage(stream);
            var ws = xlsPackage.Workbook.Worksheets.First();

            int Frow = 1;
            for (int Fcol = 1; Fcol < 30; Fcol++)
            {
                var mergedadress = ws.MergedCells[Frow, Fcol];
                if (mergedadress != null)
                {
                    Frow = Frow + 1;
                    mergedadress = null;
                }
            }
            foreach (var firstRowCell in ws.Cells[Frow, 1, 1, ws.Dimension.End.Column])
            {
                tbl.Columns.Add(firstRowCell.Text);
            }
            var startRow = 3;
            for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                DataRow row = tbl.Rows.Add();
                try
                {
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.ToString();
                    continue;
                }
            }
            tbl.Rows.RemoveAt(tbl.Rows.Count - 1);
            return tbl;
        }
       
        public static DataTable ExcelToDataTableWithOleDb(string Path)
        {
            DataTable dtResult = null;
            int totalSheet = 0; //No of sheets on excel file  
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;

                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();                  

                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                return dtResult; //Returning Dattable  
            }
        }
        public static DataTable ConvertCsvToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }

    }
}
