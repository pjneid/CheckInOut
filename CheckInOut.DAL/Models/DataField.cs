using System.Collections.Generic;

namespace CheckInOut.DAL.Models
{
    public class DataField
    {
        //public DataField()
        //{
        //    DeviceDataFields = new List<DeviceVisitorTypeDataField>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string HtmlInputType { get; set; }

        public virtual ICollection<DeviceVisitorTypeDataField> DeviceDataFields { get; set; }
    }
}