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
            var patient4 = patientRepository.Create(new Patient { Name = "Laura Torres", Document = "44556677", Email = "laura.torres@example.com" });
            var patient5 = patientRepository.Create(new Patient { Name = "Miguel Castro", Document = "88990011", Email = "miguel.castro@example.com" });
            var patient6 = patientRepository.Create(new Patient { Name = "Isabel Reyes", Document = "22334455", Email = "isabel.reyes@example.com" });

            // Seed Doctors
            var doctor1 = doctorRepository.Create(new Doctor { Name = "Dr. Carlos López", Document = "11223344", Specialty = "Cardiología" });
            var doctor2 = doctorRepository.Create(new Doctor { Name = "Dra. Sofía Martínez", Document = "55667788", Specialty = "Pediatría" });
            var doctor3 = doctorRepository.Create(new Doctor { Name = "Dr. Andres Ramirez", Document = "99887766", Specialty = "Cardiología" });
            var doctor4 = doctorRepository.Create(new Doctor { Name = "Dr. Elena Vargas", Document = "12121212", Specialty = "Dermatología" });
            var doctor5 = doctorRepository.Create(new Doctor { Name = "Dr. Ricardo Peña", Document = "34343434", Specialty = "Oncología" });
            var doctor6 = doctorRepository.Create(new Doctor { Name = "Dra. Carmen Soto", Document = "56565656", Specialty = "Neurología" });
            var doctor7 = doctorRepository.Create(new Doctor { Name = "Dr. Jorge Núñez", Document = "78787878", Specialty = "Dermatología" });


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

            // Cita para Laura con Dr. Vargas (programada)
            appointmentRepository.Create(new Appointment
            {
                PatientId = patient4.Id,
                DoctorId = doctor4.Id,
                AppointmentDate = DateTime.Now.AddDays(4).Date.AddHours(15),
                Status = AppointmentStatus.Scheduled
            });

            // Cita para Miguel con Dr. Peña (programada)
            appointmentRepository.Create(new Appointment
            {
                PatientId = patient5.Id,
                DoctorId = doctor5.Id,
                AppointmentDate = DateTime.Now.AddDays(5).Date.AddHours(10),
                Status = AppointmentStatus.Scheduled
            });

            // Cita para Isabel con Dra. Soto (atendida)
            appointmentRepository.Create(new Appointment
            {
                PatientId = patient6.Id,
                DoctorId = doctor6.Id,
                AppointmentDate = DateTime.Now.AddDays(-2).Date.AddHours(16),
                Status = AppointmentStatus.Attended
            });

             // Cita para Juan con Dr. Núñez (cancelada)
            appointmentRepository.Create(new Appointment
            {
                PatientId = patient1.Id,
                DoctorId = doctor7.Id,
                AppointmentDate = DateTime.Now.AddDays(6).Date.AddHours(11),
                Status = AppointmentStatus.Canceled
            });

            // Cita para Ana con Dr. Vargas (programada)
            appointmentRepository.Create(new Appointment
            {
                PatientId = patient2.Id,
                DoctorId = doctor4.Id,
                AppointmentDate = DateTime.Now.AddDays(7).Date.AddHours(9),
                Status = AppointmentStatus.Scheduled
            });

            Console.WriteLine("Test data loaded.");
        }
    }
}
