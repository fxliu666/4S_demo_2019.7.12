using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class Customer
    {
        public long customerID { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string customerAddress { get; set; }
        public string customerPID { get; set; }
    }
}
