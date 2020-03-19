using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class workOrderEditModel
    {
        public long workOrderID { get; set; }
        public string workOrderNum { get; set; }         //派工单号
        public string maintainOrderNum { get; set; }     //维保工单号
        public string departNum { get; set; }            //部门编号
        public string maintainProjectName { get; set; }  //维保项目名
        public string actualTime { get; set; }           //实际完成时间
        public string expectedTime { get; set; }         //预计完成时间
        public string assignTime { get; set; }           //派工时间
        public float exWorkTime { get; set; }            //预计维保工时
        public float acWorkTime { get; set; }            //实际维保工时
        public string maintainStaff { get; set; }        //维保员工
        public int workOrderState { get; set; }          //派工单状态
    } 
}
