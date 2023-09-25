using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWebsite.DTO
{
   public class ColumnMasDto
    {
        public int SEQ_NO { get; set; }
        public string COLUMN_NAME { get; set; }
        public string DISPLAY_NAME { get; set; }
        public string PARA_SYNONYM { get; set; }
        public Nullable<short> SORT_NO { get; set; }
        public string EXCEL_COLUMN { get; set; }
        public string MAS_SEQ { get; set; }
        public string DB_COLUMN { get; set; }
        public int COLUMN_SEQ { get; set; }
    }
}
