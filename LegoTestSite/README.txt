This is currently a two part project, with this part being the database portion of the server. 
These are the important sections summarized.

Program:
	Used to only access set up the server side of the web application. The start off point.

Database Accessors:
	These are used to determine the type of database is being connected to. In this project, there are 2 databases. 
A main one that is a MySQL database that is meant to contain the main contents, and the SQLite database used for 
test purposes and a default database if the MySQL database can't be accessed. This architecture also allows 
the development away from my main computer without needing the credentials for the MySQL database.

Databases:
	There are 2:
	- MySQL (Main database)
	- SQLite (Test and default database)

Controllers:
	These are the sections that allow the API to be accessable. Although this is only subject to localhost ports
currently. 

SensitiveReader.cs:
	Allows for reading of credentials while decoupling it from the code. Especially handy in not exposing the
credentials to github.

LegoTestSite.http:
	This is related to testing and was generated when originally making the project. Used for simulated testing to
make sure thigns work correctly without needing to type it out in a browser each time. If testing is needed for 
error codes, use this. If formatting is important, then looking at the browser is preferable since json has a 
habbit of including whitespace characters in this project.