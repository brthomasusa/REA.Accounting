What? Add an application around the AdventureWorks database.
Adventure Works Cycles is a large (fictitious) multinational manufacturing company that produces and distributes metal and composite bicycles to commercial markets in North America, Europe, and Asia.  The headquarters for Adventure Works Cycles is Bothell, Washington where the company employs 500 workers.  Additionally, Adventure Works Cycles employs several regional sales teams throughout its market base.

The AdventureWorks database is a sample database for  Microsoft SQL Server 2008 through 2014. It supports standard online transaction processing (OLTP) scenarios for a fictitious bicycle manufacturer. Scenarios include Manufacturing, Sales, Purchasing, Product Management, Contact Management, and Human Resources.  The database has 71 tables, 20 views, 10 stored procedures, and 11 functions spread across human resources, person, production, purchasing, and sales schemas.  The information in the database is what would result from accounting transactions produced during the expenditure, conversion, and revenue processes. 

My goal is to develop an application that will allow a user to preform Create, Retrieved, Update, and Delete (C.R.U.D.) operations on the data in the AdventureWorks database.  

Why?
To learn software development using C#, ASP.NET CORE, and TSQL.

When?
The project has already began; the Human Resources module is about 50% complete.

How?
The initial implementation, a proof of concept, will be a three-tier that uses Blazor Web Assembly for the presentation layer, ASP.NET CORE Minimum API for the business layer, and Transact-SQL to create and manipulate objects in the storage layer. The primary application architectural pattern will be domain-driven-design (DDD).  The presentation layer will be divided into a store front and a back office system for administration.
