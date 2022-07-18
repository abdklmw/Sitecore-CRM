using System.Linq;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using static System.FormattableString;
using Sitecore.Configuration;
using Sitecore.Security.Accounts;
using System.Web.Security;
using System;
using Sitecore.Data.Items;
using Sitecore.Data;

namespace Project.Feature.SitecoreCRM
{
    public class CustomSubmitAction : SubmitActionBase<string>
    {
        public CustomSubmitAction(ISubmitActionData submitActionData) : base(submitActionData)
        {
        }

        public void AddContact(string firstName, string lastName, string email, string phone, string address)
        {
            try
            {
                //Disabling security to add items to Sitecore without login
                using (new Sitecore.SecurityModel.SecurityDisabler())
                {
                    var CrmContactsFolderId = new ID(Sitecore.Configuration.Settings.GetSetting("SitecoreCRM.CrmContactsFolder"));
                    var crmContactTemplateId = new ID(Sitecore.Configuration.Settings.GetSetting("SitecoreCRM.CrmContactTemplateId"));

                    // Get the master database
                    Sitecore.Data.Database masterDb = Sitecore.Data.Database.GetDatabase("master");
                    //get contacts folder
                    var contactsRootFolder = masterDb.GetItem(CrmContactsFolderId);
                    //get template
                    TemplateItem contactTemplate = masterDb.GetItem(crmContactTemplateId);
                    var tempName = new Guid().ToString().Replace("{", "").Replace("}", "");
                    //add new contact with a temporary name of a new ID
                    Item newContactItem = contactsRootFolder.Add(tempName, contactTemplate);

                    //create a new name for the item
                    var contactItemName = $"{lastName.ToLower()}-{firstName.ToLower()}";
                    //check if item already exists with this name
                    if (masterDb.GetItem($"{contactsRootFolder.Paths.Path}\\{contactItemName}") != null)
                    {
                        //if an existing item already exists by this name, let's add a number and check if it exists, up until 100
                        for (int i = 1; i <= 100; i++)
                        {
                            if (masterDb.GetItem($"{contactsRootFolder.Paths.Path}\\{contactItemName}-{i}") == null)
                            {
                                contactItemName += $"-{i}";
                                break;
                            }
                        }
                    }

                    //begin editting the new contact item
                    newContactItem.Editing.BeginEdit();
                    //rename the item
                    newContactItem.Name = contactItemName;
                    try
                    {
                        //ordinarily I would avoid using "magic strings" but in the interest of time, I am here
                        newContactItem.Fields["Last Name"].Value = lastName;
                        newContactItem.Fields["First Name"].Value = firstName;
                        newContactItem.Fields["Email"].Value = email;
                        newContactItem.Fields["Phone"].Value = phone;
                        newContactItem.Fields["Address"].Value = address;

                        //write the edits
                        newContactItem.Editing.EndEdit();
                    }
                    catch (System.Exception ex)
                    {
                        //if error occurred, let's log it
                        Log.Error("Could not update item " + newContactItem.Paths.FullPath + ": " + ex.Message, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Addition of contact failed: " + ex.Message, this);
            }
        }

        protected override bool TryParse(string value, out string target)
        {
            target = string.Empty;
            return true;
        }

        protected override bool Execute(string data, FormSubmitContext formSubmitContext)
        {
            Assert.ArgumentNotNull(formSubmitContext, nameof(formSubmitContext));

            if (!formSubmitContext.HasErrors)
            {
                string firstName = formSubmitContext.Fields.Where(x => x.Name.Equals("First Name")).FirstOrDefault()?.GetType().GetProperty("Value")?.GetValue(formSubmitContext.Fields.Where(x => x.Name.Equals("First Name")).FirstOrDefault(), null)?.ToString();
                string lastName = formSubmitContext.Fields.Where(x => x.Name.Equals("Last Name")).FirstOrDefault()?.GetType().GetProperty("Value")?.GetValue(formSubmitContext.Fields.Where(x => x.Name.Equals("Last Name")).FirstOrDefault(), null)?.ToString();
                string email = formSubmitContext.Fields.Where(x => x.Name.Equals("Email")).FirstOrDefault()?.GetType().GetProperty("Value")?.GetValue(formSubmitContext.Fields.Where(x => x.Name.Equals("Email")).FirstOrDefault(), null)?.ToString();
                string phone = formSubmitContext.Fields.Where(x => x.Name.Equals("Phone")).FirstOrDefault()?.GetType().GetProperty("Value")?.GetValue(formSubmitContext.Fields.Where(x => x.Name.Equals("Phone")).FirstOrDefault(), null)?.ToString();
                string address = formSubmitContext.Fields.Where(x => x.Name.Equals("Address")).FirstOrDefault()?.GetType().GetProperty("Value")?.GetValue(formSubmitContext.Fields.Where(x => x.Name.Equals("Address")).FirstOrDefault(), null)?.ToString();
                Log.Info(Invariant($"Form {formSubmitContext.FormId} submitted successfully."), this);
                AddContact(firstName, lastName, email, phone, address);
            }
            else
            {
                Log.Warn(Invariant($"Form {formSubmitContext.FormId} submitted with errors: {string.Join(", ", formSubmitContext.Errors.Select(t => t.ErrorMessage))}."), this);
            }

            return true;
        }
    }
}