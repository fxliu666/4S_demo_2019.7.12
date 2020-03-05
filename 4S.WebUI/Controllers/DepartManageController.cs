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
    public class DepartManageController : Controller
    {
        static IDbConnection db = DapperService.MySqlConnection();
        // GET: DepartManage
        public ActionResult DepartManageIndex()
        {
            return View();
        }

        // GET: DepartManage
        public ActionResult StaffManageIndex()
        {
            return View();
        }


        //返回部门信息
        public JsonResult LoadDeparts(int page = 1, int limit = 10)
        {
            CheckConnect();
            string query = "select * from DEPART_BASE limit " + ((page - 1) * limit).ToString() + "," + limit.ToString();
            table<depart> data = new table<depart>();
            List<depart> departs = (List<depart>)db.Query<depart>(query);
            data.data = departs;
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //返回员工信息
        public JsonResult LoadStaff(int page = 1, int limit = 10)
        {
            CheckConnect();
            string query = "SELECT * FROM STAFF_BASE limit " + ((page - 1) * limit).ToString() + "," + limit.ToString();
            table<staff> data = new table<staff>();
            List<staff> staff = (List<staff>)db.Query<staff>(query);
            data.data = staff;
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //返回单个员工信息
        public JsonResult LoadSingleStaff(int id)
        {
            CheckConnect();
            string query1 = "SELECT * FROM STAFF_BASE WHERE staffID = @Id";
            string query2 = "SELECT * FROM DEPART_BASE WHERE departID = @Id";      
            string query4 = "SELECT * FROM DEPART_BASE WHERE departLevel = @Level";
            staff sta = (staff)db.Query<staff>(query1,new { Id = id }).SingleOrDefault();
            depart dep = (depart)db.Query<depart>(query2, new { Id = sta.departID }).SingleOrDefault();
            List<depart> deps = (List<depart>)db.Query<depart>(query4, new { Level = dep.departLevel });
            string query3 = "SELECT classNum,className FROM CLASS_ITEM WHERE classNum LIKE '020" + dep.departLevel.ToString() + "%'";
            List<position> pos = (List<position>)db.Query<position>(query3);
            staffInfoModel model = new staffInfoModel { sta = sta, dep = dep, deps = deps, pos = pos };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //添加部门
        public ViewResult AddDepart()
        {
            return View();
        }


        //选择部门
        public JsonResult DepartSelect(int id)
        {
            string query = "select * from DEPART_BASE where departLevel = @Level";
            table<depart> data = new table<depart>();
            List<depart> departs = (List<depart>)db.Query<depart>(query, new { Level = id });
            data.data = departs;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //选择职位
        public JsonResult PositionSelect(int id)
        {
            CheckConnect();
            string query = "SELECT * FROM CLASS_ITEM WHERE classNum LIKE '020" + id.ToString() + "%'";
            table<position> data = new table<position>();
            List<position> positions = (List<position>)db.Query<position>(query);
            data.data = positions;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //添加结果
        public JsonResult AddResult(int parentId,int level,string name)
        {
            string time = DateTime.Now.ToString();
            string id;
            string query = "SELECT * FROM DEPART_BASE WHERE parentID = @Id ORDER BY departID DESC LIMIT 1";
            depart temp = (depart)db.Query<depart>(query, new { Id = parentId }).SingleOrDefault();
            if(temp == null)
            {
                string queryparent = "SELECT * FROM DEPART_BASE WHERE departID = @Id";
                depart parent = (depart)db.Query<depart>(queryparent, new { Id = parentId }).SingleOrDefault();
                id = parent.departNum + "01";

            }
            else
            {
                string str = temp.departNum;
                string num = str.Substring(str.Length - 2);
                int id1 = int.Parse(num) + 1;
                if (id1 < 10) id = str.Substring(0, str.Length - 2) + "0" + id1.ToString();
                else id = str.Substring(0, str.Length - 2) + id1.ToString();
            }       
            string sql = "INSERT INTO DEPART_BASE(departNum,departName,departLevel,parentID,createTime,createStaffID) VALUES(@Num,@Name,@Level,@ParentId,@CreateTime,@CreateStaffId)";
            int result = db.Execute(sql,new {Num = id, Name = name, Level = level+1, ParentId = parentId, CreateTime = time, CreateStaffId = 1});
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        //添加员工
        public ViewResult AddStaff()
        {
            return View();
        }
        //添加员工结果
        public JsonResult AddStaffResult(string staffName,string passwd,string phone,long departId,string position)
        {
            CheckConnect();
            string query = "SELECT * FROM STAFF_BASE ORDER BY staffID DESC LIMIT 1";
            staff temp = (staff)db.Query<staff>(query).SingleOrDefault();
            string staffNum = "";
            if (temp == null)
            {
                staffNum = "E01001";
            }
            else
            {
                int num = int.Parse(temp.staffNum.Substring(temp.staffNum.Length - 3))+1;
                if (num < 10)
                {
                    staffNum += "E0100";
                }
                else if (num < 100)
                {
                    staffNum += "E010";
                }
                else staffNum += "E01";
                staffNum += num.ToString();
            }
           
            
            string sql = "INSERT INTO STAFF_BASE(staffNum,staffName,departID,staffPosition,staffAccount,staffPasswd,staffPhone) VALUES(@Num,@Name,@Id,@Position,@Account,@Passwd,@Phone)";
            int result = db.Execute(sql, new { Num = staffNum, Name = staffName, Id = departId, Position = position, Account = "", Passwd = passwd, Phone = phone });
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        //编辑员工信息
        public ViewResult EditStaff(long id)
        {
            ViewBag.id = id;
            return View(ViewBag.id);
        }

        //修改员工信息
        public JsonResult UpdateStaffResult(long id, string name, long departid, string position, 
            string account, string passwd, string phone)
        {
            CheckConnect();
            int result = 0;
            string set = "UPDATE STAFF_BASE SET staffName = @Name, departID = @Departid, staffPosition ="+
                          "@Position, staffAccount = @Account, staffPasswd = @Passwd, staffPhone ="+
                          "@Phone where staffID = @Id";           
            result = db.Execute(set, new { Name = name, Departid = departid, Position = position,
                                            Account = account, Passwd = passwd, Phone = phone, Id = id});
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void CheckConnect()
        {
            if (db.State == ConnectionState.Open) db.Close();
            db.Open();
        }

    }
}