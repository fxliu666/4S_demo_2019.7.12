using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4S.WebUI.Models
{
    public class maintainWorkorder
    {
        public string orderNum { get; set; }
        public string clientName { get; set; }
        public string carType { get; set; }
        public string createTime { get; set; }
        public string faultPh { get; set; }
        public string projects { get; set; }
        public string price { get; set; }
        public string state { get; set; }
    }
}