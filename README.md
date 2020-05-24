# ContactInfo

This project is created for maintaining the contact information.I have Created five projects in a solution.Four projects used to implement DIP with generic repository pattern and Structure map. These are:

ContactInfo.API (Web API)

ContactInfo.Data (class library)

ContactInfo.Service (class library)

ContactInfo.Web (web application)

One project(ContactInfo.Test) Created for the Unit testing.

Benefits of using this structure

1.Centralized the data access logic therefore easy to maintain.

2.Redundant Code.

3.Provide great facility to write unit tests for Data Layer.

4.Improves the readability.


ContactInfo.API

For a Web application, it represents the Web API. This layer has an implementation of the dependency injection principle so that the application builds a loosely coupled structure and can communicate to the internal layer via interfaces.

ContactInfo.Service

The Service layer holds interfaces with common operations, such as Insert, Delete, Update, and Select. Also, this layer is used to communicate between the UI layer and repository layer. The Service layer also could hold business logic for an entity. In this layer, service interfaces are kept separate from its implementation, keeping loose coupling and separation of concerns in mind.

ContactInfo.Data

This layer creates an abstraction between the domain entities and business logic of an application. In this layer, we typically add interfaces that provide object saving and retrieving behavior typically by involving a database. This layer consists of the data access pattern, which is a more loosely coupled approach to data access. We also create a generic repository, and add queries to retrieve data from the source, map the data from data source to a business entity, and persist changes in the business entity to the data source.

ContactInfo.Web

The ContactInfo.Web is the front end layer in this architecture and consists of the user interface. This user interface is often a graphical one accessible through a web browser or web-based application and which displays content and information useful to an end user. This ContactInfo.Web project contains such as HTML5, JavaScript, CSS, or through other popular web development frameworks, and communicates with others layers through API calls.

ContactInfo.Test

I have created a test project by selection Unit Test project template from the Test project tab. To start writing test, very first in the unit test project I have added following references

Reference of the API project
Reference of the Data project
Reference of the Service project
Entity framework package

In the test project, I have created a class called ContactInfoControllerTest. Inside the unit test class initialize the test as shown in listing below,

        [TestInitialize]
        public void Initialize()
        {
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new TestContext();
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _contactInfoRepository = new ContactInfoRepository(_testUnitOfWork);
            _contactInfoService = new ContactInfoService(_contactInfoRepository);
            _controller = new ContactInfoController(_contactInfoService);
        }

In the test setup, I am creating instance of TestContext class and then setting up the database with the initial values. Also in the test setup, instance of Controller class is created. 

 	[TestMethod]
        public async Task GetAllContactDetails_Return_Test()
        {
            var result = await _controller.GetDetails();
            Assert.IsNotNull(result);
            var numberOfRecords = result.ToList().Count;
            Assert.AreEqual(2, numberOfRecords);
        }
	
In the test above I am calling the GetDetails method of the Controller class and then verifying the number of records. In ideal condition above test should be passed. You can verify the test result in Test-Window-Test Explorer.We can right tests for Insert, Delete, and Update also. As of now I have written test to verify that database is getting created along with the initialized data. 



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
	
    <add name="ContactInfoEntities" 	connectionString="metadata=res://*/ContactInfo.csdl|res://*/ContactInfo.ssdl|res://*/ContactInfo.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=XXXXXXXXX;initial catalog=XXXXXXXX;persist security info=True;user id=XXXXX;password=XXXXXXX;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
		
  </connectionStrings>

6. Compile the project and run the application.

