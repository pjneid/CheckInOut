using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using CheckInOut.DAL.Context;
using CheckInOut.DAL.Helpers;
using CheckInOut.DAL.Mappers;
using CheckInOut.DAL.Models;
using CheckInOut.DAL.ViewModels;
using CheckInOut.Display.Helpers;

namespace CheckInOut.Display.Controllers
{
    public class HomeController : Controller
    {
        private readonly CheckInOutContext _context = new CheckInOutContext();
        public ActionResult Index()
        {
            var deviceVisitorTypes = _context.DeviceVisitorTypes.Where(s => s.DeviceId == SettingsManager.DeviceId && s.IsActive).ToList();
            var model = deviceVisitorTypes.Select(e => new VmDeviceVisitorType().MapFromDeviceVisitorTypes(e)).ToList();

            return View(model);
        }

        public ActionResult SignIn(int deviceVisitorTypeId = -1)
        {
            if (deviceVisitorTypeId == -1)
                return RedirectToAction("Index");

            var deviceVisitorTypeDataFields =
                _context.DeviceVisitorTypeDataFields.Where(s => s.DeviceVisitorTypeId == deviceVisitorTypeId && s.IsActive)
                    .ToList();

            var model = deviceVisitorTypeDataFields.Select(
                    e => new VmDeviceVisitorTypeDataField().MapFromDeviceVisitorTypeDataFields(e)).ToList();

            ViewData["VisitorTypeId"] = deviceVisitorTypeId;
            return View(model);
        }

