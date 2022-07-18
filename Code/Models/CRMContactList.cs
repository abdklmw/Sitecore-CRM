using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Feature.SitecoreCRM.Models
{
    public class CRMContactList
    {
        public Item DataSource { get; set; }
        public List<CRMContact> Contacts { get; set; }

        public CRMContactList()
        {
            this.Contacts = new List<CRMContact>();
        }
    }
}