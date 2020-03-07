using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class project
    {
        public long projectID { get; set; }
        public string projectNum { get; set; }
        public string projectName { get; set; }
        public string projectDescript { get; set; }
        public long createStaffID { get; set; }
        public string createTime { get; set; }
    }
}
