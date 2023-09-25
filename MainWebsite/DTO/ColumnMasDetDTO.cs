using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWebsite.DTO
{
   public class ColumnMasDetDTO
    {
        public int SEQ_NO { get; set; }
        public Nullable<int> MAS_SEQ { get; set; }
        public string DB_COLUMN { get; set; }
        public string EXCEL_COLUMN { get; set; }
    }
}
