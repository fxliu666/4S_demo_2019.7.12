using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4S.Domain.Entities;
namespace _4S.WebUI.Controllers
{
    public class table
    {
        public int code = 0;
        public string msg = "";
        public int count = 1000;
        public IEnumerable<Customer> data;
    }
}