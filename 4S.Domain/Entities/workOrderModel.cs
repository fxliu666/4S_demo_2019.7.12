using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class workOrderModel
    {
        public List<project> pj { get; set; }
        public List<depart> dp { get; set; }
        public List<partclass> pc { get; set; }
    }
}
