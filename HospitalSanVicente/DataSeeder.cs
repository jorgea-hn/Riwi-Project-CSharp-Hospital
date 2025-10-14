using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;

namespace HospitalSanVicente
{
    public class DataSeeder
    {
        public static void Seed(IPatientRepository patientRepository, IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository)
        {
            // Seed Patients
            var patient1 = patientRepository.Create(new Patient { Name = "Juan Pérez", Document = "12345678", Email = "juan.perez@example.com" });
            var patient2 = patientRepository.Create(new Patient { Name = "Ana García", Document = "87654321", Email = "ana.garcia@example.com" });
            var patient3 = patientRepository.Create(new Patient { Name = "Luis Rodriguez", Document = "11122233", Email = "luis.rodriguez@example.com" });

            // Seed Doctors
            var doctor1 = doctorRepository.Create(new Doctor { Name = "Dr. Carlos López", Document = "11223344", Specialty = "Cardiología" });
            var doctor2 = doctorRepository.Create(new Doctor { Name = "Dra. Sofía Martínez", Document = "55667788", Specialty = "Pediatría" });
            var doctor3 = doctorRepository.Create(new Doctor { Name = "Dr. Andres Ramirez", Document = "99887766", Specialty = "Cardiología" });

            // Seed Appointments
            // Cita para Juan con Dr. Lopez (programada)
            appointmentRepository.Create(new Appointment 
            { 
                PatientId = patient1.Id, 
                DoctorId = doctor1.Id, 
                AppointmentDate = DateTime.Now.AddDays(2).Date.AddHours(9), 
                Status = AppointmentStatus.Scheduled 
            });

            // Cita para Ana con Dra. Martinez (programada)
            appointmentRepository.Create(new Appointment 
            { 
                PatientId = patient2.Id, 
                DoctorId = doctor2.Id, 
                AppointmentDate = DateTime.Now.AddDays(3).Date.AddHours(11), 
                Status = AppointmentStatus.Scheduled 
            });

            // Cita para Luis con Dr. Lopez (cancelada)
            appointmentRepository.Create(new Appointment
            {
                PatientId = patient3.Id,
                DoctorId = doctor1.Id,
                AppointmentDate = DateTime.Now.AddDays(1).Date.AddHours(14),
                Status = AppointmentStatus.Canceled
            });

            // Cita para Juan con Dra. Martinez (atendida)
            appointmentRepository.Create(new Appointment
            {
                PatientId = patient1.Id,
                DoctorId = doctor2.Id,
                AppointmentDate = DateTime.Now.AddDays(-5).Date.AddHours(10),
                Status = AppointmentStatus.Attended
            });

            Console.WriteLine("Datos de prueba cargados.");
        }
    }
}
