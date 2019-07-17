using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4S.Domain.Abstract;
using _4S.Domain.Entities;

namespace _4S.WebUI.Controllers
{
    public class CustomerController : Controller
    {

        private ICustomersRepository repository;

        public CustomerController(ICustomersRepository repo)
        {
            repository = repo;
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List()
        {
            return View(repository.Customers);
        }
    }
}