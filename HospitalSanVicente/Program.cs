
using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSanVicente
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            // Seed data
            var patientRepository = serviceProvider.GetService<IPatientRepository>();
            var doctorRepository = serviceProvider.GetService<IDoctorRepository>();
            var appointmentRepository = serviceProvider.GetService<IAppointmentRepository>();
            DataSeeder.Seed(patientRepository, doctorRepository, appointmentRepository);

            var patientService = serviceProvider.GetService<IPatientService>();
            var doctorService = serviceProvider.GetService<IDoctorService>();
            var appointmentService = serviceProvider.GetService<IAppointmentService>();
            var notificationService = serviceProvider.GetService<INotificationService>();

            while (true)
            {
                Console.WriteLine("\n--- San Vicente Hospital ---");
                Console.WriteLine("--- Patient Management ---");
                Console.WriteLine("1. Register Patient");
                Console.WriteLine("2. Edit Patient");
                Console.WriteLine("3. List All Patients");
                Console.WriteLine("--- Doctor Management ---");
                Console.WriteLine("4. Register Doctor");
                Console.WriteLine("5. Edit Doctor");
                Console.WriteLine("6. List All Doctors");
                Console.WriteLine("--- Appointment Management ---");
                Console.WriteLine("7. Schedule Appointment");
                Console.WriteLine("8. Cancel Appointment");
                Console.WriteLine("9. Mark Appointment as Attended");
                Console.WriteLine("10. View Appointments by Patient");
                Console.WriteLine("11. View Appointments by Doctor");
                Console.WriteLine("--- System ---");
                Console.WriteLine("12. Send Appointment Reminders");
                Console.WriteLine("13. Exit");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            RegisterPatient(patientService);
                            break;
                        case "2":
                            EditPatient(patientService);
                            break;
                        case "3":
                            ListAllPatients(patientService);
                            break;
                        case "4":
                            RegisterDoctor(doctorService);
                            break;
                        case "5":
                            EditDoctor(doctorService);
                            break;
                        case "6":
                            ListAllDoctors(doctorService);
                            break;
                        case "7":
                            ScheduleAppointment(appointmentService);
                            break;
                        case "8":
                            CancelAppointment(appointmentService);
                            break;
                        case "9":
                            MarkAppointmentAsAttended(appointmentService);
                            break;
                        case "10":
                            GetAppointmentsByPatient(appointmentService);
                            break;
                        case "11":
                            GetAppointmentsByDoctor(appointmentService);
                            break;
                        case "12":
                            notificationService.SendAppointmentReminders();
                            Console.WriteLine("Reminders sent successfully.");
                            break;
                        case "13":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void RegisterPatient(IPatientService patientService)
        {
            Console.Write("Patient Name: ");
            var name = Console.ReadLine();
            Console.Write("Patient Document ID: ");
            var document = Console.ReadLine();
            Console.Write("Patient Email: ");
            var email = Console.ReadLine();

            var patient = new Patient { Name = name, Document = document, Email = email };
            patientService.RegisterPatient(patient);
            Console.WriteLine("Patient registered successfully.");
        }

        private static void EditPatient(IPatientService patientService)
        {
            Console.Write("Enter the document ID of the patient to edit: ");
            var document = Console.ReadLine();
            var patient = patientService.FindPatientByDocument(document);

            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            Console.Write($"New name ({patient.Name}): ");
            var newName = Console.ReadLine();
            patient.Name = string.IsNullOrEmpty(newName) ? patient.Name : newName;

            Console.Write($"New email ({patient.Email}): ");
            var newEmail = Console.ReadLine();
            patient.Email = string.IsNullOrEmpty(newEmail) ? patient.Email : newEmail;

            patientService.UpdatePatient(patient);
            Console.WriteLine("Patient updated successfully.");
        }

        private static void ListAllPatients(IPatientService patientService)
        {
            var patients = patientService.GetAllPatients();
            if (!patients.Any())
            {
                Console.WriteLine("No patients registered.");
                return;
            }

            Console.WriteLine("\n--- All Patients ---");
            foreach (var p in patients)
            {
                Console.WriteLine($"- Name: {p.Name}, Document: {p.Document}, Email: {p.Email}");
            }
        }

        private static void RegisterDoctor(IDoctorService doctorService)
        {
            Console.Write("Doctor Name: ");
            var name = Console.ReadLine();
            Console.Write("Doctor Document ID: ");
            var document = Console.ReadLine();
            Console.Write("Doctor Specialty: ");
            var specialty = Console.ReadLine();

            var doctor = new Doctor { Name = name, Document = document, Specialty = specialty };
            doctorService.RegisterDoctor(doctor);
            Console.WriteLine("Doctor registered successfully.");
        }

        private static void EditDoctor(IDoctorService doctorService)
        {
            Console.Write("Enter the document ID of the doctor to edit: ");
            var document = Console.ReadLine();
            var doctor = doctorService.FindDoctorByDocument(document);

            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            Console.Write($"New name ({doctor.Name}): ");
            var newName = Console.ReadLine();
            doctor.Name = string.IsNullOrEmpty(newName) ? doctor.Name : newName;

            Console.Write($"New specialty ({doctor.Specialty}): ");
            var newSpecialty = Console.ReadLine();
            doctor.Specialty = string.IsNullOrEmpty(newSpecialty) ? doctor.Specialty : newSpecialty;

            doctorService.UpdateDoctor(doctor);
            Console.WriteLine("Doctor updated successfully.");
        }

        private static void ListAllDoctors(IDoctorService doctorService)
        {
            var doctors = doctorService.GetAllDoctors();
            if (!doctors.Any())
            {
                Console.WriteLine("No doctors registered.");
                return;
            }

            Console.WriteLine("\n--- All Doctors ---");
            foreach (var d in doctors)
            {
                Console.WriteLine($"- Name: {d.Name}, Document: {d.Document}, Specialty: {d.Specialty}");
            }
        }

        private static void ScheduleAppointment(IAppointmentService appointmentService)
        {
            Console.Write("Patient Document ID: ");
            var patientDocument = Console.ReadLine();

            Console.Write("Doctor Document ID: ");
            var doctorDocument = Console.ReadLine();

            Console.Write("Appointment Date (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.ScheduleAppointment(patientDocument, doctorDocument, date);
            Console.WriteLine("Appointment scheduled successfully.");
        }

        private static void CancelAppointment(IAppointmentService appointmentService)
        {
            Console.Write("Patient Document ID: ");
            var patientDocument = Console.ReadLine();

            Console.Write("Appointment Date (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.CancelAppointment(patientDocument, date);
            Console.WriteLine("Appointment canceled successfully.");
        }

        private static void MarkAppointmentAsAttended(IAppointmentService appointmentService)
        {
            Console.Write("Doctor Document ID: ");
            var doctorDocument = Console.ReadLine();

            Console.Write("Patient Document ID: ");
            var patientDocument = Console.ReadLine();

            Console.Write("Appointment Date (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.MarkAppointmentAsAttended(doctorDocument, patientDocument, date);
            Console.WriteLine("Appointment marked as attended.");
        }

        private static void GetAppointmentsByPatient(IAppointmentService appointmentService)
        {
            Console.Write("Patient Document ID: ");
            var patientDocument = Console.ReadLine();
            
            var appointments = appointmentService.GetAppointmentsByPatient(patientDocument);

            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found for this patient.");
                return;
            }

            Console.WriteLine($"Appointments for patient {patientDocument}:");
            foreach (var app in appointments)
            {
                Console.WriteLine($"- Date: {app.AppointmentDate}, Doctor: {app.Doctor.Name}, Status: {app.Status}");
            }
        }

        private static void GetAppointmentsByDoctor(IAppointmentService appointmentService)
        {
            Console.Write("Doctor Document ID: ");
            var doctorDocument = Console.ReadLine();
            
            var appointments = appointmentService.GetAppointmentsByDoctor(doctorDocument);

            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found for this doctor.");
                return;
            }

            Console.WriteLine($"Appointments for doctor {doctorDocument}:");
            foreach (var app in appointments)
            {
                Console.WriteLine($"- Date: {app.AppointmentDate}, Patient: {app.Patient.Name}, Status: {app.Status}");
            }
        }
    }
}
