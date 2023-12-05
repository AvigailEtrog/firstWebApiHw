#Supermarket site
The recommended work environment for running the project:
 The project is written in C# and in the .NET 7 framework. In addition, the project uses a SQL SEVER database.
Description:
This project was written in Rest Architecture,
Different packages were installed in it for different needs such as checking password strength, using orm and object mapping
We used a dependency injection design pattern and thus caused a disconnection between the layers,
To create a scalability site we used async and await and return task object, we used Microsoft's framework-orm entity and the database first method, in addition we used swagger to plan, build and consume the api services,
 to remove circular dependencies we created dto's objects for each object and used In the auto mapper library to map the objects, we added in the configuration file of the development workspace important settings such as a connection string to the database and used them in the code, we used a logger and wrote various middlewares such as handling
errors and creating a new entry in the rating table for each route entry with different details about this entry.
Good luck and have fun
