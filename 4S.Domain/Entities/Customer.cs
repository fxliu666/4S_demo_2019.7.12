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
        public Guid CustomerID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string UIdentity { get; set; }
    }
}
