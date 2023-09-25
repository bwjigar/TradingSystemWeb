using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class DashboardCount_Response
    {
        public string TodayServiceAmt { get; set; }
        public string TodaySaleAmt { get; set; }
        public string TodayCashReceive { get; set; }
        public string TodayCashPay { get; set; }
    }
    public class Last_10_Sevice_Bill_Response
    {
        public long SrNo { get; set; }
        public string SP_Date { get; set; }
        public string Party_Name { get; set; }
        public string TotalAmt { get; set; }
    }
    public class Last_10_Party_Response
    {
        public long SrNo { get; set; }
        public string Party_Name { get; set; }
        public string StateName { get; set; }
    }
}
