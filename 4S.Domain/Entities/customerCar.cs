using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class customerCar
    {
        public long customerCarID { get; set; }
        public string customerCarNum { get; set; }
        public long customerID { get; set; }
        public string carBrand { get; set; }
        public string carType { get; set; }
        public string engineNum { get; set; }
        public string licenseNum { get; set; }
        public string buyDate { get; set; }
        

    }
}
