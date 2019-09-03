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
    public class UserManageController : Controller
    {
        static IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString);
        // GET: UserManage
        public ActionResult UserManageIndex(FormCollection form)
        {
            if (form["name"] != null)
            {
                string name = form["name"];
                string phone = form["phone"];
                string address = form["address"];
                string idcard = form["idcard"];
                Customer person = new Customer() { Name = name, Phone = phone, Address = address, UIdentity = idcard };
                if (form["id"] != null)
                {
                    Guid temp = new Guid(form["id"]);
                    person.CustomerID = temp;
                    string updatesql = " UPDATE Customers SET Name = @Name,Phone= @Phone,Address = @Address,UIdentity = @UIdentity WHERE CustomerID = @CustomerID";
                    db.Execute(updatesql, person);
                }else
                {
                    db.Execute("insert into Customers(Name,Phone,Address,UIdentity) values(@Name,@Phone,@Address,@UIdentity)", person);
                }
            }
            return View();
        }

        public JsonResult LoadUsers()
        {
            table data = new table();
            string query = "select * from Customers";
            List<Customer> customers = (List<Customer>)db.Query<Customer>(query);
            data.data = customers;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //添加用户
        public ViewResult AddUser()
        {
            return View();
        }


        //编辑用户
        public ViewResult EditUser(Guid id)
        {
            string query = "select * from Customers where CustomerID = @Id";
            Customer temp = (Customer)db.Query<Customer>(query, new { Id = id }).SingleOrDefault();
            return View(temp);
        }

        //删除用户
        public JsonResult deleteuser(Guid id)
        {
            Customer person = new Customer() { CustomerID = id };
            int result = db.Execute("delete from Customers where CustomerID=@CustomerID", person);
            if (result != 0)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0,JsonRequestBehavior.AllowGet);
        }
    }
}