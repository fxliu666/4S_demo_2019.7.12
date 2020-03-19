using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class maintainOrderTableModel
    {
        public long maintainOrderID { get; set; }
        public string maintainOrderNum { get; set; }
        public string appointOrderNum { get; set; }
        public string customerCarNum { get; set; }
        public string createStaffNum { get; set; }
        public string createTime { get; set; }
        public int orderState { get; set; }
    }
}
