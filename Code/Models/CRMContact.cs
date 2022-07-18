using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Feature.SitecoreCRM.Models
{
    public class CRMContact
    {
        public ID Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public CRMContact()
        {

        }

        public CRMContact(Item item)
        {
            this.Id = item?.ID;
            this.LastName = item?.Fields["Last Name"]?.Value;
            this.FirstName = item?.Fields["First Name"]?.Value;
            this.Email = item?.Fields["Email"]?.Value;
            this.Phone = item?.Fields["Phone"]?.Value;
            this.Address = item?.Fields["Address"]?.Value;
        }
    }
}