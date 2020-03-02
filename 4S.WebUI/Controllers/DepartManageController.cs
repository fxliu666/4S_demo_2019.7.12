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

        //添加部门
        public ViewResult AddDepart()
        {
            return View();
        }


        //选择部门
        public JsonResult DepartSelect(int id)
        {
            CheckConnect();
            string query = "select * from DEPART_BASE where departLevel = @Level";
            table<depart> data = new table<depart>();
            List<depart> departs = (List<depart>)db.Query<depart>(query, new { Level = id });
            data.data = departs;
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
        public void CheckConnect()
        {
            if (db.State == ConnectionState.Open) db.Close();
            db.Open();
        }

    }
}