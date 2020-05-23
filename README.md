# ContactInfo

This project is created for maintaining the contact information.I have Created five projects in a solution.Four projects used to implement DIP with generic repository pattern and Structure map. These are:

ContactInfo.API.

ContactInfo.Data (class library)

ContactInfo.Service (class library)

ContactInfo.Web (web application)

One project(ContactInfo.Test) Created for the Unit testing.

Benefits of using this structure

1.Centralized the data access logic therefore easy to maintain.

2.Redundant Code.

3.Provide great facility to write unit tests for Data Layer.

4.Improves the readability.


The architecture is dividing the project into three layers that are User interface layer, business layer and data(database) layer where we separate UI, logic, and data in three divisions. If user wants to change the database then he has to only make a change in the data layer, rest everything will be remains the same.

A service layer is an additional layer in an application that mediates communication between a controller and repository layer. The service layer contains business logic. The benefit for this layer creates a lose coupling. We can call this layer from other Presentation layer.

Data Layer - The Service Layer exposes Interfaces to be implemented in the Data Access Layer so we get the "abstraction". The thing we will do is to create the DbContext class that will be responsible to access the database. 

Instructions to run the application :-

1. Download the project.
2. Open ContactInfo.API.sln with Visual Studio 2017.
3. Right click on the solution and go to the properties.Select multiple startup projects radio button and choose contactInfo.API and ContactInfo.Web prject.Action for both the projects should be Start.
4. Go to the ContactInfo.Web project and right click on ContactInfo.html inside the pages folder then select Set as Start Page.
4. Execute the below script in the database.Added the same below query in the DB script folder.

CREATE TABLE [dbo].[tblContactInfo](

	[ID] [int] IDENTITY(1,1) NOT NULL,
	
	[FirstName] [varchar](100) NULL,
	
	[LastName] [varchar](100) NULL,
	
	[Email] [varchar](100) NULL,
	
	[PhoneNumber] [varchar](50) NULL,
	
	[Status] [bit] NULL,
	
 CONSTRAINT [PK_tblContactInfo] PRIMARY KEY CLUSTERED 
 
(
	[ID] ASC
	
)

WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

5. Please change the connection string(Data Source,Initial Catalog,UserId,Password) in web.config file of ContactInfo.API project.

<connectionStrings>
	
    <add name="ContactInfoEntities" connectionString="metadata=res://*/ContactInfo.csdl|res://*/ContactInfo.ssdl|res://*/ContactInfo.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=XXXXXXXXX;initial catalog=XXXXXXXX;persist security info=True;user id=XXXXX;password=XXXXXXX;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
		
  </connectionStrings>

6. Compile the project and run the application.

