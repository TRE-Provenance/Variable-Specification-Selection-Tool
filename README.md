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

### Tool Overview

SQL Server Management Studio						19.1.56.0
SQL Server Management Objects (SMO)					16.200.48044
Within SSMS right click on Databases and ‘attach’ the DareToDash database file.

![image](https://github.com/user-attachments/assets/8dbb608e-cf49-4ad3-83d4-2a671ccc73e8)

Table Name | Description 
--- | --- 
Dataset |	Holds the individual dataset details (e.g. dataset name, description, current dataset etc).
DS_Variables|		Individual dataset variable details (variable name, data type, is identifiable field etc).
Project|	Researcher project details (title, description, etc).
Project_Dataset|	Lists the datasets associated with a project.
Researcher| 	All registered researchers’ details (Name, login username, Email etc).
Researcher_Custom_Variables|	List of custom calculated variables as outlined by the researcher (e.g. age at event date).
Researcher_Documents|	List of documents uploaded by the researcher to a specific project.
Researcher_Projects|	Lists all the projects associated with an individual researcher
Researcher_Variables|	List of the variables selected for a specific project dataset.
Students|	To be implemented V2
Users_Unsecure|	Temporary user table for demo purposes only

Stored Procedures | Description 
--- | --- 
OutputSourceDatasets_toJson_byProjectId_DatasetID | Provides structured JSON data for TRE analyst derived from researcher input
_DareUK_MetaDB_TableData |	Provides a SQL script for running against source data in TRE environment


#### Interface 

To load web interface into the development environment right click on ‘DaSH_Researcher_Portal.sln’ and select  open with >> Microsoft Visual Studio 2022 
Update the web.config  connectionStrings  DareToDaSHEntities2 entity data source to point to your SQL Server Management Studio instance.

##### Login.aspx

![image](https://github.com/user-attachments/assets/9fe76133-52a5-423c-86f9-1fe7d50ed985)


Simple login/registration page for demo purposes only – used for linking user to projects.

#### Page: Home/About/Contact: Standard Customisable Content

![image](https://github.com/user-attachments/assets/618065ee-04d2-4ff1-af70-67fd3eb5998b)

#### Researcher Project Screen: Accessed via ‘My DaSH’ menu option

![image](https://github.com/user-attachments/assets/9ea75600-f695-48ce-bb09-69e9d5f0762f)

*	Lists all projects associated with the logged in user.
*	‘New Project’ button initiates the new project registration process.
*	‘Go >>’ button takes user to the individual project section.




