using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4S.WebUI.Models
{
    public class assignOrder
    {
        public string orderNum { get; set; }
        public string workNum { get; set; }
        public string groupNum { get; set; }
        public string maintainProj { get; set; }
        public string maintainTime { get; set; }
        public string AssignTime { get; set; }
        public string maintainPeople { get; set; }
        public string state { get; set; }
    }
}