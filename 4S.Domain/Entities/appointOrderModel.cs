using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class appointOrderModel
    {
        public Customer cus { get; set; }
        public customerCar car { get; set; }
        public appointOrder order { get; set; }

    }
}
