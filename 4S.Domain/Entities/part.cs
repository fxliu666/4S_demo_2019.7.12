using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class part
    {
        public long partID { get; set; }
        public string partNum { get; set; }
        public string partName { get; set; }
        public bool isOrigin { get; set; }
        public string qualification { get; set; }
        public long createstaffID { get; set; }
        public string createTime { get; set; }
    }
}
