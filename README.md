## ⚡ N5 Challenge Api.

### Instrucciones para correr proyecto : 
#### Crear base de datos en SQL Server: 

```SQL
USE master;
GO

CREATE DATABASE N5;
GO

USE N5;
GO

CREATE TABLE TipoPermisos (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Descripcion TEXT NOT NULL
);

CREATE TABLE Permisos (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  NombreEmpleado TEXT NOT NULL,
  ApellidoEmpleado TEXT NOT NULL,
  TipoPermiso INT NOT NULL,
  FechaPermiso DATE NOT NULL,
  CONSTRAINT FK_TipoPermisos FOREIGN KEY (TipoPermiso) REFERENCES TipoPermisos(Id)
);

```
#### Clonar proyecto :
* Clonar proyecto y abrirlo con Visual Studio.

#### Ajustar connectionstring:
* Ajustar el connectionstring en caso de que se necesite 

### Tecnologías utilizadas : 
![](https://img.shields.io/badge/Code-NetCore-Code?style=flat&logo=dotnet&logoColor=white&color=c691t3)
![](https://img.shields.io/badge/Test-XUnits-informational?style=flat&logo=selenium&logoColor=white&color=c691t3)
![](https://img.shields.io/badge/ORM-EntityFramework-informational?style=flat&logo=&logoColor=white&color=c691t3)
![](https://img.shields.io/badge/DataBase-SQLServer-informational?style=flat&logo=mysql&logoColor=white&color=c691t3)

