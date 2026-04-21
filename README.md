# 🏥 API de Gestion de Pacientes (.NET 9)

## 📌 Descripcion

API REST desarrollada en .NET 9 para la gestion de pacientes. Permite operaciones CRUD, validacion de duplicados, paginacion, filtros y ejecucion de procedimientos almacenados en SQL Server.

---

# ⚙️ Tecnologias utilizadas

* ASP.NET Core Web API (.NET 9)
* Entity Framework Core
* SQL Server
* xUnit (pruebas unitarias)
* Swagger (OpenAPI)

---

# 🚀 Instalacion y configuracion

## 1. Clonar repositorio

```bash
git clone https://github.com/IAS145/patients.git
cd PatientsApi
```

---

## 2. Configurar conexion a SQL Server

Editar archivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=PatientsDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

---

# 🗄️ Creacion de la base de datos

## Opcion 1: Script manual

```sql
CREATE DATABASE PatientsDb;
GO

USE PatientsDb;

CREATE TABLE Patients (
    PatientId INT IDENTITY(1,1) PRIMARY KEY,
    DocumentType NVARCHAR(10) NOT NULL,
    DocumentNumber NVARCHAR(20) NOT NULL,
    FirstName NVARCHAR(80) NOT NULL,
    LastName NVARCHAR(80) NOT NULL,
    BirthDate DATE NOT NULL,
    PhoneNumber NVARCHAR(20) NULL,
    Email NVARCHAR(120) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT UQ_Patient_Document UNIQUE (DocumentType, DocumentNumber)
);
GO

CREATE OR ALTER PROCEDURE GetPatientsCreatedAfter
    @CreatedAfter DATETIME2
AS
BEGIN
    SELECT *
    FROM Patients
    WHERE CreatedAt > @CreatedAfter
    ORDER BY CreatedAt DESC;
END
```

---

## Opcion 2: Migraciones EF Core

```bash
dotnet ef database update
```

---

# ▶️ Ejecucion del proyecto

```bash
dotnet run
```

Swagger disponible en:

```
https://localhost:1237/swagger
```

---

# 📡 Endpoints

## Crear paciente

```
POST /api/patients
```

## Listar pacientes (paginado + filtros)

```
GET /api/patients?page=1&pageSize=10&name=Juan&documentNumber=123
```

## Obtener por ID

```
GET /api/patients/{id}
```

## Actualizar paciente

```
PUT /api/patients/{id}
```

✔ Implementa actualizacion parcial (solo campos enviados)

## Eliminar paciente

```
DELETE /api/patients/{id}
```

## Obtener pacientes por fecha (Stored Procedure)

```
GET /api/patients/created-after?date=2024-01-01
```

---

# 🧪 Pruebas unitarias

Ejecutar:

```bash
dotnet test
```

Incluyen:

* Creacion de pacientes
* Validacion de duplicados
* Paginacion

---

# 🏗️ Arquitectura

El proyecto sigue una arquitectura en capas simple:

### 🔹 Controllers

Manejan las solicitudes HTTP y respuestas.

### 🔹 DbContext (EF Core)

Gestiona el acceso a datos y configuraci0n de entidades.

### 🔹 Entidades (Models)

Representan la estructura de la base de datos.

### 🔹 DTOs

Separan los datos de entrada/salida del modelo de dominio.

---

# 🎯 Decisiones técnicas

### ✔ Entity Framework Core

* ORM oficial de .NET
* Permite consultas con LINQ
* Integraci0n directa con SQL Server

### ✔ SQL Server

* Integridad de datos mediante constraints
* Índice único para evitar duplicados
* Uso de Stored Procedure

### ✔ Validaci0n de duplicados

Se implementa en:

* API → mejor experiencia de usuario
* Base de datos → integridad garantizada

### ✔ Paginaci0n

Se implementa con:

```csharp
Skip((page - 1) * pageSize).Take(pageSize)
```

### ✔ Stored Procedure

Permite consultas optimizadas y demuestra integracion con SQL nativo.


# 👨‍💻 Autor

Edgar Leonardo Velasquez Villar
edgarleonar555@gmail.com
