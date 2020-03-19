using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class workOrder
    {
        public long workOrderID { get; set; }
        public string workOrderNum { get; set; }
        public long maintainOrderID { get; set; }
        public long departID { get; set; }
        public long maintainProjectID { get; set; }
        public string expectedTime { get; set; }
        public string actualTime { get; set; }
        public string assignTime { get; set; }
        public string maintainStaff { get; set; }
        public int workOrderState { get; set; }
    }
}
