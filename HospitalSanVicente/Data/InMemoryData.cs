using HospitalSanVicente.Model;
using System.Collections.Generic;

namespace HospitalSanVicente.Data
{
    public static class InMemoryData
    {
        public static readonly List<Patient> Patients = new List<Patient>();
        public static readonly List<Doctor> Doctors = new List<Doctor>();
        public static readonly List<Appointment> Appointments = new List<Appointment>();
        // public static readonly List<EmailLog> EmailLogs = new List<EmailLog>();
    }
}
