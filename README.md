# ByteLab

Proyecto final de Programación de Software - ITM

Samuel Tabares Parra

Sistema web para una tienda de hardware: catálogo de productos, inventario por sucursal, compras a proveedores, ventas, pagos, envíos, devoluciones y garantías.

## Tecnologías

- .NET 10 / C#
- ASP.NET Core Web API
- ASP.NET Core Razor Pages
- Entity Framework Core + SQL Server
- MSTest
- QuestPDF y ClosedXML (reportes PDF y Excel)
- Leaflet + OpenStreetMap (mapa de sucursales)

## Proyectos

| Proyecto | Descripción |
|---|---|
| lib_bytelab | Entidades, interfaces, repositorios y conexión a la base de datos |
| servicio_bytelab | API REST (http://localhost:5210) |
| presentaciones_bytelab | Comunicación del front con la API |
| front_bytelab | Aplicación web (http://localhost:5180) |
| pruebas_bytelab | Pruebas unitarias |

## Cómo ejecutar

1. Ejecutar `lib_bytelab/Script.sql` en SQL Server Management Studio. Crea la base `db_bytelab` con datos de prueba.
2. Si el servidor no es `localhost`, cambiar la cadena de conexión en `servicio_bytelab/appsettings.json` y en `lib_bytelab/nucleo/DatosGenerales.cs`.
3. Abrir `ByteLab.slnx` y configurar varios proyectos de inicio: `servicio_bytelab` y `front_bytelab`.
4. Ejecutar y entrar a http://localhost:5180.

## Usuarios de prueba

| Correo | Contraseña | Rol |
|---|---|---|
| admin@bytelab.com | Admin.123 | Administrador |
| vendedor@bytelab.com | Vendedor.123 | Vendedor |
| bodega@bytelab.com | Bodega.123 | Bodeguero |
| laura@correo.com | Cliente.123 | Cliente |

## Funcionalidades

- 25 tablas, cada una con su API y su ventana.
- Login con contraseñas cifradas (SHA-256), roles y permisos por ventana.
- Auditoría de todos los cambios en la tabla Auditorias.
- Al registrar una venta se descuenta el inventario y se calcula el total con la promoción.
- Reportes en PDF y Excel en todas las ventanas y en la página de Reportes.
- Factura imprimible de cada venta.
- Mapa de sucursales.

## Publicación en IIS

1. Instalar el ASP.NET Core Hosting Bundle de .NET 10.
2. Ejecutar `publicar-iis.ps1`.
3. En IIS crear los sitios `bytelab-api` (puerto 5210) y `bytelab-web` (puerto 8080) con grupo de aplicaciones "Sin código administrado".
