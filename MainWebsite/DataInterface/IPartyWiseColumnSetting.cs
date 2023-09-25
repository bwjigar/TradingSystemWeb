using MainWebsite.DataAccess;
using MainWebsite.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace MainWebsite.DataInterface
{   
    public interface IPartyWiseColumnSetting :IDisposable
    {
        IEnumerable<COLUMN_MAS> GetAllColumnMas();
        IEnumerable<SUPPLIER_MAS> GetAllSupplier();
        void IndertExcelColumnDet(IEnumerable<EXCEL_DEMAND_DET> Mappindata);
        void IndertExcelColumnMas(EXCEL_DEMAND_MAS excelModel);
        IEnumerable<EXCEL_DEMAND_MAS> getAllExcelMas();
        IEnumerable<SHAPE_MAS> GetShape();
        IEnumerable<COLOR_MAS> GetColor();
        IEnumerable<PURITY_MAS> GetPurity();
        IEnumerable<CUT_MAS> GetCut();
        IEnumerable<POLISH_MAS> GetPolish();
        IEnumerable<SYMM_MAS> GetSymm();
        IEnumerable<FLS_MAS> GetFls();
        IEnumerable<LAB_MAS> GetLab();
        IEnumerable<FANCY_COLOR_MAS> GetAllFencyColorMas();        
        IEnumerable<EXCEL_DEMAND_DET> GetAllColumnDet();       
        void UpdateExcelColumnDet(IEnumerable<EXCEL_DEMAND_DET> Mappindata);
        void UpdateExcelColumnMas(EXCEL_DEMAND_MAS excelModel, int id);
        void UpdateClientStock(int id, string Value);
        void UpdateStock(string PacketNo, int Value);
        IEnumerable<CLIENT_STOCK> GetAllClientStock();
        void UpdateShapeMas(int id, string Symm);
        void UpdateCutMas(int id, string Symm);
        void UpdateColorMas(int id, string Symm);
        void UpdatePolishMas(int id, string Symm);
        void UpdateSymmMas(int id, string Symm);
        void UpdateFlsMas(int id, string Symm);
        void UpdateLabMas(int id, string Symm);
        string DeleteMainStock(string SuppSeq);        
    }
}
