using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRM.BOL
{
    public class OrganisationBOL
    {

        public int CompanyID { get; set; } // Primary Key

        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string RegistrationNo { get; set; }
        public string VAT { get; set; }
        public string CST { get; set; }
        public string PANNO { get; set; }
        public string TIN { get; set; }
        public string ESI { get; set; }
        public string PF { get; set; }
        public string DateFormat { get; set; }
        public string ContactName { get; set; }
        public string LogoName { get; set; }
        public string Status { get; set; }

        public int CountryID { get; set; }
        public int EmployeeCount { get; set; }
    }
}
