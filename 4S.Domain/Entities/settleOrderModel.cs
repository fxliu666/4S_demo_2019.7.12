using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class settleOrderModel
    {
        public string customerName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string carBrand { get; set; }
        public string carType { get; set; }
        public string licenseNum { get; set; }
        public string engineNum { get; set; }
        public string startTime { get; set; }
        public string finishTime { get; set; }
        public List<workOrderSettleModel> orders { get; set; }
    }
}
