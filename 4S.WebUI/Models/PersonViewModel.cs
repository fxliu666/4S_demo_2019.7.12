using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4S.WebUI.Models
{
    public class PersonViewModel
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public bool IsMarried { get; set; }
    }
}