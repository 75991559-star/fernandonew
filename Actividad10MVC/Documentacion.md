# Guía de la actividad

## Módulos incluidos

- **Empleados (caso desarrollado):** apellidos, nombres, fecha de nacimiento, categoría, hijos opcionales entre 0 y 3 y correo electrónico opcional validado.
- **Productos (caso propuesto 01):** descripción, fecha, categoría Útiles/Regalos/Fiestas, stock entre 1 y 1000 y precio mayor que cero.
- **Clientes (caso propuesto 02):** nombre, distrito en lista desplegable, teléfono, correo electrónico y fecha de registro.

Los códigos de empleado, producto y cliente se asignan desde el controlador y no se aceptan como dato editable.

## Estructura MVC

- `Models/`: modelos y reglas de validación con Data Annotations.
- `Controllers/`: acciones `Create`, `List`, `Details`, `Edit` y `Delete` para cada módulo.
- `Views/`: vistas Razor para formularios, listados, detalles y confirmación de eliminación.
- `Services/RegistroStore.cs`: colecciones genéricas `List<Empleado>`, `List<Producto>` y `List<Cliente>`.
- `wwwroot/css/site.css`: estilos de la plantilla, menú, portada y formularios.

## Métodos de colección demostrados

- `Add`: alta de cada registro.
- `Find`: búsqueda de un registro por su código.
- `Remove`: eliminación.
- `Exists`: comprobación de existencia antes de eliminar.
- `Where`: búsqueda y filtrado de listados.
- `Select`: construcción de las opciones para los cuadros combinados.
- `First`: muestra el primer producto de manera segura si la colección no está vacía.
- `FirstOrDefault`: obtiene un empleado destacado sin fallar cuando aún no existen registros.

## Consideración

El enunciado trabaja con `List<T>`, por eso no se conecta a MySQL. Los elementos se conservan mientras la aplicación está abierta y se borran al detenerla; para persistirlos se requeriría otra actividad con base de datos.
