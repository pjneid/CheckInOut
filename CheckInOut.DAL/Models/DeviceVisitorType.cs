using System.Collections.Generic;

namespace CheckInOut.DAL.Models
{
    public class DeviceVisitorType
    {
        //public DeviceVisitorType()
        //{
        //    DeviceVisitorTypeDataFields = new List<DeviceVisitorTypeDataField>();
        //}

        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int VisitorTypeId { get; set; }
        public bool IsActive { get; set; }
        public string Display { get; set; }
        public int OrderNo { get; set; }

        public virtual Device Device { get; set; }
        public virtual VisitorType VisitorType { get; set; }

        public virtual ICollection<DeviceVisitorTypeDataField> DeviceVisitorTypeDataFields { get; set; } 
    }
}
