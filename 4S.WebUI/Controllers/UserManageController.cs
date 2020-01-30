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

        static IDbConnection db = DapperService.MySqlConnection();
        // GET: UserManage
        public ActionResult UserManageIndex(FormCollection form)
        {
            if (form["name"] != null)
            {
                string name = form["name"];
                string phone = form["phone"];
                string address = form["address"];
                string idcard = form["idcard"];
                Customer person = new Customer() { customerName = name, customerPhone = phone, customerAddress = address, customerPID = idcard };
                if (form["id"] != null)
                {
                    person.customerID = long.Parse(form["id"]);
                    string updatesql = " UPDATE CUSTOMER SET customerName = @Name,customerPhone= @Phone,customerAddress = @Address,customerPID = @Idcard WHERE customerID = @CustomerID";
                    db.Execute(updatesql, person);
                }else
                {
                    db.Execute("insert into CUSTOMER(customerName,customerPhone,customerAddress,customerPID) values(@Name,@Phone,@Address,@Idcard)", person);
                }
            }
            return View();
        }

        public JsonResult LoadUsers()
        {
            table data = new table();
            string query = "select * from CUSTOMER";
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
        public ViewResult EditUser(long id)
        {
            string query = "select * from CUSTOMER where customerID = @Id";
            Customer temp = (Customer)db.Query<Customer>(query, new { Id = id }).SingleOrDefault();
            return View(temp);
        }

        //删除用户
       /* public JsonResult deleteuser(Guid id)
        {
            Customer person = new Customer() { CustomerID = id };
            int result = db.Execute("delete from Customers where CustomerID=@CustomerID", person);
            if (result != 0)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(0,JsonRequestBehavior.AllowGet);
        }*/
    }
}