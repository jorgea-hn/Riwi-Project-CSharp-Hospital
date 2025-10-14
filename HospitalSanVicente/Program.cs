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

            MainMenu(patientService, doctorService, appointmentService);
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
        private static void MainMenu(IPatientService patientService, IDoctorService doctorService, IAppointmentService appointmentService)
        {
            var patientUI = new PatientUI(patientService);
            var doctorUI = new DoctorUI(doctorService);
            var appointmentUI = new AppointmentUI(appointmentService, patientService, doctorService);

            while (true)
            {
                Console.WriteLine("\n--- San Vicente Hospital ---");
                Console.WriteLine("1. Patient Management");
                Console.WriteLine("2. Doctor Management");
                Console.WriteLine("3. Appointment Management");
                Console.WriteLine("4. Exit");
                
                var choice = ConsoleReader.ReadMenuOption("Select an option: ", 4);

                try
                {
                    switch (choice)
                    {
                        case 1: patientUI.ShowMenu(); break;
                        case 2: doctorUI.ShowMenu(); break;
                        case 3: appointmentUI.ShowMenu(); break;
                        case 4: return;
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
