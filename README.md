Backend de GestorTareas, proyecto evaluativo.
Lautaro Tamborini Dalmasso
Plantilla Usada: ASP.NET Core Web API

Ejecutar API: Abra la solucion con Visual Studio y pulse en el boton 'Run" arriba en la interfaz de Visual Studio, deberia abrirse uno de sus navegadores con la pagina de Swagger. Para cambiar de navegador, vaya al boton "Run" y pulse en la flecha hacia abajo que se encuentra justo al a derecha del boton, pegado a este, luego vaya a "Explorador Web" y elija su explorador de preferencia.

API del proyecto evaluativo, testeado con Swagger.
CRUD más Filtrado por Estado en metodo GetTasks y GetAll, Delete y Create de Users.
ConnectionString: "Server=DESKTOP-LIIBE0D\\MSSQL2022;Database=GestorTareasDB;Trusted_Connection=true;Encrypt=false;MultipleActiveResultSets=true;TrustServerCertificate=True;"

Tecnologias usadas: Swagger, plantilla ASP.NET Core Web API, .NET 8.0, C#, Git.

Decidí crear un controlador para Users con los métodos GetAll, Delete y Create para cerrar el circuito del sistema, facilitar la prueba de este en implementación, y a la vez ser facil de reemplazar. A diferencia de con tasks, no hice separación de responsabilidades, todos los métodos de Users estan implementados en el controlador, borrar este controlador elimina todos los métodos y permite ser remplazado por el código de un supuesto compañero de trabajo que haya implementado las funciones de Users. 
Decidí usar interfaces para organizarme y dar contexto a InteliCode para que me ayude a escribir.

IA usada: ChatGPT y "InteliCode" (ML de Visual Studio)
Usada en: Creación de contenido de prueba de bases de datos (ChatGPT), durante ingeniería inversa de EF Tools (ChatGPT), Durante testeo (ChatGPT), Escribiendo código (InteliCode), implementación de códigos HTTP (ChatGPT), implementación de buenas practicas de comunicacion Servicio - Constructor (ChatGPT), Comprender "Get /api/tasks?status={estado}" (ChatGPT), Investigación, implementación y Debugin de FrontEnd con React.js (Chatgpt), implementación de CORS (ChatGPT), implementación de Tablas (ChatGPT), implementación e Investigación de Agrupacion de Paginas en React.js (ChatGPT). Investigación de manejo de Git con frontend de React.js (ChatGPT).

Creación de contenido de prueba de bases de datos > Debugging. 
Durante ingeniería inversa de EF Tools > Debugging.
Durante testeo > Debugging.
Escribiendo código > Aparece espontáneamente el autocompletar en gris, lo use cuando vi que era correcto, no sé exactamente en donde pero si se que fue durante todo el proceso.
implementación de códigos HTTP > Investigación.
implementación de buenas practicas de comunicacion Servicio - Constructor > Investigación.
Comprender "Get /api/tasks?status={estado}" > Investigación, implementación y Debugging.
Investigación e implementación de FrontEnd con React.js > Investigación e implementación.
implementación de CORS > implementación. 
implementación de Tablas > implementación.
implementación e Investigación de Agrupacion de Paginas en React.js > implementación e Investigación.
Investigación de manejo de Git con frontend de React.js > Investigación.
