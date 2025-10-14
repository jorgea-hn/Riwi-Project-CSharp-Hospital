# San Vicente Hospital - Management System

A .NET 8 console application that provides a basic management system for a hospital. It allows for the management of patients, doctors, and appointments, and includes an email notification feature using the SendGrid service.

This project was developed by **Jorge Henríquez**.

## Features

- **Patient Management:** Create, list, and find patients.
- **Doctor Management:** Create, list, and find doctors by specialty.
- **Appointment Management:**
    - Schedule new appointments.
    - Cancel existing appointments.
    - Mark appointments as attended.
    - View appointments by patient or doctor.
- **Email Notifications:** Automatically sends a confirmation email to the patient when a new appointment is scheduled. It uses SendGrid for the actual email delivery and maintains a notification history log in the database.
- **In-Memory Persistence:** All data is managed in-memory and is reset when the application restarts. It includes a `DataSeeder` to populate the application with test data for a quick and easy demonstration.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A [SendGrid Account](https://sendgrid.com/) for email notifications.

## Project Configuration

To enable the email notification feature, you **must** configure your SendGrid credentials in the `HospitalSanVicente/appsettings.json` file.

```json
{
  "SendGrid": {
    "ApiKey": "YOUR_SENDGRID_API_KEY_HERE",
    "FromEmail": "your_verified_email@example.com",
    "FromName": "San Vicente Hospital"
  }
  // ...
}
```

### Configuration Steps:

1.  **`ApiKey`**:
    - Log in to your SendGrid account.
    - Navigate to **Settings -> API Keys**.
    - Create a new API Key with at least "Mail Send" permissions.
    - Copy the generated key and paste it as the `ApiKey` value in `appsettings.json`.

2.  **`FromEmail`**:
    - In your SendGrid dashboard, navigate to **Settings -> Sender Authentication**.
    - Verify a "Single Sender Identity" (this will be your email address).
    - Once verified, use that exact email address as the `FromEmail` value in the configuration file.

> **Important Note:** Email delivery will fail if the `ApiKey` is invalid or if the `FromEmail` does not match a verified sender identity in your SendGrid account.

## How to Run the Application

1.  Open a terminal and navigate to the project's root folder (`HospitalSanVicente`).
2.  Build the project to restore dependencies:
    ```sh
    dotnet build
    ```
3.  Run the application:
    ```sh
    dotnet run
    ```
    The application will start, load the seed data, and display the main menu in the console.

## Project Structure

```
/HospitalSanVicente
|-- Data/
|   |-- InMemoryData.cs
|-- Interfaces/
|   |-- IAppointmentRepository.cs
|   |-- IAppointmentService.cs
|   |-- ICrudService.cs
|   |-- IDoctorRepository.cs
|   |-- IDoctorService.cs
|   |-- IEmailNotificationRepository.cs
|   |-- IEmailNotificationService.cs
|   |-- IEmailService.cs
|   |-- IPatientRepository.cs
|   |-- IPatientService.cs
|   |-- IRepository.cs
|-- Model/
|   |-- Appointment.cs
|   |-- Doctor.cs
|   |-- EmailNotification.cs
|   |-- EmailStatus.cs
|   |-- Patient.cs
|   |-- Person.cs
|-- Repository/
|   |-- AppointmentRepository.cs
|   |-- DoctorRepository.cs
|   |-- EmailNotificationRepository.cs
|   |-- PatientRepository.cs
|-- Services/
|   |-- AppointmentService.cs
|   |-- CombinedEmailService.cs
|   |-- DoctorService.cs
|   |-- EmailNotificationService.cs
|   |-- PatientService.cs
|   |-- SendGridEmailService.cs
|-- UI/
|   |-- AppointmentUI.cs
|   |-- ConsoleReader.cs
|   |-- DoctorUI.cs
|   |-- EmailNotificationUI.cs
|   |-- PatientUI.cs
|-- docs/
|   |-- DIagramaUML.png
|   |-- DiagramaCasoDeUso.svg
|-- DataSeeder.cs
|-- HospitalSanVicente.csproj
|-- Program.cs
|-- Startup.cs
|-- appsettings.json
```
