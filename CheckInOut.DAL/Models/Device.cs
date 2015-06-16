using System;
using System.Collections.Generic;

namespace CheckInOut.DAL.Models
{
    public class Device
    {
        //public Device()
        //{
        //    DeviceVisitorTypes = new List<DeviceVisitorType>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ICollection<DeviceVisitorType> DeviceVisitorTypes { get; set; }
    }
}