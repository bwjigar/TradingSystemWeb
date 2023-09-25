using MainWebsite.DataInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainWebsite.DataAccess;
using MainWebsite.DTO;
using System.Data.Entity;
using System.Windows.Forms;
namespace MainWebsite.Data_Repositary
{
    public class PartyWiseColumnSetting : IPartyWiseColumnSetting, IDisposable
    {
        private TradingDBEntities _Tradcontext;
        public PartyWiseColumnSetting(TradingDBEntities Tradcontext)
        {
            this._Tradcontext = Tradcontext;
        }
        
        public IEnumerable<COLUMN_MAS> GetAllColumnMas()
        {
            return _Tradcontext.COLUMN_MAS.ToList();
        }
               
        public IEnumerable<SUPPLIER_MAS> GetAllSupplier()
        {
            return _Tradcontext.SUPPLIER_MAS.ToList();
        }

        /// <summary>
        /// Insert Column Mapping Table Into EXCEL_DEMAND_DET
        /// </summary>
        /// <param name="Mappindata"></param>
        public void IndertExcelColumnDet(IEnumerable<EXCEL_DEMAND_DET> Mappindata)
        {
            try
            {
               if(Mappindata!=null)
                {
                    _Tradcontext.EXCEL_DEMAND_DET.AddRange(Mappindata);
                    _Tradcontext.SaveChanges();
                }
               
            }
            catch (Exception ex)
            {                
                throw;
            }        
                     
        }

        /// <summary>
        /// Insert Column Mapping Data Into EXCEL_DEMAND_MAS By Party
        /// </summary>
        /// <param name="excelModel"></param>
        public void IndertExcelColumnMas(EXCEL_DEMAND_MAS excelModel)
        {
            try
            {
                _Tradcontext.EXCEL_DEMAND_MAS.Add(excelModel);
                _Tradcontext.SaveChanges();
            }
            catch (Exception ex)
            {               
                throw;
            }
           
        }
        public IEnumerable<EXCEL_DEMAND_MAS> getAllExcelMas()
        {
            return _Tradcontext.EXCEL_DEMAND_MAS.ToList();
        }
        public void Save()
        {
            _Tradcontext.SaveChanges();
        }

        public IEnumerable<SHAPE_MAS> GetShape()
        {
            return _Tradcontext.SHAPE_MAS.ToList();
        }
        public IEnumerable<COLOR_MAS> GetColor()
        {
            return _Tradcontext.COLOR_MAS.ToList();
        }
        public IEnumerable<PURITY_MAS> GetPurity()
        {
            return _Tradcontext.PURITY_MAS.ToList();
        }
        public IEnumerable<CUT_MAS> GetCut()
        {
            return _Tradcontext.CUT_MAS.ToList();
        }
        public IEnumerable<POLISH_MAS> GetPolish()
        {
            return _Tradcontext.POLISH_MAS.ToList();
        }
        public IEnumerable<SYMM_MAS> GetSymm()
        {
            return _Tradcontext.SYMM_MAS.ToList();
        }
        public IEnumerable<FLS_MAS> GetFls()
        {
            return _Tradcontext.FLS_MAS.ToList();
        }
        public IEnumerable<LAB_MAS> GetLab()
        {
            return _Tradcontext.LAB_MAS.ToList();
        }
        public IEnumerable<EXCEL_DEMAND_DET> GetAllColumnDet()
        {
            return _Tradcontext.EXCEL_DEMAND_DET.ToList();
        }

