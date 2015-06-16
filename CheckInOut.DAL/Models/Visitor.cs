using System;
using System.Collections.Generic;

namespace CheckInOut.DAL.Models
{
    public class Visitor
    {
        //public Visitor()
        //{
        //    TrxVisits = new List<TrxVisit>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AccessNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int VisitorTypeId { get; set; }
        public int CompanyId { get; set; }


        
        public virtual VisitorType VisitorType { get; set; }
        public virtual Company VisitingCompany { get; set; }

        public virtual ICollection<TrxVisit> TrxVisits { get; set; }
    }
}