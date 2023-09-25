﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainWebsite.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MainWebsite.DataAccess
{
    public class MetadataCOLUMN_MAS
    {
        public int SEQ_NO { get; set; }
        public string COLUMN_NAME { get; set; }
        public string DISPLAY_NAME { get; set; }
        public string PARA_SYNONYM { get; set; }
        public Nullable<short> SORT_NO { get; set; }
        [NotMapped]
        public string EXCEL_COLUMN { get; set; }
    }
    public class MetadataSUPPLIER_MAS
    {
        public int SEQ_NO { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string COMP_NAME { get; set; }
        public string EMAIL_ID { get; set; }
        public string MOBILE_NO { get; set; }
        public string PHONE_NO { get; set; }
        public Nullable<System.DateTime> TRANS_DATE { get; set; }
    }
    public partial class MetadataEXCEL_DEMAND_MAS
    {
        public int SEQ_NO { get; set; }
        [Required(ErrorMessage ="Party Name Required..!")]
        public Nullable<int> SUPP_SEQ { get; set; }
        public Nullable<int> START_EXCEL_READ_LINE { get; set; }
        public string NARRATION { get; set; }
        public string FILE_NAME { get; set; }
        public string DISC_SIGN { get; set; }
        public string PRE_SIGN { get; set; }
        [Required(ErrorMessage = "Please Select Data From..!")]
        public string DATA_FROM { get; set; }
        public string LINK { get; set; }
        public string TYPE_OF_DATA { get; set; }
        public Nullable<int> REFRESH_MINUTE { get; set; }
        public string JSON_DATA_TYPE { get; set; }
        public string PARAMETER { get; set; }
        public string PARAMETER_VALUE { get; set; }
        public string PARAMETER_TYPE { get; set; }
        public string TOKEN_TEXT { get; set; }
        public string TOKEN_KEY { get; set; }
        public string TOKEN_URL { get; set; }
        public string TOKEN_TYPE { get; set; }
        public string EXTRA_WORD { get; set; }
        public Nullable<bool> IS_SPACE_BEFORE_TOKEN { get; set; }
        public string SERVICE_REFERENCE_NAME { get; set; }
        public string SERVICE_REFERENCE_PARAM_VALUES { get; set; }
        public Nullable<bool> MULTIPLE_RUN { get; set; }
        public Nullable<bool> IS_UPLOAD_STRING { get; set; }
    }
}
