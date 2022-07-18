# Sitecore Mini CRM installation guide  
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
11.	Open the page created in the previous step using Experience editor and insert an MVC Form rendering with data source /sitecore/Forms/Add CRM Contact (installed by the package file).
12.	Your mini CRM should now be ready to use.
