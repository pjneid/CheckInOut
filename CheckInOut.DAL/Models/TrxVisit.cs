using System;

namespace CheckInOut.DAL.Models
{
    public class TrxVisit
    {
        public int Id { get; set; }
        public bool SignedIn { get; set; }
        public bool? SignedOut { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DeviceInId { get; set; }
        public int? DeviceOutId { get; set; }
        public int? VisitorId { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Visitor Visitor { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
