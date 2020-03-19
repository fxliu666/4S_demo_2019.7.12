using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class maintainOrder
    {
        public long maintainOrderID { get; set; }
        public string maintainOrderNum { get; set; }
        public long maintainAppointID { get; set; }
        public long customerCarID{ get; set; }
        public long createStaffID { get; set; }
        public string createTime { get; set; }
        public string faultDescript  { get; set; }
        public string expectedProjects { get; set; }
        public decimal expectedPrice { get; set; }
        public string actualProjects { get; set; }
        public decimal actualPrice { get; set; }
        public string startTime { get; set; }
        public string finishTime { get; set; }
        public string expectedPickTime { get; set; }
        public string actualPickTime { get; set; }
        public long pickStaffID { get; set; }
        public int orderState { get; set; }
    }
}
