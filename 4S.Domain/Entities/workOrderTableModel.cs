using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class workOrderTableModel
    {
        public long workOrderID { get; set; }
        public string workOrderNum { get; set; }
        public string maintainOrderNum { get; set; }
        public string departNum { get; set; }
        public string expectedTime { get; set; }
        public string assignTime { get; set; }
        public int workOrderState { get; set; }
    }
}
