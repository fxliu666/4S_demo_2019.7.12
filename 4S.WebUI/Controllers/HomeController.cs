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


        public JsonResult maintainOrder_list(string name="")
        {
            maintainOrderList_message orders = new maintainOrderList_message();
            List<maintainOrderVM> rt = new List<maintainOrderVM>();

            maintainOrderVM temp = new maintainOrderVM()
            {
                maintainOrder_num = "1101"

            };
            rt.Add(temp);
            //foreach (maintainOrder temp in repository.maintainOrders.ToList())
            //{
            //    maintainOrderVM item = new maintainOrderVM();
            //    item.maintainOrder_num = temp.maintainOrder_num;
            //    item.createTime = temp.createTime;
            //    item.clientCar_id = temp.clientCar_id;
            //    item.firstOrder_time = temp.firstOrder_time;
            //    item.maintainType = temp.maintainType;
            //    item.faultPhenomenon = temp.faultPhenomenon;
            //    item.wayTostore = temp.wayTostore;
            //    item.reasonForrefuse = temp.reasonForrefuse;
            //    rt.Add(item);
            //}
            orders.data = rt;
            return Json(orders, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MaintainOrderCheck(string name = "")
        {
            var obj = from n in repository.Customers
                      where n.Name == name
                      select n;

            Customer pass = obj.First();
            return View(pass);
        }

        public ActionResult MaintainOrderEdit(string name = "")
        {
            var obj = from n in repository.Customers
                      where n.Name == name
                      select n;

            Customer pass = obj.First();
            return View(pass);
        }


        public ViewResult MaintainworkOrderCreate(string orderNum = "")
        {
            return View();
        }

        public ViewResult MaintainjobOrderCreate()
        {
            return View();
        }

        public ViewResult MaintainOrderPass()
        {
            return View();
        }

        public ViewResult MaintainworkOrderQuery()
        {
            return View();
        }

        public ViewResult MaintainworkOrderAssign()
        {
            return View();
        }


        public ViewResult MaintainworkOrderGetParts()
        {
            return View();
        }

        public ViewResult MaintainworkOrderCloseAccount()
        {
            return View();
        }

        public JsonResult MaintainworkOrder_list()
        {
            maintainworkOrderlist_message message = new maintainworkOrderlist_message();
            List<maintainWorkorder> orders = new List<maintainWorkorder>();
            maintainWorkorder order = new maintainWorkorder()
            {
                orderNum = "20180520002",
                clientName = "王大雷",
                carType = "宝马I8",
                createTime = "20180902172389",
                faultPh = "车一直熄火",
                projects = "发动机维修",
                price = "800",
                state = "待派工"
            };
            for(int i = 0; i < 10; i++)
            {
                orders.Add(order);
            }
           
            message.data = orders;
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MaintainworkOrder_list2()
        {
            maintainworkOrderlist_message message = new maintainworkOrderlist_message();
            List<maintainWorkorder> orders = new List<maintainWorkorder>();
            maintainWorkorder order = new maintainWorkorder()
            {
                orderNum = "20180520002",
                clientName = "王大雷",
                carType = "宝马I8",
                createTime = "20180902172389",
                faultPh = "车一直熄火",
                projects = "发动机维修",
                price = "800",
                state = "已完成"
            };
            for (int i = 0; i < 10; i++)
            {
                orders.Add(order);
            }

            message.data = orders;
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MaintainassignkOrder_list()
        {
            assignOrderlist_message message = new assignOrderlist_message();
            List<assignOrder> orders = new List<assignOrder>();
            assignOrder order = new assignOrder()
            {
                orderNum="2018062000201",
                workNum="20180620002",
                groupNum="0102",
               maintainProj="发送机维修",
               maintainTime="2018-9-20",
               AssignTime="2018-9-21-10-20-45",
               maintainPeople="李德军，刘祖",
               state="维修中"
    };
            for (int i = 0; i < 10; i++)
            {
                orders.Add(order);
            }

            message.data = orders;
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ViewResult DepartAdd()
        {
            return View();
        }


        public ViewResult StaffAdd()
        {
            return View();
        }

        public ViewResult CarAdd()
        {
            return View();
        }

        public ViewResult PartsAdd()
        {
            return View();
        }

        public ViewResult AddProject()
        {
            return View();
        }

        public ViewResult AddPart()
        {
            return View();
        }
    }
}