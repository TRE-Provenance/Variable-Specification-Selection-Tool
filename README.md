# Variable_Specification_Selection_Tool

# Project Title
Researcher Variable Selection Tool






## Getting Started

Code to be used within the local environment.  Prerequisites will need to be installed.

### Prerequisites

- Microsoft Visual Studio Community 2022 <a href="https://visualstudio.microsoft.com/vs/community/"> Here</a>
- SQL Server Management Studio <a href="https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16"> Here</a>


### Installing


Database Development Environment
(https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)**

SQL Server Management Studio                                   19.1.56.0
SQL Server Management Objects (SMO)                            16.200.48044
To create a local server group:
https://learn.microsoft.com/en-us/sql/ssms/register-servers/create-a-new-registered-server-sql-server-management-studio?view=sql-server-ver16

Within the new local server right click on Databases and ‘attach’ the DareToDash database file.


Code Development Environment 

(https://visualstudio.microsoft.com/vs/community/)**
 Microsoft Visual Studio Community 2022 Version                17.7.4 
Microsoft .NET Framework Version                               4.8.09037 
Selection : Asp.NET and web development. 

To load web interface into the development environment right click on ‘DaSH_Researcher_Portal.sln’ and select open with >> Microsoft Visual Studio 2022 

Within the web.config file the <connectionStrings> ‘DareToDaSHEntities2’ entity ‘data source’ field needs to be updated with the name of the SQL Server Management Studio (SSMS) Database Engine/Server Name (this was specified during the setup up of the SSMS local server group) 

<add name="DareToDaSHEntities2" connectionString="metadata=res://*/EntityFramework.DareModel.csdl|res://*/EntityFramework.DareModel.ssdl|res://*/EntityFramework.DareModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source= Database_Engine/Server_Name_GOES_HERE;initial catalog=DareToDaSH;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />


**Please check licence agreement: NO RIGHTS OF USAGE ARE INFERRED BY PROVIDING THESE LINKS. 
