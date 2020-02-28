using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class appointOrder
    {
        public long appointOrderID { get; set; }
        public string appointOrderNum { get; set; }
        public string createTime { get; set; }
        public long customerCarID { get; set; }
        public string expectedTime { get; set; }
        public string actualTime { get; set; }
        public string maintainType { get; set; }
        public string faultDescript { get; set; }
        public string goType { get; set; }
        public string failReason { get; set; }
        public string checkStaffID { get; set; }
        public string checkTime { get; set; }
        public string appointOrderState { get; set; }
    }
}
