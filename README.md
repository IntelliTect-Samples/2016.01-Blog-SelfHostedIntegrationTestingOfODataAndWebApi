# webapi-odata-testing-example
An example for testing OData v4 Web API controllers in a full-stack manner using the OWIN self-host.

Usage:
======

1. Read blog post: [link here]
2. Clone with VS2015
3. Build
4. Run Update-Database in the Package Manager Console to generate DB and seed with data.
5. Run Tests
6. Start with Ctrl-F5 and query with Postman

Note:
=====

If you want to re-generate the OData client from the T4 template, you need to have the "OData v4 Client Code Generator" 
installed in Visual Studio.  The VSIX is found [here](https://visualstudiogallery.msdn.microsoft.com/9b786c0e-79d1-4a50-89a5-125e57475937).

Technology Stack:
=================

1. Visual Studio 2015
2. SQL Server Express 2014 or LocalDB
3. NuGet 3.x
4. Optional: OData v4 Client Code Generator
5. IIS Express (to run the site outside of the OWIN self-host)