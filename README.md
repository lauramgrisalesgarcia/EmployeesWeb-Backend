<h1><strong><i>Back-end creado para la gestión de empleados.</i></strong></h1>
<h6>Back-end desarrollado en C# ASP.NET CORE WEB API Netcore 8.0</h6>
<p>Desarrollado en estructura MVC (Modelo Vista Controlador)</p>
<h3><strong><i>Especificaciones del proyecto:</i></strong></h3>
<p>API para el sitio web de la empresa MORA (Creación única para el proyecto) que requiere la administración de los empleados asociados a la compañía</p>
<p>Los campos deben tener las siguientes validaciones:</p>
<ul>
<li><strong>Nombre:</strong> Campo de texto obligatorio de máximo 100 caracteres</li>
<li><strong>Apellido:</strong> Campo de texto opcional</li>
<li><strong>Fecha de nacimiento:</strong> Campo de tipo fecha obligatorio. Mayor de 18 años y menor de 65 años</li>
<li><strong>Email:</strong> Campo de texto obligatorio</li>  
<li><strong>Rol:</strong> Selección de rol obligatorio</li>  
</ul>
<p>Validaciones generales:</p>
<ul>
<li>No se pueden repetir empleados</li> 
</ul>
<h3><strong><i>¿Cómo ejecutarlo?</i></strong></h3>
<ul>
<li>Descargue o clone el proyecto en su equipo</li> 
<li>En el archivo <strong>launchSettings.json</strong> encontrará las url de la aplicación disponibles. El Front-end está configurado con la URL http://localhost:5027. Si requiere cambiarla, ajuste la variable <strong>URL_API</strong> del archivo <strong>config.js</strong></li> 
<li>El back-end fue creado con entity framework in memory, por lo tanto es 100% funcional y los métodos que ejecute quedarán en memoria simulando una base de datos. Si cierra la ejecución del back-end, se borrará todo lo que haya almacenado.</li> 
<li>Cuando ejecute el proyecto no olvide seleccionar si lo hará por http o https. Si no ha realizado cambios en la URL del front-end, ejecute el proyecto en http para evitar problemas de certificados en el navegador.</li> 
<li>Al momento de ejecutarse se abrirá su navegador con una vista a Swagger, podrá acceder a los métodos sin requerir de un front-end por si requiere hacer alguna validación. Cierre la ventana sólo cuando finalice de trabajar</li> 
<li>Teniendo en ejecución el back-end, ya puede abrir el index.html del front-end e interactuar con el proyecto</li>
<li>¡Ya está listo para trabajar!</li>

