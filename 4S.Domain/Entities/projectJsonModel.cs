using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class projectJsonModel
    {
        public long pjId { get; set; }
        public long dpId { get; set; }
        public string time { get; set; }
        public List<partJsonModel> parts { get; set;}
    }
}
