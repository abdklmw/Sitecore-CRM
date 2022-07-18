using Project.Feature.SitecoreCRM.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Project.Feature.SitecoreCRM.Controllers
{
    public class SitecoreCRMController : Controller
    {
        private ID crmContactTemplateId = new ID(Sitecore.Configuration.Settings.GetSetting("SitecoreCRM.CrmContactTemplateId"));
        // GET: SitecoreCRM
        public ActionResult Index()
        {
            var dataSource = RenderingContext.Current.Rendering.DataSource;
            if (string.IsNullOrWhiteSpace(dataSource)) return null;
            var contacts = new CRMContactList();

            //get contacts folder
            var contactsFolder = Sitecore.Context.Database.GetItem(dataSource);
            //get contacts
            var children = contactsFolder.GetChildren().ToList();
            foreach (Item contactItem in children
                .Where(t => t.TemplateID.Equals(crmContactTemplateId)))
            {
                contacts.Contacts.Add(new CRMContact(contactItem));
            }

            contacts.DataSource = contactsFolder;

            return View("List", contacts);
        }
    }
}