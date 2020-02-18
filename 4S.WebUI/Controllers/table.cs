using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4S.Domain.Entities;
namespace _4S.WebUI.Controllers
{
    //模板类，以格式化json传回前端
    public class table<T>
    {
        public int code = 0;
        public string msg = "";
        public int count = 1000;
        public IEnumerable<T> data;
    }
}