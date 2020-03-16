using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class maintainOrderModel
    {
        public long maintainOrderID { get; set; }
        public string maintainOrderNum { get; set; }
        public string appointOrderNum { get; set; }
        public string carNum { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string createTime { get; set; }
        public string createStaff { get; set; }
        public string faultDescript { get; set; }
        public string expectedPj { get; set; }
        public decimal expectedPrice { get; set; }
        public string expectedTime { get; set; }
        public workOrderModel model { get; set; }
    }
}
