# TriangleServerAndMVC

## Triangle Server

This is the SOAP rest service which computes the calculations necessary for determinine the type of triangle given by its three lengths. It also handles other computations like checking for errors, calculating the current page as well as how many pages there are in the database, where each record is a page.

Accessed at http://localhost:53479/TriangleTool.asmx

### Instructions for how to develop, use, and test the code.

Download the TriangleWebService folder in Windows Server 2019, or build it yourself in Visual Studio 2019 and place the web service in Windows Server 2019.

In Windows Server 2019, open Server Manager, click on Manage > Add Roles and Features, and in features, install .NET Framework 4.5 if it has not been done already. 

![Image](https://i.imgur.com/wfxW9mp.png)

Also go to Server and choose IIS from the left panel and note down the IPv4 Address.

If you do not have IIS Manager, in Windows Powershell, type the following command.

`Install-WindowsFeature -name Web-Server -IncludeManagementTools`

![](https://i.imgur.com/oFbL2SR.png)

In IIS Manager, in the connections tab on the left, click your computer name > Sites and right click Default Web Site and choose Add Aplication.... In the Add Application menu, type in a name as your Alias (e.g. TriangleWebService) and set the Physical Path to the folder containing your .asmx file. Click OK. 

Go to your Computer Name > Application Pools and verify that the Application Pools you have created, as well as the .NET application pools are currently running. 

Return to your Computer Name > Sites > Default Web Site and right click your alias name and choose "Switch to Content View". You will see your .asmx file. Right click your Web.config file and choose Edit Permissions, and a Properties window will display. In the Security tab, verify that the users IUSR and IIS_IUSRS exist and have Full control permissions. If not, you will need to click Edit, and Add these users in yourself.

![Image](https://i.imgur.com/71GGVET.png)

Now, simply right click your .asmx file and select Browse. You will be taken to a page with all the functions. 

![Image](https://i.imgur.com/RHylCv3.png)

And finally, for completeness, replace "localhost" in your web browser URL with the IIS Server IP Address from earlier.





## Triangle MVC

This is a tool which allows the user to manually edit the results of a triangle. There really are no restrictions on what can be added here so users can get really creative with the results they wish to display.

Accessed at http://localhost:51253/Triangles

## Instructions for how to develop, use, and test the code.

#### Generating the SQL Database

Using the script.sql file, build an SQL Server with the database name TriangleDB under the server name ".".

#### Configurating your IP Address

Find your local IP Address by typing `ipconfig` into the terminal of your choice, and replace all IP addresses starting with 192.168.*.* with your own IP Address. The reason for the specific IP address is because I am currently developing an Android Application in Java which sends and captures SOAP requests and responses in order to calculate and determine the nature of the triangles provided by their lengths.
