using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4S.WebUI.Models
{
    public class maintainOrderVM
    {
        public string maintainOrder_num { get; set; }
        public DateTime createTime { get; set; }
        public Guid clientCar_id { get; set; }
        public DateTime firstOrder_time { get; set; }
        public string maintainType { get; set; }
        public string faultPhenomenon { get; set; }
        public string wayTostore { get; set; }
        public string reasonForrefuse { get; set; }
    }
}