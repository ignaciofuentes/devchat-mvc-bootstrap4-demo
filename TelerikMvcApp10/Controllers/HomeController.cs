using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikMvcApp10.Models;
using Kendo.Mvc.Extensions;

namespace TelerikMvcApp10.Controllers
{
    public class CarViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public int InStock { get; set; }

        public bool Discontinued { get; set; }
    }
    public class HomeController : Controller
    {

        Database db;
        public HomeController() {
            db = new Database();

        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        // GET: Home
        public ActionResult Controls()
        {
            return View();
        }
        // GET: Home
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Cars_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json((from c in db.Cars
                         select new CarViewModel
                         {
                             Id = c.Id,
                             Name = c.Name,
                             Category=c.Category,
                             Discontinued = c.Discontinued,
                             InStock= c.InStock
                         }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Cars_Create([DataSourceRequest] DataSourceRequest request, CarViewModel car)
        {
            var results = new List<CarViewModel>();
            if (car != null && ModelState.IsValid)
            {
                var newCar = new Car { Name = car.Name, Category=car.Category, Discontinued = car.Discontinued, InStock=car.InStock };
                db.Cars.Add(newCar);
                db.SaveChanges();
                car.Id = newCar.Id;
                results.Add(car);
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Cars_Update([DataSourceRequest] DataSourceRequest request, CarViewModel car)
        {
            if (car != null && ModelState.IsValid)
            {
                var dbCar = db.Cars.Where(c => c.Id == car.Id).SingleOrDefault();
                if (dbCar != null)
                {
                    dbCar.Name = car.Name;
                    dbCar.Category = car.Category; dbCar.Discontinued = car.Discontinued;
                    dbCar.InStock = car.InStock;

                }
            }
            db.SaveChanges();
            return Json(new[] { car }.ToDataSourceResult(request, ModelState));
        }
        [HttpPost]
        public ActionResult Cars_Destroy([DataSourceRequest] DataSourceRequest request, CarViewModel car)
        {
            var dbCar = db.Cars.Where(c => c.Id == car.Id).SingleOrDefault();
            db.Cars.Remove(dbCar);
            db.SaveChanges();
            return Json(new[] { car }.ToDataSourceResult(request));
        }
    }
}