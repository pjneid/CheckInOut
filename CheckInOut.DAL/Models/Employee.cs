using System;
using System.Collections.Generic;

namespace CheckInOut.DAL.Models
{
    public class Employee
    {
        //public Employee()
        //{
        //    TrxVisits = new List<TrxVisit>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AccessNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ICollection<TrxVisit> TrxVisits { get; set; }
    }
}