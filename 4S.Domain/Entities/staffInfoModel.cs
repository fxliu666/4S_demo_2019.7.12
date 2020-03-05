using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class staffInfoModel
    {
        public staff sta { get; set; }
        public depart dep { get; set; }
        public List<depart> deps { get; set; }
        public List<position> pos { get; set; }

    }
}
