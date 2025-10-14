using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Repository;
using HospitalSanVicente.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalSanVicente
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Repositories
            services.AddSingleton<IPatientRepository, PatientRepository>();
            services.AddSingleton<IDoctorRepository, DoctorRepository>();
            services.AddSingleton<IAppointmentRepository, AppointmentRepository>();

            // Services
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<IDoctorService, DoctorService>();
            services.AddSingleton<IAppointmentService, AppointmentService>();
        }
    }
}
