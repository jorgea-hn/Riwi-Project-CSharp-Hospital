using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using Microsoft.Extensions.DependencyInjection;
using System;

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
                Console.WriteLine("\n--- Hospital San Vicente ---");
                Console.WriteLine("1. Registrar Paciente");
                Console.WriteLine("2. Registrar Doctor");
                Console.WriteLine("3. Programar Cita");
                Console.WriteLine("4. Cancelar Cita");
                Console.WriteLine("5. Marcar Cita como Atendida");
                Console.WriteLine("6. Ver Citas por Paciente");
                Console.WriteLine("7. Ver Citas por Doctor");
                Console.WriteLine("8. Enviar Recordatorios de Citas");
                Console.WriteLine("9. Salir");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            RegisterPatient(patientService);
                            break;
                        case "2":
                            RegisterDoctor(doctorService);
                            break;
                        case "3":
                            ScheduleAppointment(appointmentService, patientService, doctorService);
                            break;
                        case "4":
                            CancelAppointment(appointmentService, patientService);
                            break;
                        case "5":
                            MarkAppointmentAsAttended(appointmentService, doctorService, patientService);
                            break;
                        case "6":
                            GetAppointmentsByPatient(appointmentService, patientService);
                            break;
                        case "7":
                            GetAppointmentsByDoctor(appointmentService, doctorService);
                            break;
                        case "8":
                            notificationService.SendAppointmentReminders();
                            Console.WriteLine("Recordatorios enviados.");
                            break;
                        case "9":
                            return;
                        default:
                            Console.WriteLine("Opción no válida");
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
            Console.Write("Nombre del Paciente: ");
            var name = Console.ReadLine();
            Console.Write("Documento del Paciente: ");
            var document = Console.ReadLine();
            Console.Write("Email del Paciente: ");
            var email = Console.ReadLine();

            var patient = new Patient { Name = name, Document = document, Email = email };
            patientService.RegisterPatient(patient);
            Console.WriteLine("Paciente registrado exitosamente.");
        }

        private static void RegisterDoctor(IDoctorService doctorService)
        {
            Console.Write("Nombre del Doctor: ");
            var name = Console.ReadLine();
            Console.Write("Documento del Doctor: ");
            var document = Console.ReadLine();
            Console.Write("Especialidad del Doctor: ");
            var specialty = Console.ReadLine();

            var doctor = new Doctor { Name = name, Document = document, Specialty = specialty };
            doctorService.RegisterDoctor(doctor);
            Console.WriteLine("Doctor registrado exitosamente.");
        }

        private static void ScheduleAppointment(IAppointmentService appointmentService, IPatientService patientService, IDoctorService doctorService)
        {
            Console.Write("Documento del Paciente: ");
            var patientDocument = Console.ReadLine();
            var patient = patientService.GetPatientByDocument(patientDocument);
            if (patient == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }

            Console.Write("Documento del Doctor: ");
            var doctorDocument = Console.ReadLine();
            var doctor = doctorService.GetDoctorByDocument(doctorDocument);
            if (doctor == null)
            {
                Console.WriteLine("Doctor no encontrado.");
                return;
            }

            Console.Write("Fecha de la Cita (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.ScheduleAppointment(patient.Id, doctor.Id, date);
            Console.WriteLine("Cita programada exitosamente.");
        }

        private static void CancelAppointment(IAppointmentService appointmentService, IPatientService patientService)
        {
            Console.Write("Documento del Paciente: ");
            var patientDocument = Console.ReadLine();
            var patient = patientService.GetPatientByDocument(patientDocument);
            if (patient == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }

            Console.Write("Fecha de la Cita (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.CancelAppointment(patient.Id, date);
            Console.WriteLine("Cita cancelada exitosamente.");
        }

        private static void MarkAppointmentAsAttended(IAppointmentService appointmentService, IDoctorService doctorService, IPatientService patientService)
        {
            Console.Write("Documento del Doctor: ");
            var doctorDocument = Console.ReadLine();
            var doctor = doctorService.GetDoctorByDocument(doctorDocument);
            if (doctor == null)
            {
                Console.WriteLine("Doctor no encontrado.");
                return;
            }

            Console.Write("Documento del Paciente: ");
            var patientDocument = Console.ReadLine();
            var patient = patientService.GetPatientByDocument(patientDocument);
            if (patient == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }

            Console.Write("Fecha de la Cita (yyyy-MM-dd HH:mm): ");
            var date = DateTime.Parse(Console.ReadLine());

            appointmentService.MarkAppointmentAsAttended(doctor.Id, patient.Id, date);
            Console.WriteLine("Cita marcada como atendida.");
        }

        private static void GetAppointmentsByPatient(IAppointmentService appointmentService, IPatientService patientService)
        {
            Console.Write("Documento del Paciente: ");
            var patientDocument = Console.ReadLine();
            var patient = patientService.GetPatientByDocument(patientDocument);
            if (patient == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }

            var appointments = appointmentService.GetAppointmentsByPatient(patient.Id);

            foreach (var app in appointments)
            {
                Console.WriteLine($"- Cita el {app.AppointmentDate} con el Dr. {app.Doctor.Name}, Estado: {app.Status}");
            }
        }

        private static void GetAppointmentsByDoctor(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            Console.Write("Documento del Doctor: ");
            var doctorDocument = Console.ReadLine();
            var doctor = doctorService.GetDoctorByDocument(doctorDocument);
            if (doctor == null)
            {
                Console.WriteLine("Doctor no encontrado.");
                return;
            }

            var appointments = appointmentService.GetAppointmentsByDoctor(doctor.Id);

            foreach (var app in appointments)
            {
                Console.WriteLine($"- Cita el {app.AppointmentDate} con {app.Patient.Name}, Estado: {app.Status}");
            }
        }
    }
}
