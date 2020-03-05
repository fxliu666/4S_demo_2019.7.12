using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4S.Domain.Entities
{
    public class staff
    {
        public long staffID { get; set; }                     //id
        public string staffNum { get; set; }                  //员工编号
        public string staffName { get; set; }                 //员工姓名
        public long departID { get; set; }                    //部门编号
        public string staffPosition { get; set; }             //职位
        public string staffAccount { get; set; }              //账号
        public string staffPasswd { get; set; }               //密码
        public string staffPhone { get; set; }                //手机号
    }
}
