using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using CheckInOut.DAL.Context;
using CheckInOut.DAL.Models;

namespace CheckInOut.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CheckInOutContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CheckInOutContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Companies.Any())
            {
                var company = new Company
                {
                    Name = "Company Name",
                    Description = "Company Description",
                    RouteData = "CRD",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                var configurationEmail = new ConfigurationEmail
                {
                    UserName = "teamsjtech@gmail.com",
                    Password = "Jtech23?",
                    SmtpClient = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Company = company,
                    CcEmails = "teamsjtech@gmail.com;teamsjtech@gmail.com"
                };

                var branch = new Branch
                {
                    Name = "Branch Name",
                    Description = "Branch Description",
                    Address = "Branch Address",
                    Email = "Branch Email",
                    Phone = "Branch Phone",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    Company = company
                };

                var device = new Device
                {
                    Name = "Device Name",
                    Description = "Device Description",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    Branch = branch
                };

                var visitorTypes = new List<VisitorType>
                    {
                        new VisitorType {Name = "Visitor", IsActive = true},
                        new VisitorType {Name = "Contractor", IsActive = true},
                        new VisitorType {Name = "Employee", IsActive = true}
                    };

                var dataFields = new List<DataField>
                    {
                        new DataField {Name = "Name", IsActive = true, HtmlInputType = "text"},
                        new DataField {Name = "Company", IsActive = true, HtmlInputType = "text"},
                        new DataField {Name = "Email", IsActive = true, HtmlInputType = "email"},
                        new DataField {Name = "Mobile", IsActive = true, HtmlInputType = "text"},
                    };

                context.Companies.Add(company);
                context.ConfigurationEmails.Add(configurationEmail);
                context.Branches.Add(branch);
                context.Devices.Add(device);

                var deviceVisitorTypes = new List<DeviceVisitorType>();

                foreach (var visitorType in visitorTypes)
                {
                    context.VisitorTypes.Add(visitorType);

                    deviceVisitorTypes.Add(new DeviceVisitorType
                    {
                        Device = device,
                        VisitorType = visitorType,
                        Display = visitorType.Name,
                        IsActive = true,
                        OrderNo = visitorTypes.IndexOf(visitorType) + 1
                    });
                }

                foreach (var deviceVisitorType in deviceVisitorTypes)
                {
                    foreach (var dataField in dataFields)
                    {
                        context.DataFields.Add(dataField);

                        context.DeviceVisitorTypeDataFields.Add(new DeviceVisitorTypeDataField
                        {
                            DeviceVisitorType = deviceVisitorType,
                            DataField = dataField,
                            IsActive = true,
                            IsRequired = true,
                            OrderNo = dataFields.IndexOf(dataField) + 1
                        });
                    }
                }

                context.SaveChanges();

                var updateData =
                    context.DeviceVisitorTypeDataFields.FirstOrDefault(
                        s => s.DeviceVisitorTypeId == 3 && s.DataFieldId == 2);
                if (updateData != null)
                {
                    updateData.IsActive = false;
                    context.Entry(updateData).State = EntityState.Modified;
                    context.SaveChanges();
                }

            }
        }
    }
}
