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
                Console.WriteLine("\n--- Hospital San Vicente ---");
                Console.WriteLine("1. Gestión de Pacientes");
                Console.WriteLine("2. Gestión de Médicos");
                Console.WriteLine("3. Gestión de Citas");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");

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
                            Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
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
                Console.WriteLine("\n--- Gestión de Pacientes ---");
                Console.WriteLine("1. Registrar Paciente");
                Console.WriteLine("2. Editar Paciente");
                Console.WriteLine("3. Listar todos los Pacientes");
                Console.WriteLine("4. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

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
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        private static void DoctorManagementMenu(IDoctorService doctorService)
        {
             while (true)
            {
                Console.WriteLine("\n--- Gestión de Médicos ---");
                Console.WriteLine("1. Registrar Médico");
                Console.WriteLine("2. Editar Médico");
                Console.WriteLine("3. Listar todos los Médicos");
                Console.WriteLine("4. Listar Médicos por Especialidad");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

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
                        Console.Write("Ingrese la especialidad: ");
                        var specialty = Console.ReadLine();
                        ListAllDoctors(doctorService, specialty);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        private static void AppointmentManagementMenu(IAppointmentService appointmentService)
        {
             while (true)
            {
                Console.WriteLine("\n--- Gestión de Citas ---");
                Console.WriteLine("1. Agendar Cita");
                Console.WriteLine("2. Cancelar Cita");
                Console.WriteLine("3. Marcar Cita como Atendida");
                Console.WriteLine("4. Ver Citas por Paciente");
                Console.WriteLine("5. Ver Citas por Médico");
                Console.WriteLine("6. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

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
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }


        private static void RegisterPatient(IPatientService patientService)
        {
            Console.Write("Nombre del Paciente: ");
            var name = Console.ReadLine();
            Console.Write("Documento del Paciente: ");
            var document = Console.ReadLine();
            Console.Write("Email del Paciente: ");
            var email = Console.ReadLine();

            var patient = new Patient { Name = name, Document = document, Email = email };
            patientService.RegisterPatient(patient);
            Console.WriteLine("Paciente registrado con éxito.");
        }

        private static void EditPatient(IPatientService patientService)
        {
            Console.Write("Ingrese el documento del paciente a editar: ");
            var document = Console.ReadLine();
            var patient = patientService.FindPatientByDocument(document);

            if (patient == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }

            Console.Write($"Nuevo nombre ({patient.Name}): ");
            var newName = Console.ReadLine();
            patient.Name = string.IsNullOrEmpty(newName) ? patient.Name : newName;

            Console.Write($"Nuevo email ({patient.Email}): ");
            var newEmail = Console.ReadLine();
            patient.Email = string.IsNullOrEmpty(newEmail) ? patient.Email : newEmail;

            patientService.UpdatePatient(patient);
            Console.WriteLine("Paciente actualizado con éxito.");
        }

        private static void ListAllPatients(IPatientService patientService)
        {
            var patients = patientService.GetAllPatients();
            if (!patients.Any())
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            Console.WriteLine("\n--- Todos los Pacientes ---");
            foreach (var p in patients)
            {
                Console.WriteLine($"- Nombre: {p.Name}, Documento: {p.Document}, Email: {p.Email}");
            }
        }

        private static void RegisterDoctor(IDoctorService doctorService)
        {
            Console.Write("Nombre del Médico: ");
            var name = Console.ReadLine();
            Console.Write("Documento del Médico: ");
            var document = Console.ReadLine();
            Console.Write("Especialidad del Médico: ");
            var specialty = Console.ReadLine();

            var doctor = new Doctor { Name = name, Document = document, Specialty = specialty };
            doctorService.RegisterDoctor(doctor);
            Console.WriteLine("Médico registrado con éxito.");
        }

        private static void EditDoctor(IDoctorService doctorService)
        {
            Console.Write("Ingrese el documento del médico a editar: ");
            var document = Console.ReadLine();
            var doctor = doctorService.FindDoctorByDocument(document);

            if (doctor == null)
            {
                Console.WriteLine("Médico no encontrado.");
                return;
            }

            Console.Write($"Nuevo nombre ({doctor.Name}): ");
            var newName = Console.ReadLine();
            doctor.Name = string.IsNullOrEmpty(newName) ? doctor.Name : newName;

            Console.Write($"Nueva especialidad ({doctor.Specialty}): ");
            var newSpecialty = Console.ReadLine();
            doctor.Specialty = string.IsNullOrEmpty(newSpecialty) ? doctor.Specialty : newSpecialty;

            doctorService.UpdateDoctor(doctor);
            Console.WriteLine("Médico actualizado con éxito.");
        }

        private static void ListAllDoctors(IDoctorService doctorService, string specialty)
        {
            IEnumerable<Doctor> doctors;
            if (string.IsNullOrEmpty(specialty))
            {
                doctors = doctorService.GetAllDoctors();
                Console.WriteLine("\n--- Todos los Médicos ---");
            }
            else
            {
                doctors = doctorService.FindDoctorsBySpecialty(specialty);
                Console.WriteLine($"\n--- Médicos con especialidad: {specialty} ---");
            }

            if (!doctors.Any())
            {
                Console.WriteLine("No se encontraron médicos.");
                return;
            }

            foreach (var d in doctors)
            {
                Console.WriteLine($"- Nombre: {d.Name}, Documento: {d.Document}, Especialidad: {d.Specialty}");
            }
        }

        private static void ScheduleAppointment(IAppointmentService appointmentService)
        {
            Console.Write("Documento del Paciente: ");
            var patientDocument = Console.ReadLine();

            Console.Write("Documento del Médico: ");
            var doctorDocument = Console.ReadLine();

            Console.Write("Fecha de la Cita (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.ScheduleAppointment(patientDocument, doctorDocument, date);
            Console.WriteLine("Cita agendada con éxito.");
        }

        private static void CancelAppointment(IAppointmentService appointmentService)
        {
            Console.Write("Documento del Paciente: ");
            var patientDocument = Console.ReadLine();

            Console.Write("Fecha de la Cita (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.CancelAppointment(patientDocument, date);
            Console.WriteLine("Cita cancelada con éxito.");
        }

        private static void MarkAppointmentAsAttended(IAppointmentService appointmentService)
        {
            Console.Write("Documento del Médico: ");
            var doctorDocument = Console.ReadLine();

            Console.Write("Documento del Paciente: ");
            var patientDocument = Console.ReadLine();

            Console.Write("Fecha de la Cita (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.MarkAppointmentAsAttended(doctorDocument, patientDocument, date);
            Console.WriteLine("Cita marcada como atendida.");
        }

        private static void GetAppointmentsByPatient(IAppointmentService appointmentService)
        {
            Console.Write("Documento del Paciente: ");
            var patientDocument = Console.ReadLine();
            
            var appointments = appointmentService.GetAppointmentsByPatient(patientDocument);

            if (!appointments.Any())
            {
                Console.WriteLine("No se encontraron citas para este paciente.");
                return;
            }

            Console.WriteLine($"Citas para el paciente {patientDocument}:");
            foreach (var app in appointments)
            {
                Console.WriteLine($"- Fecha: {app.AppointmentDate}, Médico: {app.Doctor.Name}, Estado: {app.Status}");
            }
        }

        private static void GetAppointmentsByDoctor(IAppointmentService appointmentService)
        {
            Console.Write("Documento del Médico: ");
            var doctorDocument = Console.ReadLine();
            
            var appointments = appointmentService.GetAppointmentsByDoctor(doctorDocument);

            if (!appointments.Any())
            {
                Console.WriteLine("No se encontraron citas para este médico.");
                return;
            }

            Console.WriteLine($"Citas para el médico {doctorDocument}:");
            foreach (var app in appointments)
            {
                Console.WriteLine($"- Fecha: {app.AppointmentDate}, Paciente: {app.Patient.Name}, Estado: {app.Status}");
            }
        }
    }
}
