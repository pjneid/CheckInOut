namespace CheckInOut.DAL.ViewModels
{
    public class VmSignIn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int VisitorTypeId { get; set; }
        public int DeviceId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
