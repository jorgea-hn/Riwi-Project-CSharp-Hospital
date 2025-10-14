# Hospital San Vicente - Management System

This is a console application for managing patients, doctors, and appointments at Hospital San Vicente. It is built with C# and .NET.

**Repository URL:** [https://github.com/jorgea-hn/Riwi-Project-CSharp-Hospital.git](https://github.com/jorgea-hn/Riwi-Project-CSharp-Hospital.git)

## Folder Structure

The project has the following folder structure:

```
.
|-- HospitalSanVicente/          # Main project folder
|   |-- Data/                    # Contains the JSON files used as a database.
|   |-- Interfaces/              # Contains the interfaces for services and repositories.
|   |-- Model/                   # Contains the data models (Patient, Doctor, Appointment, etc.).
|   |-- Repositories/            # Contains the implementations of the data repositories.
|   |-- Services/                # Contains the business logic services.
|   |-- DataSeeder.cs            # Class to populate the database with initial data.
|   |-- Program.cs               # Main entry point of the application.
|   |-- Startup.cs               # Configuration of services and dependency injection.
|   +-- HospitalSanVicente.csproj  # Project file.
|
|-- docs/
|   |-- uml_class_diagram.md     # UML Class Diagram of the project.
|   +-- use_case_diagram.md      # Use Case Diagram of the project.
|
|-- .gitignore
+-- README.md
```

## Diagrams

For a better understanding of the project's architecture and functionality, you can check the following diagrams:

*   **[UML Class Diagram](docs/uml_class_diagram.md)**: Shows the classes, their attributes, and the relationships between them.
*   **[Use Case Diagram](docs/use_case_diagram.md)**: Illustrates the interactions between the user and the system's functionalities.