        public ActionResult ThankYou(FormCollection frmCollection)
        {
            if (frmCollection.Count == 0)
                return RedirectToAction("Index");

            var vmSignIn = new VmSignIn
            {
                Name = frmCollection["txtName"],
                Company = frmCollection["txtCompany"],
                Email = frmCollection["txtEmail"],
                Mobile = frmCollection["txtMobile"],
                VisitorTypeId = Convert.ToInt32(frmCollection["txtVisitorType"]),
                DeviceId = SettingsManager.DeviceId,
                EmployeeId =
                    (frmCollection["txtSelectEmployeeId"].Equals("-1")
                        ? (int?)null
                        : Convert.ToInt32(frmCollection["txtSelectEmployeeId"]))
            };

            Utils utils = new Utils();
            string accessNumber;
            if (vmSignIn.VisitorTypeId.Equals(3))// Employee
            {
                int branchId = _context.Devices.FirstOrDefault(s => s.Id == vmSignIn.DeviceId).Branch.Id;

                var employee = new Employee
                {
                    Name = vmSignIn.Name,
                    Email = vmSignIn.Email,
                    Mobile = vmSignIn.Mobile,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    BranchId = branchId,
                    AccessNumber = utils.GetEmployeeAccessNumber(branchId)
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();

                SignInTransaction(vmSignIn.DeviceId, null, employee.Id);

                accessNumber = employee.AccessNumber;
            }
            else
            {
                int companyId = _context.Devices.FirstOrDefault(s => s.Id == vmSignIn.DeviceId).Branch.CompanyId;
                //Visitor or Contractor
                var visitor = new Visitor
                {
                    Name = vmSignIn.Name,
                    Company = vmSignIn.Company,
                    Email = vmSignIn.Email,
                    Mobile = vmSignIn.Mobile,
                    AccessNumber = utils.GetVisitorAccessNumber(companyId),
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    VisitorTypeId = vmSignIn.VisitorTypeId,
                    CompanyId = companyId
                };

                _context.Visitors.Add(visitor);
                _context.SaveChanges();

                SignInTransaction(vmSignIn.DeviceId, visitor.Id, vmSignIn.EmployeeId);

                if (vmSignIn.EmployeeId != null)
                {
                    string toEmail = _context.Employees.FirstOrDefault(s => s.Id == vmSignIn.EmployeeId).Email;
                    //int companyId = _db.Devices.FirstOrDefault(s => s.Id == vmSignIn.DeviceId).Branch.CompanyId;
                    if (!toEmail.Equals(""))
                    {
                        //_utils.SignInMail(toEmail, "Reception Notice",
                        //visitor.Name + " has arrived at reception", companyId);

                        ThreadStart threadStart = delegate
                        {
                            utils.SignInMail(toEmail, "Reception Notice",
                                visitor.Name + " has arrived at reception", companyId);
                        };
                        Thread thread = new Thread(threadStart);
                        thread.Start();
                    }
                }
               accessNumber= visitor.AccessNumber;
            }

            ViewData["AccessNumber"] = accessNumber;
            return View();
        }

        private void SignInTransaction(int deviceInId, int? visitorId, int? employeeId)
        {
            var transaction = new TrxVisit
            {
                SignedIn = true,
                SignedOut = null,
                TimeIn = DateTime.Now,
                TimeOut = null,
                IsActive = true,
                CreatedDate = DateTime.Now,
                DeviceInId = deviceInId,
                DeviceOutId = null,
                VisitorId = visitorId,
                EmployeeId = employeeId
            };

            _context.TrxVisits.Add(transaction);
            _context.SaveChanges();

        }

        public ActionResult SignOut()
        {
            return View();
        }

        public ActionResult GetSignedIn()
        {
            var deviceIds =
                    _context.Devices.FirstOrDefault(s => s.Id == SettingsManager.DeviceId).Branch.Devices.Select(s => s.Id).ToArray();

            var vignedInVisits =
                _context.TrxVisits.Include("Visitor").Include("Employee")
                    .Where(s => s.SignedOut == null && deviceIds.Contains(s.DeviceInId))
                    .ToList();

            var personsViewModel = vignedInVisits.Select(e => new VmSignOut().MapFromTrxVisit(e)).ToList();


            return Json(personsViewModel);
        }

        public ActionResult GetEmployees()
        {
            int branchId = _context.Devices.FirstOrDefault(s => s.Id == SettingsManager.DeviceId).Branch.Id;

            var employees =
                _context.Employees.Where(s => s.BranchId == branchId && s.IsActive).OrderBy(s => s.Name).ToList();
            var lstEmployees = employees.Select(e => new VmEmployee().MapFromEmployee(e)).ToList();


            return Json(lstEmployees);
        }

        public ActionResult GetEmployeeByCode(string code)
        {
            var isEmployee =
                     _context.Employees.FirstOrDefault(s => s.AccessNumber == code && s.IsActive);

            VmEmployee employee = null;

            if (isEmployee != null)
                employee= new VmEmployee()
                {
                    Id = isEmployee.Id,
                    Name = isEmployee.Name,
                    Email = isEmployee.Email,
                    Mobile = isEmployee.Mobile,
                    AccessNumber = isEmployee.AccessNumber
                };


            return Json(employee);
        }

        public ActionResult GetVisitorByCode(string code)
        {
            var isVisitor =
                    _context.Visitors.FirstOrDefault(s => s.AccessNumber == code && s.IsActive);

            VmVisitor visitor = null;
            if (isVisitor != null)
                visitor= new VmVisitor()
                {
                    Id = isVisitor.Id,
                    Name = isVisitor.Name,
                    Email = isVisitor.Email,
                    Mobile = isVisitor.Mobile,
                    AccessNumber = isVisitor.AccessNumber,
                    Company = isVisitor.Company,
                    VisitorType = isVisitor.VisitorType.Name
                };

            return Json(visitor);
        }

        public ActionResult SignOutVisitor(int visitId)
        {
            TrxVisit trxVisit = _context.TrxVisits.Find(visitId);

            trxVisit.SignedOut = true;
            trxVisit.TimeOut = DateTime.Now;
            trxVisit.DeviceOutId = SettingsManager.DeviceId;

            _context.Entry(trxVisit).State = EntityState.Modified;

            bool isDone;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrxVisitExists(visitId))
                {
                    isDone= false;
                }
                throw;
            }

            return Json("done");
        }

        private bool TrxVisitExists(int id)
        {
            return _context.TrxVisits.Count(e => e.Id == id) > 0;
        }
    }
}