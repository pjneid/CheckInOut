using System;
using System.Collections.Generic;

namespace CheckInOut.DAL.Models
{
    public class Company
    {
        //public Company()
        //{
        //    Branches= new List<Branch>();
        //    ConfigurationEmails = new List<ConfigurationEmail>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RouteData { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<ConfigurationEmail> ConfigurationEmails { get; set; }
    }
}
