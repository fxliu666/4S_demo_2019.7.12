using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class depart
    {
        public long departID { get; set; }
        public string departNum { get; set; }
        public string departName { get; set; }
        public string departLevel { get; set; }
        public long parentID { get; set; }
        public string createTime { get; set; }
        public long createStaffID { get; set; }
        public string updateTime { get; set; }
        public long updateStaffID { get; set; }
    }
}
