using System;
using System.Collections.Generic;

namespace CheckInOut.DAL.Models
{
    public class Branch
    {
        //public Branch ()
        //{
        //    Devices = new List<Device>();
        //    Employees = new List<Employee>();
        //}
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
