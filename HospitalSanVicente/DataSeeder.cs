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

            // Seed Doctors
            var doctor1 = doctorRepository.Create(new Doctor { Name = "Dr. Carlos López", Document = "11223344", Specialty = "Cardiología" });
            var doctor2 = doctorRepository.Create(new Doctor { Name = "Dra. Sofía Martínez", Document = "55667788", Specialty = "Pediatría" });

            // Seed Appointments
            appointmentRepository.Create(new Appointment 
            { 
                PatientId = patient1.Id, 
                DoctorId = doctor1.Id, 
                AppointmentDate = DateTime.Now.AddDays(2).Date.AddHours(9), // In 2 days at 9 AM
                Status = AppointmentStatus.Scheduled 
            });

            appointmentRepository.Create(new Appointment 
            { 
                PatientId = patient2.Id, 
                DoctorId = doctor2.Id, 
                AppointmentDate = DateTime.Now.AddDays(3).Date.AddHours(11), // In 3 days at 11 AM
                Status = AppointmentStatus.Scheduled 
            });

            Console.WriteLine("Datos de prueba cargados.");
        }
    }
}
