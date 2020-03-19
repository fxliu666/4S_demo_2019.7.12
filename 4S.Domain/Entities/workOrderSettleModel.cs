using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class workOrderSettleModel
    {
        public long workOrderID { get; set; }
        public string projectName { get; set; }
        public float workTime { get; set; }
        public string departNum { get; set; }
        public List<partSettleModel> parts { get; set; }
    }

}
