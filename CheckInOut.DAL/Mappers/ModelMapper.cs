using CheckInOut.DAL.Models;
using CheckInOut.DAL.ViewModels;

namespace CheckInOut.DAL.Mappers
{
    public static class ModelMapper
    {
        public static VmDeviceVisitorType MapFromDeviceVisitorTypes(this VmDeviceVisitorType vmVisitorType,
            DeviceVisitorType deviceVisitorType)
        {
            vmVisitorType.Id = deviceVisitorType.Id;
            vmVisitorType.Display = deviceVisitorType.Display;
            vmVisitorType.OrderNo = deviceVisitorType.OrderNo;
            return vmVisitorType;
        }

        public static VmDeviceVisitorTypeDataField MapFromDeviceVisitorTypeDataFields(
            this VmDeviceVisitorTypeDataField vmDeviceVisitorTypeDataField,
            DeviceVisitorTypeDataField deviceVisitorTypeDataField)
        {
            vmDeviceVisitorTypeDataField.Id = deviceVisitorTypeDataField.Id;
            vmDeviceVisitorTypeDataField.DataFieldName = deviceVisitorTypeDataField.DataField.Name;
            vmDeviceVisitorTypeDataField.VisitorTypeName = deviceVisitorTypeDataField.DeviceVisitorType.VisitorType.Name;
            vmDeviceVisitorTypeDataField.HtmlInputType = deviceVisitorTypeDataField.DataField.HtmlInputType;
            vmDeviceVisitorTypeDataField.IsRequired = (deviceVisitorTypeDataField.IsRequired ? 1 : 0);
            vmDeviceVisitorTypeDataField.OrderNo = deviceVisitorTypeDataField.OrderNo;
            return vmDeviceVisitorTypeDataField;
        }

        public static VmEmployee MapFromEmployee(this VmEmployee vmEmployee, Employee employee)
        {
            vmEmployee.Id = employee.Id;
            vmEmployee.Name = employee.Name;
            vmEmployee.Email = employee.Email;
            vmEmployee.Mobile = employee.Mobile;
            return vmEmployee;
        }

        public static VmSignOut MapFromTrxVisit(this VmSignOut vmVisit, TrxVisit trxVisit)
        {
            string name;
            string code;
            string company;
            if (trxVisit.Visitor == null)
            {
                name = trxVisit.Employee.Name;
                code = trxVisit.Employee.AccessNumber;
                company = trxVisit.Employee.Branch.Company.Name;
            }
            else
            {
                name = trxVisit.Visitor.Name;
                code = trxVisit.Visitor.AccessNumber;
                company = trxVisit.Visitor.Company;
            }
            vmVisit.Id = trxVisit.Id;
            vmVisit.Name = name;
            vmVisit.Code = code;
            vmVisit.Company = company;
            return vmVisit;
        }
    }
}