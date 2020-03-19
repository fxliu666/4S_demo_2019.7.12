using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class partReceive
    {
        public long partReceiveID { get; set; }
        public string workOrderNum { get; set; }
        public string partNum { get; set; }
        public int partQuantity { get; set; }
        public long partEXID { get; set; }
    }
}
