using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CheckInOut.DAL.Mappers;
using CheckInOut.DAL.ViewModels;
using CheckInOut.Display.Helpers;

namespace CheckInOut.Display.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var deviceVisitorTypes = _context.DeviceVisitorTypes.Where(s => s.DeviceId == SettingsManager.DeviceId && s.IsActive).ToList();
            var model = deviceVisitorTypes.Select(e => new VmDeviceVisitorType().MapFromDeviceVisitorTypes(e)).ToList();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}