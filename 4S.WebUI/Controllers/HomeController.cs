using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4S.WebUI.Models;
using _4S.Domain.Entities;
using _4S.Domain.Abstract;


namespace _4S.WebUI.Controllers
{
    public class HomeController : Controller
    {

        ICustomersRepository repository;
        public HomeController(ICustomersRepository repo)
        {
            repository = repo;
        }
        // GET: Home
        public ActionResult Index()
        {
        
            return View();
        }

        public JsonResult List()
        {
            IEnumerable<Customer> Customers = repository.Customers;

          
            return Json(Customers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PersonList(string name="")
        {
            table tt = new table();
            if (name == "")
            {
                tt.data = repository.Customers;
            }
            else
            {
                List<Customer> temp = new List<Customer>();
               // temp.Add(repository.Customers
                //    .First(x => x.Name == name));
                var obj = from n in repository.Customers
                         where n.Name == name
                         select n;
                tt.data = obj;
            }

            //IList<PersonViewModel> persons = new List<PersonViewModel>();
            //for(int i = 0;i < Customers.Count(); i++)
            //{
            //    persons.Add(new PersonViewModel { Name = "hello", PersonID = i, IsMarried = false, PhoneNum = "112233" });
            //}
            //Response.Write(Json(Customers, JsonRequestBehavior.AllowGet).ToString());
           
            return Json(tt, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult MaintainOrderMan()
        {
            return PartialView();
        }
    }
}