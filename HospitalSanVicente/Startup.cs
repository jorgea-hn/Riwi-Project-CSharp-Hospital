using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Repository;
using HospitalSanVicente.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace HospitalSanVicente
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Secret.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // --- Configuración ---
            services.AddSingleton(Configuration);

            // --- Repositories ---
            services.AddSingleton<IPatientRepository, PatientRepository>();
            services.AddSingleton<IDoctorRepository, DoctorRepository>();
            services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
            services.AddSingleton<IEmailNotificationRepository, EmailNotificationRepository>();

            // --- Email Services Registration ---
            // 1. Registra las implementaciones concretas que usaremos.
            services.AddSingleton<SendGridEmailService>(); // El servicio real
            services.AddSingleton<EmailService>();       // El servicio de consola

            // 2. Registra el "director de orquesta" como la implementación principal de IEmailService.
            // Cuando se pida un IEmailService, se entregará el CombinedEmailService.
            // El sistema automáticamente le pasará los dos servicios de arriba.
            services.AddSingleton<IEmailService, CombinedEmailService>();

            // --- Other Core Services ---
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<IDoctorService, DoctorService>();
            services.AddSingleton<IEmailNotificationService, EmailNotificationService>();
            services.AddSingleton<IAppointmentService, AppointmentService>();
        }
    }
}
