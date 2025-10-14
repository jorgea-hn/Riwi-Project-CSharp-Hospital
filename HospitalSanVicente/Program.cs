using HospitalSanVicente.Interfaces;
using HospitalSanVicente.UI;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HospitalSanVicente
{
    class Program
    {
        // Main Execution Block
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            SeedData(serviceProvider);

            var patientService = serviceProvider.GetService<IPatientService>();
            var doctorService = serviceProvider.GetService<IDoctorService>();
            var appointmentService = serviceProvider.GetService<IAppointmentService>();
            var emailNotificationService = serviceProvider.GetService<IEmailNotificationService>();

            MainMenu(patientService, doctorService, appointmentService, emailNotificationService);
        }

        // Dependency Injection and Seeding
        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);
            return services.BuildServiceProvider();
        }

        private static void SeedData(ServiceProvider serviceProvider)
        {
            var patientRepository = serviceProvider.GetService<IPatientRepository>();
            var doctorRepository = serviceProvider.GetService<IDoctorRepository>();
            var appointmentRepository = serviceProvider.GetService<IAppointmentRepository>();
            DataSeeder.Seed(patientRepository, doctorRepository, appointmentRepository);
        }
        
        // --- Main Menu ---
        private static void MainMenu(IPatientService patientService, IDoctorService doctorService, IAppointmentService appointmentService, IEmailNotificationService emailNotificationService)
        {
            var patientUI = new PatientUI(patientService);
            var doctorUI = new DoctorUI(doctorService);
            var appointmentUI = new AppointmentUI(appointmentService, patientService, doctorService);
            var emailNotificationUI = new EmailNotificationUI(emailNotificationService);

            while (true)
            {
                Console.WriteLine("\n--- San Vicente Hospital ---");
                Console.WriteLine("1. Patient Management");
                Console.WriteLine("2. Doctor Management");
                Console.WriteLine("3. Appointment Management");
                Console.WriteLine("4. View Notification History");
                Console.WriteLine("5. Exit");
                
                var choice = ConsoleReader.ReadMenuOption("Select an option: ", 5);

                try
                {
                    switch (choice)
                    {
                        case 1: patientUI.ShowMenu(); break;
                        case 2: doctorUI.ShowMenu(); break;
                        case 3: appointmentUI.ShowMenu(); break;
                        case 4: emailNotificationUI.ShowNotificationHistory(); break;
                        case 5: return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }
    }
}
