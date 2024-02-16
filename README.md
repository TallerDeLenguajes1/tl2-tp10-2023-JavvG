# Documentación: Gestor de Tareas

Este Gestor de Tareas es una aplicación web basada en el *framework* **ASP.NET MVC**, diseñada para proporcionar una solución integral para la gestión de tableros y tareas. Este sistema incluye características como la gestión de usuarios, roles, tableros y tareas.

#
## Tecnologías Utilizadas
- **Lenguaje de programación:** C#
- **Marco de diseño (framework):** ASP.NET MVC
- **Entorno de Desarrollo Integrado (IDE):** Visual Studio Code
- **Base de Datos:** SQLite

#
## Principales Características
- **Usuarios:**
   - Registro, edición y eliminación de usuarios.
   - Autenticación de usuarios.
   - Permisos basados en roles (**administrador** u **operador**). En el caso de usuarios operadores, se restringen de la siguiente forma:
        - **Tableros Propios:** permisos completos (*lectura, escritura y eliminación*)

        - **Tableros Compartidos:** solo lectura.

        - **Tareas No Asignadas:** solo lectura.

        - **Tareas Asignadas:** lectura y modificación (únicamente para modificar el estado de la tarea).

- **Tareas:**
   - Creación, modificación y eliminación de tareas.
   - Asignación de tareas a usuarios.

- **Tableros:**
   - Creación, modificación y eliminación de tableros.
   - Asociación de tareas a tableros.

- **Autenticación:**
   - Mecanismo de autenticación de usuarios.

- **Manejo de Errores:**
   - Implementación de controladores de errores.

- **ViewModels:**
   - Uso de ViewModels para una representación flexible de datos en las vistas.

#
## Detalles Adicionales

#### Base de Datos
- La base de datos utiliza **SQLite** y está estructurada con tres entidades principales: **Usuario**, **Tarea** y **Tablero**. Se establecen relaciones entre ellas, utilizando claves foráneas para mantener la integridad referencial.


#### Patrones de Diseño
- **Patrón Repositorio:** se usó el patrón de diseño *Repository* para los repositorios de **Usuario**, **Tarea** y **Tablero**. Esto ayuda a separar la lógica de acceso a datos de la lógica de negocio, proporcionando una interfaz común para interactuar con la base de datos.
  
- **Inyección de Dependencia (DI):** se utiliz para gestionar las dependencias de manera flexible y desacoplada, mejorando la mantenibilidad y la escalabilidad del código.

#### Diseño
- Para el diseño de las vistas, se usaron recursos tanto de **CSS** personalizado como **Bootstrap**. El diseño de la interfaz se inspiró en los elementos visuales de *Vision OS* de Apple (los recursos de inspiración se tomaron de su sitio oficial de **Figma**).

#
**Programador: Javier Granero, *CS Student*.**
#