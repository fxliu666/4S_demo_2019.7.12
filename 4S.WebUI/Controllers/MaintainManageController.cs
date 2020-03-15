using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using _4S.Domain.Entities;
namespace _4S.WebUI.Controllers
{
    public class MaintainManageController : Controller
    {
        static IDbConnection db = DapperService.MySqlConnection();

        public ActionResult MaintainorderIndex()
        {
            return View();
        }
        // GET: MaintainManage
        public ActionResult WorkorderIndex()
        {
            return View();
        }
        
        public JsonResult LoadOrders(int page = 1, int limit = 10, int code = 0)
        {
            CheckConnect();
            table<maintainOrder> data = new table<maintainOrder>();
            string query;
            int count = 1000;
            string querycount = "select * from MAINTAIN_ORDER";
            count = db.Query<maintainOrder>(querycount).Count();
            List<maintainOrder> orders;
            if (code == 0)
            {
                query = "select * from MAINTAIN_ORDER limit " + ((page - 1) * limit).ToString() + "," + limit.ToString();
                orders = (List<maintainOrder>)db.Query<maintainOrder>(query);
            }
            else if (code == 1)
            {
                query = "select * from MAINTAIN_ORDER where appointOrderState = 1 limit " + ((page - 1) * limit).ToString() + "," + limit.ToString();
                orders = (List<maintainOrder>)db.Query<maintainOrder>(query);
            }
            else if (code == 2)
            {
                query = "select * from MAINTAIN_ORDER where appointOrderState = 2 limit " + ((page - 1) * limit).ToString() + "," + limit.ToString();
                orders = (List<maintainOrder>)db.Query<maintainOrder>(query);
            }
            else
            {
                query = "select * from MAINTAIN_ORDER where appointOrderState = 3 limit " + ((page - 1) * limit).ToString() + "," + limit.ToString();
                orders = (List<maintainOrder>)db.Query<maintainOrder>(query);
            }
            data.data = orders;
            data.count = count;
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //审核预约单
        public ViewResult CheckOrder(long id)
        {
            ViewBag.id = id;
            return View(ViewBag.id);
        }

        //加载数据
        public JsonResult LoadData(long id)
        {
            CheckConnect();
            string queryOrder = "select * from APPOINT_ORDER where appointOrderID = @Id";
            string queryCar = "select * from CUSTOMER_CAR_BASE where customerCarID = @Id";
            string queryCustomer = "select * from CUSTOMER where customerID = @Id";
            appointOrderModel model = new appointOrderModel();
            appointOrder order = (appointOrder)db.Query<appointOrder>(queryOrder, new { Id = id }).SingleOrDefault();
            customerCar car = (customerCar)db.Query<customerCar>(queryCar, new { Id = order.customerCarID }).SingleOrDefault();
            Customer cus = (Customer)db.Query<Customer>(queryCustomer, new { Id = car.customerID }).SingleOrDefault();
            model.cus = cus;
            model.car = car;
            model.order = order;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //审核结果
        public JsonResult CheckResult(long id, int code, string time = "", string mt = "", string gt = "", string fd = "", string rr = "")
        {
            CheckConnect();
            int result = 0;
            if (code == 0)
            {
                string set = "update APPOINT_ORDER set appointOrderState = 2,expectedTime = @Time," +
                    "maintainType = @Mt,faultDescript = @Fd,goType = @Gt where appointOrderID = @Id";
                result = db.Execute(set, new { Id = id, Time = time, Mt = mt, Fd = fd, Gt = gt });

            }
            else if (code == 1)
            {
                string set = "update APPOINT_ORDER set appointOrderState = 3,failReason = @Rr where appointOrderID = @Id";
                result = db.Execute(set, new { Id = id, Rr = rr });
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        //预约单详情
        public ViewResult OrderDetail(int id)
        {
            ViewBag.id = id;
            return View(ViewBag.id);
        }


        //新建维保工单
        public ViewResult AddMaintainOrder()
        {
            return View();
        }

        //导入用户信息或预约单信息
        public JsonResult LoadCusOrAppoint()
        {
            CheckConnect();
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        //派工
        public ViewResult AssignWorkOrder(long id)
        {
            ViewBag.id = id;
            return View(ViewBag.id);
        }

        //派工单model
        public JsonResult LoadWorkOrder()
        {
            CheckConnect();
            string query1 = "SELECT * FROM MAINTAIN_PROJECT";
            string query2 = "SELECT * FROM DEPART_BASE WHERE departLevel = 3";
            string query3 = "SELECT * FROM CLASS_ITEM WHERE classNum LIKE '030_'";
            List<project> pj = (List<project>)db.Query<project>(query1);
            List<depart> dp = (List<depart>)db.Query<depart>(query2);
            List<partclass> pc = (List<partclass>)db.Query<partclass>(query3);
            workOrderModel model = new workOrderModel() { pj = pj, dp = dp, pc = pc };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //选择零件
        public JsonResult PartSelect(long id)
        {
            CheckConnect();
            string query = "SELECT * FROM MAINTAIN_PART WHERE classID = @Id";
            List<part> p = (List<part>)db.Query<part>(query, new { Id = id });
            return Json(p, JsonRequestBehavior.AllowGet);
        }
        public void CheckConnect()
        {
            if (db.State == ConnectionState.Open) db.Close();
            db.Open();
        }
    }
}