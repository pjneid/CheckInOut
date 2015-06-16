namespace CheckInOut.DAL.Models
{
    public class DeviceVisitorTypeDataField
    {
        public int Id { get; set; }
        public int DeviceVisitorTypeId { get; set; }
        public int DataFieldId { get; set; }
        public bool IsActive { get; set; }
        public bool IsRequired { get; set; }
        public int OrderNo { get; set; }

        public virtual DeviceVisitorType DeviceVisitorType { get; set; }
        public virtual DataField DataField { get; set; }
    }
}
