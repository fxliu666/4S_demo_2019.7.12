using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class maintainOrder
    {
        public Guid maintainOrder_id { get; set; }
        public string maintainOrder_num { get; set; }
        public DateTime createTime { get; set; }
        public Guid clientCar_id { get; set; }
        public DateTime firstOrder_time { get; set; }
        public DateTime finalOrder_time { get; set; }
        public string maintainType  { get; set; }
        public string faultPhenomenon { get; set; }
        public string wayTostore { get; set; }
        public string reasonForrefuse { get; set; }
        public Guid checkStaff_id { get; set; }
        public DateTime checkTIme { get; set; }
        public int maintainOrderstate { get; set; }
    }
}
