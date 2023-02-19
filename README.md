# RecordDb using SQL Server and Dapper

This is a .Net Core console program that is used to test methods that I have created for use in My ASP.Net core website.

This version uses **SQL Server** with **Dapper** to extract data from my Record database.

See the [RecordEFConsole repository](https://github.com/alanrob17/RecordEFConsole) README for documentation on the database structure.

## Other versions of this code

I have written a number of different versions of this code using various database technologies.

The original code was written using ADO.Net 3.5 and is in a private repository. I can make this code available if you require.

### SQL Server

* [Entity Framework and SQL server.](https://github.com/alanrob17/RecordEFConsole)
* [Dapper and SQL Server.](https://github.com/alanrob17/RecordDbSqlDapperApp)

### Sqlite

* [Entity Framework and Sqlite.](https://github.com/alanrob17/RecordDBEFSQLite)
* [Dapper and Sqlite.](https://github.com/alanrob17/RecordDbSQLiteDapper)

### JavaScript

I have a Javascript version of my Record database that consumes JSON data from local storage.

[RecordDb Website](https://recordlist.netlify.app/)

[Source code.](https://github.com/alanrob17/recorddb-app)

## Dapper

I have a long history of working with C# and SQL Server and am proficient in creating stored procedures. Previously I have used ADO.Net to build my database layers and I am interested in using new alternatives for my databases.

This has led me to working with Sqlite and MongoDb.

I'm not overly comfortable with using Entity Framework and was looking for an alternative ORM so decided to investigate using Dapper as my ORM.

### Program.cs

Contains a list of method calls for database routines that I use in my website. These call the **Test** folder methods that are used to return data to the console.

### Test/ArtistTest.cs

Contains methods that consume data from the **Artist** table in my database.

### Test/RecordTest.cs

Are the **Record** table methods and also contains code that joins the Artist and Record tables in a one to many relationship.

### DAL layer

Is a separate project that contains the **Dapper** database access methods.

### Models

Contains the **ArtistModel.cs** and **RecordModel.cs** entities that are used for the database layer.

### ArtistDataAccess.cs

Are the Dapper database access methods that are called by the **Test** methods bring back Artist data.

### RecordDataAccess.cs

Are the Dapper Record entity routines called by the **Test** methods to bring back Record data.
