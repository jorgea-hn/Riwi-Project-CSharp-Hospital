using HospitalSanVicente.Interfaces;
using System;
using System.Linq;

namespace HospitalSanVicente.UI
{
    public class AppointmentUI
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public AppointmentUI(IAppointmentService appointmentService, IPatientService patientService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Appointment Management ---");
                Console.WriteLine("1. Schedule Appointment");
                Console.WriteLine("2. Cancel Appointment");
                Console.WriteLine("3. Mark Appointment as Attended");
                Console.WriteLine("4. View Appointments by Patient");
                Console.WriteLine("5. View Appointments by Doctor");
                Console.WriteLine("6. List All Appointments");
                Console.WriteLine("7. Back to main menu");

                var choice = ConsoleReader.ReadMenuOption("Select an option: ", 7);

                switch (choice)
                {
                    case 1: ScheduleAppointment(); break;
                    case 2: CancelAppointment(); break;
                    case 3: MarkAppointmentAsAttended(); break;
                    case 4: GetAppointmentsByPatient(); break;
                    case 5: GetAppointmentsByDoctor(); break;
                    case 6: ListAllAppointments(); break;
                    case 7: return;
                }
            }
        }

        private void ScheduleAppointment()
        {
            ListAllPatients();
            var patientDocument = ConsoleReader.ReadString("Patient Document: ");

            ListAllDoctors();
            var doctorDocument = ConsoleReader.ReadString("Doctor Document: ");

            var date = ConsoleReader.ReadDate("Appointment Date (yyyy-MM-dd HH:mm): ");

            _appointmentService.ScheduleAppointment(patientDocument, doctorDocument, date);
            Console.WriteLine("Appointment scheduled successfully.");
        }

        private void CancelAppointment()
        {
            ListAllPatients();
            var patientDocument = ConsoleReader.ReadString("Patient Document: ");
            var date = ConsoleReader.ReadDate("Appointment Date (yyyy-MM-dd HH:mm): ");

            _appointmentService.CancelAppointment(patientDocument, date);
            Console.WriteLine("Appointment canceled successfully.");
        }

        private void MarkAppointmentAsAttended()
        {
            ListAllDoctors();
            var doctorDocument = ConsoleReader.ReadString("Doctor Document: ");

            ListAllPatients();
            var patientDocument = ConsoleReader.ReadString("Patient Document: ");

            var date = ConsoleReader.ReadDate("Appointment Date (yyyy-MM-dd HH:mm): ");

            _appointmentService.MarkAppointmentAsAttended(doctorDocument, patientDocument, date);
            Console.WriteLine("Appointment marked as attended.");
        }

        private void GetAppointmentsByPatient()
        {
            ListAllPatients();
            var patientDocument = ConsoleReader.ReadString("Patient Document: ");
            
            var appointments = _appointmentService.GetAppointmentsByPatient(patientDocument);

            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found for this patient.");
                return;
            }

            Console.WriteLine($"\nAppointments for patient {patientDocument}:");
            foreach (var app in appointments)
            {
                Console.WriteLine($"- Date: {app.AppointmentDate}, Doctor: {app.Doctor.Name}, Status: {app.Status}");
            }
        }

        private void GetAppointmentsByDoctor()
        {
            ListAllDoctors();
            var doctorDocument = ConsoleReader.ReadString("Doctor Document: ");
            
            var appointments = _appointmentService.GetAppointmentsByDoctor(doctorDocument);

            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found for this doctor.");
                return;
            }

            Console.WriteLine($"\nAppointments for doctor {doctorDocument}:");
            foreach (var app in appointments)
            {
                Console.WriteLine($"- Date: {app.AppointmentDate}, Patient: {app.Patient.Name}, Status: {app.Status}");
            }
        }

        private void ListAllAppointments()
        {
            var appointments = _appointmentService.GetAllAppointments();

            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found.");
                return;
            }

            Console.WriteLine("\n--- All Appointments ---");
            foreach (var app in appointments.OrderBy(a => a.AppointmentDate))
            {
                Console.WriteLine($"- Date: {app.AppointmentDate}, Patient: {app.Patient.Name}, Doctor: {app.Doctor.Name}, Status: {app.Status}");
            }
        }

        private void ListAllPatients()
        {
            var patients = _patientService.GetAll();
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

        private void ListAllDoctors()
        {
            var doctors = _doctorService.GetAll();
            if (!doctors.Any())
            {
                Console.WriteLine("No doctors found.");
                return;
            }

            Console.WriteLine("\n--- All Doctors ---");
            foreach (var d in doctors)
            {
                Console.WriteLine($"- Name: {d.Name}, Document: {d.Document}, Specialty: {d.Specialty}");
            }
        }
    }
}
