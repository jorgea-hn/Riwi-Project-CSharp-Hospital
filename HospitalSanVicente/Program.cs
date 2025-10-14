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

            while (true)
            {
                Console.WriteLine("\n--- San Vicente Hospital ---");
                Console.WriteLine("1. Patient Management");
                Console.WriteLine("2. Doctor Management");
                Console.WriteLine("3. Appointment Management");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            PatientManagementMenu(patientService);
                            break;
                        case "2":
                            DoctorManagementMenu(doctorService);
                            break;
                        case "3":
                            AppointmentManagementMenu(appointmentService);
                            break;
                        case "4":
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

        private static void PatientManagementMenu(IPatientService patientService)
        {
            while (true)
            {
                Console.WriteLine("\n--- Patient Management ---");
                Console.WriteLine("1. Register Patient");
                Console.WriteLine("2. Edit Patient");
                Console.WriteLine("3. List all Patients");
                Console.WriteLine("4. Back to main menu");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

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
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private static void DoctorManagementMenu(IDoctorService doctorService)
        {
             while (true)
            {
                Console.WriteLine("\n--- Doctor Management ---");
                Console.WriteLine("1. Register Doctor");
                Console.WriteLine("2. Edit Doctor");
                Console.WriteLine("3. List all Doctors");
                Console.WriteLine("4. List Doctors by Specialty");
                Console.WriteLine("5. Back to main menu");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterDoctor(doctorService);
                        break;
                    case "2":
                        EditDoctor(doctorService);
                        break;
                    case "3":
                        ListAllDoctors(doctorService, null);
                        break;
                    case "4":
                        Console.Write("Enter specialty: ");
                        var specialty = Console.ReadLine();
                        ListAllDoctors(doctorService, specialty);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        private static void AppointmentManagementMenu(IAppointmentService appointmentService)
        {
             while (true)
            {
                Console.WriteLine("\n--- Appointment Management ---");
                Console.WriteLine("1. Schedule Appointment");
                Console.WriteLine("2. Cancel Appointment");
                Console.WriteLine("3. Mark Appointment as Attended");
                Console.WriteLine("4. View Appointments by Patient");
                Console.WriteLine("5. View Appointments by Doctor");
                Console.WriteLine("6. Back to main menu");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ScheduleAppointment(appointmentService);
                        break;
                    case "2":
                        CancelAppointment(appointmentService);
                        break;
                    case "3":
                        MarkAppointmentAsAttended(appointmentService);
                        break;
                    case "4":
                        GetAppointmentsByPatient(appointmentService);
                        break;
                    case "5":
                        GetAppointmentsByDoctor(appointmentService);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }


        private static void RegisterPatient(IPatientService patientService)
        {
            Console.Write("Patient Name: ");
            var name = Console.ReadLine();
            Console.Write("Patient Document: ");
            var document = Console.ReadLine();
            Console.Write("Patient Email: ");
            var email = Console.ReadLine();

            var patient = new Patient { Name = name, Document = document, Email = email };
            patientService.RegisterPatient(patient);
            Console.WriteLine("Patient registered successfully.");
        }

        private static void EditPatient(IPatientService patientService)
        {
            Console.Write("Enter the document of the patient to edit: ");
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
            Console.Write("Doctor Document: ");
            var document = Console.ReadLine();
            Console.Write("Doctor Specialty: ");
            var specialty = Console.ReadLine();

            var doctor = new Doctor { Name = name, Document = document, Specialty = specialty };
            doctorService.RegisterDoctor(doctor);
            Console.WriteLine("Doctor registered successfully.");
        }

        private static void EditDoctor(IDoctorService doctorService)
        {
            Console.Write("Enter the document of the doctor to edit: ");
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

        private static void ListAllDoctors(IDoctorService doctorService, string specialty)
        {
            IEnumerable<Doctor> doctors;
            if (string.IsNullOrEmpty(specialty))
            {
                doctors = doctorService.GetAllDoctors();
                Console.WriteLine("\n--- All Doctors ---");
            }
            else
            {
                doctors = doctorService.FindDoctorsBySpecialty(specialty);
                Console.WriteLine($"\n--- Doctors with specialty: {specialty} ---");
            }

            if (!doctors.Any())
            {
                Console.WriteLine("No doctors found.");
                return;
            }

            foreach (var d in doctors)
            {
                Console.WriteLine($"- Name: {d.Name}, Document: {d.Document}, Specialty: {d.Specialty}");
            }
        }

        private static void ScheduleAppointment(IAppointmentService appointmentService)
        {
            Console.Write("Patient Document: ");
            var patientDocument = Console.ReadLine();

            Console.Write("Doctor Document: ");
            var doctorDocument = Console.ReadLine();

            Console.Write("Appointment Date (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.ScheduleAppointment(patientDocument, doctorDocument, date);
            Console.WriteLine("Appointment scheduled successfully.");
        }

        private static void CancelAppointment(IAppointmentService appointmentService)
        {
            Console.Write("Patient Document: ");
            var patientDocument = Console.ReadLine();

            Console.Write("Appointment Date (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.CancelAppointment(patientDocument, date);
            Console.WriteLine("Appointment canceled successfully.");
        }

        private static void MarkAppointmentAsAttended(IAppointmentService appointmentService)
        {
            Console.Write("Doctor Document: ");
            var doctorDocument = Console.ReadLine();

            Console.Write("Patient Document: ");
            var patientDocument = Console.ReadLine();

            Console.Write("Appointment Date (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.MarkAppointmentAsAttended(doctorDocument, patientDocument, date);
            Console.WriteLine("Appointment marked as attended.");
        }

        private static void GetAppointmentsByPatient(IAppointmentService appointmentService)
        {
            Console.Write("Patient Document: ");
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
            Console.Write("Doctor Document: ");
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
