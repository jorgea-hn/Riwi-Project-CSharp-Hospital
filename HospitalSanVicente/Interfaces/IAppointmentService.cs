using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalSanVicente.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment> ScheduleAppointment(string patientDocument, string doctorDocument, DateTime date);
        Appointment CancelAppointment(string patientDocument, DateTime date);
        Appointment MarkAppointmentAsAttended(string doctorDocument, string patientDocument, DateTime date);
        IEnumerable<Appointment> GetAppointmentsByPatient(string patientDocument);
        IEnumerable<Appointment> GetAppointmentsByDoctor(string doctorDocument);
        IEnumerable<Appointment> GetAllAppointments();
    }
}
