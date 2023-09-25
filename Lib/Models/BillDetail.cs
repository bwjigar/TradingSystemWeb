using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class BillDetailGrid_Request
    {
        public string PageSize { get; set; }
        public string PageNo { get; set; }
        public string OrderBy { get; set; }
        public string Search { get; set; }
    }
    public class BillDetailGrid_Response
    {
        public long TotRec { get; set; }
        public long SrNo { get; set; }
        public string SPCode { get; set; }
        public string SPDate { get; set; }
        public string SPTrnType { get; set; }
        public string SPInvNo { get; set; }
        public string PartyBillNo { get; set; }
        public string PartyBillDate { get; set; }
        public string DueDate { get; set; }
        public string DueDays { get; set; }
        public string SPPartyCode { get; set; }
        public string AcName { get; set; }
        public string SPtotQuantity { get; set; }
        public string SPTotalAmount { get; set; }
        public string SPDiscountPer { get; set; }
        public string SPIGST { get; set; }
        public string SPSGST { get; set; }
        public string SPCGST { get; set; }
        public string RoundOff { get; set; }
        public string SPExtraAmount { get; set; }
        public string SPFinalAmount { get; set; }
        public string SPBillRemarks { get; set; }
        public string CreateUser { get; set; }
        public string CreateDate { get; set; }
        public string ModifyUser { get; set; }
        public string ModifyDate { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleKM { get; set; }
        public string VehiclePeriodFromDate { get; set; }
        public string VehiclePeriodToDate { get; set; }
        public string Three_MCalling { get; set; }
        public string FeedBack { get; set; }
        public string Six_MCalling { get; set; }
    }
}