        /// <summary>
        /// Update ReMapping Record into EXCEL_DEMAND_DET
        /// </summary>
        /// <param name="Mappindata"></param>
        public void UpdateExcelColumnDet(IEnumerable<EXCEL_DEMAND_DET> Mappindata)
        {
            try
            {
                if (Mappindata != null)
                {
                    foreach (var item in Mappindata)
                    {
                        _Tradcontext.Entry(item).State = EntityState.Modified;
                        _Tradcontext.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {               
                throw;
            }
        }

        /// <summary>
        /// Update Party Record Into EXCEL_DEMAND_MAS
        /// </summary>
        /// <param name="excelModel"></param>
        /// <param name="id"></param>
        public void UpdateExcelColumnMas(EXCEL_DEMAND_MAS excelModel, int id)
        {
            try
            {
                var data = _Tradcontext.EXCEL_DEMAND_MAS.Find(id);
                if (data != null)
                {
                    data.SUPP_SEQ = excelModel.SUPP_SEQ;
                    data.NARRATION = excelModel.NARRATION;
                    data.FILE_NAME = excelModel.FILE_NAME;
                    data.DATA_FROM = excelModel.DATA_FROM;
                    data.LINK = excelModel.LINK;
                    data.TYPE_OF_DATA = excelModel.TYPE_OF_DATA;
                    _Tradcontext.Entry(data).State = EntityState.Modified;
                    _Tradcontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {               
                throw;
            }

        }
        public IEnumerable<CLIENT_STOCK> GetAllClientStock()
        {
            return _Tradcontext.CLIENT_STOCK.ToList();
        }
        public IEnumerable<FANCY_COLOR_MAS> GetAllFencyColorMas()
        {
            return _Tradcontext.FANCY_COLOR_MAS.ToList();
        }
        public void UpdateClientStock(int id,string Value)
        {
            try
            {
                CLIENT_STOCK obj   = _Tradcontext.CLIENT_STOCK.Find(id);
                obj.SHAPE = Value;
                obj.ERROR_FLAG = false;
                if (obj != null)
                {
                    _Tradcontext.Entry(obj).State = EntityState.Modified;                   
                    _Tradcontext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateStock(string PacketNo, int Value)
        {
            try
            {
                STOCK obj = _Tradcontext.STOCKs.Where(x => x.PACKET_NO == PacketNo).FirstOrDefault();
                obj.SHAPE_SEQ = Convert.ToByte(Value);
                                
                if (obj != null)
                {
                    _Tradcontext.Entry(obj).State = EntityState.Modified;                   
                    _Tradcontext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateShapeMas(int id, string Symm)
        {
            try
            {
                SHAPE_MAS obj = _Tradcontext.SHAPE_MAS.Find(id);                
                if(obj!=null)
                {
                    var SymmList = obj.SYNONYM_LIST;
                    if (!(SymmList.Contains(Symm.ToString())))
                    {
                        obj.SYNONYM_LIST = SymmList + "|" + Symm;
                        _Tradcontext.Entry(obj).State = EntityState.Modified;
                        _Tradcontext.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateCutMas(int id, string Symm)
        {
            try
            {
                CUT_MAS obj = _Tradcontext.CUT_MAS.Find(id);               
                if (obj != null)
                {
                    var SymmList = obj.SYNONYM_LIST;
                    if(!(SymmList.Contains(Symm.ToString())))
                    {
                        obj.SYNONYM_LIST = SymmList + "|" + Symm;
                        _Tradcontext.Entry(obj).State = EntityState.Modified;
                        _Tradcontext.SaveChanges();
                    }
                }
                   
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateColorMas(int id, string Symm)
        {
            try
            {
                COLOR_MAS obj = _Tradcontext.COLOR_MAS.Find(id);                
                if (obj != null)
                {
                    var SymmList = obj.SYNONYM_LIST;
                    if (!(SymmList.Contains(Symm.ToString())))
                    {
                        obj.SYNONYM_LIST = SymmList + "|" + Symm;
                        _Tradcontext.Entry(obj).State = EntityState.Modified;
                        _Tradcontext.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdatePolishMas(int id, string Symm)
        {
            try
            {
                POLISH_MAS obj = _Tradcontext.POLISH_MAS.Find(id);               
                if (obj != null)
                {
                    var SymmList = obj.SYNONYM_LIST;
                    if (!(SymmList.Contains(Symm.ToString())))
                    {
                        obj.SYNONYM_LIST = SymmList + "|" + Symm;
                        _Tradcontext.Entry(obj).State = EntityState.Modified;
                        _Tradcontext.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateSymmMas(int id, string Symm)
        {
            try
            {
                SYMM_MAS obj = _Tradcontext.SYMM_MAS.Find(id);                
                if (obj != null)
                {
                    var SymmList = obj.SYNONYM_LIST;
                    if (!(SymmList.Contains(Symm.ToString())))
                    {
                        obj.SYNONYM_LIST = SymmList + "|" + Symm;
                        _Tradcontext.Entry(obj).State = EntityState.Modified;
                        _Tradcontext.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateFlsMas(int id, string Symm)
        {
            try
            {
                FLS_MAS obj = _Tradcontext.FLS_MAS.Find(id);                
                if (obj != null)
                {
                    var SymmList = obj.SYNONYM_LIST;
                    if (!(SymmList.Contains(Symm.ToString())))
                    {
                        obj.SYNONYM_LIST = SymmList + "|" + Symm;
                        _Tradcontext.Entry(obj).State = EntityState.Modified;
                        _Tradcontext.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateLabMas(int id, string Symm)
        {
            try
            {
                LAB_MAS obj = _Tradcontext.LAB_MAS.Find(id);               
                if (obj != null)
                {
                    var SymmList = obj.SYNONYM_LIST;
                    if (!(SymmList.Contains(Symm.ToString())))
                    {
                        obj.SYNONYM_LIST = SymmList + "|" + Symm;
                        _Tradcontext.Entry(obj).State = EntityState.Modified;
                        _Tradcontext.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteMainStock(string SuppSeq)
        {
            try
            {
                var id = int.Parse(SuppSeq);
                List<STOCK> obj = _Tradcontext.STOCKs.Where(x => x.SUPP_SEQ == id).ToList();
                if(obj.Count > 0)
                {
                    _Tradcontext.STOCKs.RemoveRange(obj);
                    _Tradcontext.SaveChanges();
                    return "Success";
                }
                return "Data Not Found";

            }
            catch (Exception ex)
            {
                return "FAIL";
                throw;
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    _Tradcontext.Dispose();
                }
            }
            this.disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
        #endregion

    }
}
