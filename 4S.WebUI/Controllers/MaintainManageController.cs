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
using Newtonsoft.Json;
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


        public JsonResult LoadOrders(int page = 1, int limit = 10, int code = 0)
        {
            CheckConnect();
            table<maintainOrderTableModel> data = new table<maintainOrderTableModel>();
            string query;
            string querycount = "SELECT COUNT(1) FROM MAINTAIN_ORDER";
            int count = db.Query<int>(querycount).SingleOrDefault();
            List<maintainOrderTableModel> orders;
            if (code == 0)
            {
                query = "SELECT M.maintainOrderID as maintainOrderID,M.maintainOrderNum as maintainOrderNum,"+
                        "A.appointOrderNum as appointOrderNum,C.customerCarNum as customerCarNum,"+
                        "S.staffNum as createstaffNum,M.createTime as createTime,M.orderState as orderState"+
                        " FROM MAINTAIN_ORDER AS M"+
                        " LEFT JOIN APPOINT_ORDER AS A"+
                        " ON M.maintainAppointID = A.appointOrderID"+
                        " INNER JOIN CUSTOMER_CAR_BASE AS C"+
                        " ON M.customerCarID = C.customerCarID"+
                        " INNER JOIN STAFF_BASE AS S"+
                        " ON M.createStaffID = S.staffID"+
                        " LIMIT "+ ((page - 1) * limit).ToString() + "," + limit.ToString();
                
            }
            else
            {
                query = "SELECT M.maintainOrderID as maintainOrderID,M.maintainOrderNum as maintainOrderNum," +
                        "A.appointOrderNum as appointOrderNum,C.customerCarNum as customerCarNum," +
                        "S.staffNum as createstaffNum,M.createTime as createTime,M.orderState as orderState" +
                        " FROM MAINTAIN_ORDER AS M" +
                        " INNER JOIN APPOINT_ORDER AS A" +
                        " ON M.maintainAppointID = A.appointOrderID" +
                        " INNER JOIN CUSTOMER_CAR_BASE AS C" +
                        " ON M.customerCarID = C.customerCarID" +
                        " INNER JOIN STAFF_BASE AS S" +
                        " ON M.createStaffID = S.staffID" +
                        " WHERE M.orderState == " + code.ToString()+
                        " LIMIT " + ((page - 1) * limit).ToString() + "," + limit.ToString();
            }
            orders = (List<maintainOrderTableModel>)db.Query<maintainOrderTableModel>(query);
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
        public JsonResult LoadWorkOrder(long id)
        {
            CheckConnect();
            string query1 = "SELECT * FROM MAINTAIN_PROJECT";
            string query2 = "SELECT * FROM DEPART_BASE WHERE departLevel = 3";
            string query3 = "SELECT * FROM CLASS_ITEM WHERE classNum LIKE '030_'";
            string query4 = "SELECT * FROM MAINTAIN_ORDER WHERE maintainOrderID = @Id";
            string query5 = "SELECT customerCarNum FROM CUSTOMER_CAR_BASE WHERE customerCarID = @Id";
            string query6 = "SELECT customerID FROM CUSTOMER_CAR_BASE WHERE customerCarID = @Id";
            string query7 = "SELECT customerName FROM CUSTOMER WHERE customerID = @Id";
            string query8 = "SELECT customerPhone FROM CUSTOMER WHERE customerID = @Id";
            string query9 = "SELECT staffNum FROM STAFF_BASE WHERE staffID = @Id";
            List<project> pj = (List<project>)db.Query<project>(query1);
            List<depart> dp = (List<depart>)db.Query<depart>(query2);
            List<partclass> pc = (List<partclass>)db.Query<partclass>(query3);
            maintainOrder order = db.Query<maintainOrder>(query4, new { Id = id }).SingleOrDefault();
            string carNum = db.Query<string>(query5, new { Id = order.customerCarID }).SingleOrDefault();
            long customerId = db.Query<long>(query6, new { Id = order.customerCarID }).SingleOrDefault();
            string customerName = db.Query<string>(query7, new { Id = customerId }).SingleOrDefault();
            string phone = db.Query<string>(query8, new { Id = customerId }).SingleOrDefault();
            string staffNum = db.Query<string>(query9, new { Id = order.createStaffID }).SingleOrDefault();
            workOrderModel model = new workOrderModel() { pj = pj, dp = dp, pc = pc };
            maintainOrderModel order_model = new maintainOrderModel()
            {
                maintainOrderID = order.maintainOrderID,
                maintainOrderNum = order.maintainOrderNum,
                appointOrderNum = "",
                carNum = carNum,
                customerName = customerName,
                customerPhone = phone,
                createTime = order.createTime,
                createStaff = staffNum,
                faultDescript = order.faultDescript,
                expectedPj = order.expectedProjects,
                expectedPrice = order.expectedPrice,
                expectedTime = order.expectedPickTime,
                model = model
            };
            return Json(order_model, JsonRequestBehavior.AllowGet);
        }

        //选择零件
        public JsonResult PartSelect(long id)
        {
            CheckConnect();
            string query = "SELECT * FROM MAINTAIN_PART WHERE classID = @Id";
            List<part> p = (List<part>)db.Query<part>(query, new { Id = id });
            return Json(p, JsonRequestBehavior.AllowGet);
        }

        //派工结果
        public JsonResult AddWorkOrderResult(string orders, long id)
        {
            CheckConnect();
            int year1 = DateTime.Now.Year;
            int month1 = DateTime.Now.Month;
            int day1 = DateTime.Now.Day;
            string year2 = "", month2 = "", day2 = "";
            if (month1 < 10) {
                month2 += "0";
            }
            month2 += month1.ToString();

            if (day1 < 10)
            {
                day2 += "0";
            }
            day2 += day1.ToString();
            year2 = year1.ToString();
            string now = year2 + month2 + day2;
            string query = "SELECT workOrderNum FROM WORK_ORDER WHERE workOrderNum LIKE 'W" + now + "%' ORDER BY workOrderID DESC LIMIT 1";
            string temp = db.Query<string>(query).SingleOrDefault();
            string pre = "W" + now;
            int last;
            if (temp == null)
            {
                last = 0;
            }
            else
            {
                string num = temp.Substring(temp.Length - 3);
                last = int.Parse(num);
            }
            List<projectJsonModel> pjs = JsonConvert.DeserializeObject<List<projectJsonModel>>(orders);
            string sql1 = "INSERT INTO WORK_ORDER(workOrderNum, maintainOrderID, departID, maintainProjectID, expectedTime, actualTime, assignTime) VALUES(@Num, @Mid, @Did, @Pid, @Etime, @Actualtime, @Assigntime)";
            string sql2 = "INSERT INTO PART_RECEIVE(workOrderID, partID, partQuantity, partEXID) VALUES(@WId, @PId, @PQ, @Exid)";
            string query2 = "SELECT workOrderID FROM WORK_ORDER WHERE workOrderNum = @Num";
            long woId = 0;
            int result = 0;
            foreach (projectJsonModel item in pjs)
            {
                string num = "";
                last += 1;
                if (last < 10) num += "00";
                else if (last < 100) num += "0";
                num += last.ToString();
                result = db.Execute(sql1, new
                {
                    Num = pre + num,
                    Mid = id,
                    Did = item.dpId,
                    Pid = item.pjId,
                    Etime = item.time,
                    Actualtime = DateTime.Now.ToString(),
                    Assigntime = DateTime.Now.ToString()
                });
                woId = db.Query<long>(query2, new { Num = (pre + num) }).SingleOrDefault();
                foreach (partJsonModel part in item.parts)
                {
                    result = db.Execute(sql2, new { WId = woId, PId = part.partId, PQ = part.amount, Exid = 0 });
                }
            }
            string set = "UPDATE MAINTAIN_ORDER SET orderState = 2 WHERE maintainOrderID = @Id";
            result = db.Execute(set, new { Id = id });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //派工单管理
        public ViewResult WorkorderIndex()
        {
            return View();
        }

        //加载派工单
        public JsonResult LoadWorkOrders(int page = 1, int limit = 10, int code = 0)
        {
            CheckConnect();
            table<workOrderTableModel> data = new table<workOrderTableModel>();
            string query;
            int count = 1000;
            string querycount = "SELECT COUNT(*) FROM WORK_ORDER";
            count = db.Query<int>(querycount).SingleOrDefault();
            List<workOrderTableModel> orders;
            if (code == 0)
            {
                query = "SELECT W.workOrderID as workOrderID,W.workOrderNum as workOrderNum,"+
                        "M.maintainOrderNum as maintainOrderNum,D.departNum as departNum,"+
                        "W.expectedTime as expectedTime,W.assignTime as assignTime,"+
                        "W.workOrderState as workOrderState"+
                        " FROM WORK_ORDER as W"+
                        " INNER JOIN MAINTAIN_ORDER AS M"+
                        " ON W.maintainOrderID = M.maintainOrderID"+
                        " INNER JOIN DEPART_BASE AS D"+
                        " ON W.departID = D.departID"+
                        " LIMIT " + 
                        ((page - 1) * limit).ToString() + "," + limit.ToString();
                
            }
            else
            {
                query = "SELECT W.workOrderID as workOrderID,W.workOrderNum as workOrderNum," +
                       "M.maintainOrderNum as maintainOrderNum,D.departNum as departNum," +
                       "W.expectedTime as expectedTime,W.assignTime as assignTime," +
                       "W.workOrderState as workOrderState" +
                       " FROM WORK_ORDER as W" +
                       " INNER JOIN MAINTAIN_ORDER AS M" +
                       " ON W.maintainOrderID = M.maintainOrderID" +
                       " INNER JOIN DEPART_BASE AS D" +
                       " ON W.departID = D.departID" +
                       " WHERE W.workOrderState = "+
                       code.ToString()+
                       " LIMIT " +
                       ((page - 1) * limit).ToString() + "," + limit.ToString();
            }
            orders = (List<workOrderTableModel>)db.Query<workOrderTableModel>(query);
            data.data = orders;
            data.count = count;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //编辑派工单
        public ViewResult EditWorkorder(long id)
        {
            ViewBag.id = id;
            return View(ViewBag.id);
        }

        //加载要编辑的派工单信息
        public JsonResult LoadEditWorkOrder(long id)
        {
            string query = "SELECT W.workOrderID as workOrderID,W.workOrderNum as workOrderNum," +
                   "M.maintainOrderNum as maintainOrderNum,D.departNum as departNum,PJ.projectName as maintainProjectName," +
                   "W.expectedTime as expectedTime,W.actualTime as actualTime,W.assignTime as assignTime," +
                   "PJ.workTime as exWorkTime,W.workTime as acWorkTime,W.workOrderState as workOrderState" +
                   " FROM WORK_ORDER as W" +
                   " INNER JOIN MAINTAIN_ORDER AS M" +
                   " ON W.maintainOrderID = M.maintainOrderID" +
                   " INNER JOIN DEPART_BASE AS D" +
                   " ON W.departID = D.departID" +
                   " INNER JOIN MAINTAIN_PROJECT AS PJ"+
                   " ON W.maintainProjectID = PJ.projectID"+
                   " WHERE W.workOrderID = @Id";
            workOrderEditModel model = db.Query<workOrderEditModel>(query, new { Id = id }).SingleOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //编辑派工单结果
        public JsonResult EditWorkOrderResult(string finishTime,float workTime,string staff,int state,long id)
        {
            string sql = "UPDATE WORK_ORDER SET actualTime = @FinishTime, workTime = @WorkTime, maintainStaff = @Staff, " +
                        "workOrderState = @State WHERE workOrderID = @Id";
            int result = db.Execute(sql, new
            {
                FinishTime = finishTime,
                WorkTime = workTime,
                Staff = staff,
                State = state,
                Id = id
            });
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        //打印零件领取单
        public ViewResult PrintOrder(long id)
        {
            ViewBag.id = id;
            return View(ViewBag.id);
        }

        //要打印的零件领取单
        public JsonResult Order2Print(long id, int code = 0)
        {
            CheckConnect();
            string query = "SELECT PR.partReceiveID as partReceiveID,W.workOrderNum as workOrderNum,P.partNum as partNum,PR.partQuantity as partQuantity,PR.partEXID as partEXID" +
                           " FROM PART_RECEIVE AS PR" +
                           " INNER JOIN WORK_ORDER AS W" +
                           " ON PR.workOrderID = W.workOrderID" +
                           " INNER JOIN MAINTAIN_PART AS P" +
                           " ON PR.partID = P.partID" +
                           " WHERE PR.workOrderID = @Id";
            int count = 0;
            table<partReceive> data = new table<partReceive>();
            List<partReceive> parts = (List<partReceive>)db.Query<partReceive>(query, new { Id = id });
            count = parts.Count();
            data.data = parts;
            data.count = count;
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        //工单结算
        public ViewResult SettleOrder(long id)
        {
            ViewBag.id = id;
            return View(ViewBag.id);
        }

        //加载结算单信息
        public JsonResult LoadSettleOrder(long id)
        {
            CheckConnect();
            settleOrderModel model = new settleOrderModel();
            string query = "SELECT * FROM MAINTAIN_ORDER WHERE maintainOrderID = @Id";
            maintainOrder Morder = db.Query<maintainOrder>(query, new { Id = id }).SingleOrDefault();
            string query2 = "SELECT * FROM CUSTOMER_CAR_BASE WHERE customerCarID = @Id";
            customerCar Car = db.Query<customerCar>(query2, new { Id = Morder.customerCarID }).SingleOrDefault();
            string query3 = "SELECT * FROM CUSTOMER WHERE customerID = @Id";
            Customer Cus = db.Query<Customer>(query3, new { Id = Car.customerID }).SingleOrDefault();
            model.customerName = Cus.customerName;
            model.phone = Cus.customerPhone;
            model.address = Cus.customerAddress;
            model.carBrand = Car.carBrand;
            model.carType = Car.carType;
            model.engineNum = Car.engineNum;
            model.licenseNum = Car.licenseNum;
            model.startTime = Morder.createTime;
            model.finishTime = Morder.finishTime;
            string query4 = "SELECT W.workOrderID as workOrderID,PJ.projectName as projectName,W.workTime as workTime,D.departNum as departNum" +
                            " FROM WORK_ORDER AS W" +
                            " INNER JOIN MAINTAIN_PROJECT AS PJ" +
                            " ON W.maintainProjectID = PJ.projectID" +
                            " INNER JOIN DEPART_BASE AS D" +
                            " ON W.departID = D.departID" +
                            " WHERE W.maintainOrderID = @Id";
            List<workOrderSettleModel> Wmodel = (List<workOrderSettleModel>)db.Query<workOrderSettleModel>(query4, new { Id = id });
            foreach(workOrderSettleModel item in Wmodel)
            {
                string q = "SELECT MP.partName AS partName,P.partQuantity AS partAmount,MP.partPrice* P.partQuantity AS partPrice" +
                           " FROM PART_RECEIVE AS P" +
                           " INNER JOIN MAINTAIN_PART AS MP" +
                           " ON P.partID = MP.partID" +
                           " WHERE P.workOrderID = @Id";
                List<partSettleModel> parts = (List<partSettleModel>)db.Query<partSettleModel>(q, new { Id = item.workOrderID });
                item.parts = parts;
            }
            model.orders = Wmodel;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public void CheckConnect()
        {
            if (db.State == ConnectionState.Open) db.Close();
            db.Open();
        }
    }
}