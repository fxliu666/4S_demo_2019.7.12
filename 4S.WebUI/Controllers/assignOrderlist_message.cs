using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4S.WebUI.Models;

namespace _4S.WebUI.Controllers
{
    public class assignOrderlist_message
    {
        public int code = 0;
        public string msg = "";
        public int count = 1000;
        public IEnumerable<assignOrder> data;
    }
}