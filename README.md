# Sitecore Mini CRM Installation Guide  
## Prerequisites  
1.	Sitecore 10.2 – it is possible the mini CRM will work with earlier versions, but it has not been tested.
2.	Sitecore Forms
3.	Bootstrap v5.1.3 – supplied as part of the installation package.
4.	JQuery 3.6.0 – supplied as part of the installation package; required for search functionality.

  
## Installation instructions
1.	Clone this repo https://github.com/abdklmw/Sitecore-CRM.git
2.	From package manager console in Visual Studio, run this command to restore nuget packages:
Update-Package -reinstall
3.	Build and deploy to your Sitecore 10.2 instance.
4.	Install the supplied Sitecore Items package from the root of the repo, “Sitecore Mini CRM Items.zip”.  
  
> For your convenience, a layout has been supplied which can be used to quickly get the CRM up and running. It is recommended that you edit your own placeholders to allow the addition of the “SitecoreCRM Contacts Folder” rendering where desired. This supplied layout is located at /sitecore/layout/Layouts/Project/SitecoreCRM/SitecoreCRMLayout and is already the layout for the page items referenced below.  
  
5.	Create an item in the content tree based on the template: /sitecore/templates/Project/Sitecore CRM/CRM Page
6.	Create a sub item of that new page based on the template: /sitecore/templates/Project/Sitecore CRM/CRM Contacts Folder (note, this item is not required to be under the CRM Page, but is suggested).
7.	Update the config file /App_Config/Include/SitecoreCRM.config and set the setting SitecoreCRM.CrmContactsFolder to the ID for the contacts folder created in the previous step.
8.	Using Experience editor, open the page created in the previous step and insert a **SitecoreCRM** rendering and set the data source to the same created in step 6 above.
9.	In roughly the center of the component, you should see **[No text in field]**, click here and link to the page you created in step 9 above; be sure to enter a **Link Description** which will be the text that will be displayed on the button.
10.	Create a page based on the template: /sitecore/templates/Project/Sitecore CRM/CRM Add Contact Page
11.	Open the page created in the previous step using Experience editor and insert an **MVC Form** rendering with data source /sitecore/Forms/Add CRM Contact (installed by the package file).
12.	Edit the form located at /sitecore/Forms/Add CRM Contact and update the redirect action to the page item created in step 5 above.
13.	Your mini CRM should now be ready to use.
  

# Sitecore Mini CRM User Guide  
## Adding contacts  
1.	To add new contacts ensure you are viewing the listing page from either experience editor in preview mode or from the published site and click the green button at the top center with text which was defined during installation.
2.	Enter the contact values and click **Save Contact**.

## Searching  
From the list page, enter at least 3 characters into the search field at the top left. The search is automatically performed on all fields as you type.

## Update existing contact  
1.	From within Content Editor, navigate to the **CRM Contacts Folder**.
2.	Locate the desired content in the folder and select it.
3.	Update desired field(s).
4.	Click **Save**.

## Delete existing contact  
1.	From within Content Editor, navigate to the CRM Contacts Folder.
2.	Locate the desired content in the folder and select it.
3.	Click **Delete**.
