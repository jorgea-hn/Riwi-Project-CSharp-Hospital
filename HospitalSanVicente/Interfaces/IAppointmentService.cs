using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalSanVicente.Interfaces
{
    /// <summary>
    /// Defines the contract for managing patient appointments.
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// Schedules a new appointment and sends a confirmation email.
        /// </summary>
        /// <param name="patientDocument">The document ID of the patient.</param>
        /// <param name="doctorDocument">The document ID of the doctor.</param>
        /// <param name="date">The date and time of the appointment.</param>
        /// <returns>A Task that resolves to the created Appointment object.</returns>
        Task<Appointment> ScheduleAppointment(string patientDocument, string doctorDocument, DateTime date);

        /// <summary>
        /// Cancels an existing appointment.
        /// </summary>
        /// <param name="patientDocument">The document ID of the patient.</param>
        /// <param name="date">The date and time of the appointment to cancel.</param>
        /// <returns>The updated Appointment object with a 'Canceled' status.</returns>
        Appointment CancelAppointment(string patientDocument, DateTime date);

        /// <summary>
        /// Marks an appointment as attended.
        /// </summary>
        /// <param name="doctorDocument">The document ID of the doctor who attended the appointment.</param>
        /// <param name="patientDocument">The document ID of the patient.</param>
        /// <param name="date">The date and time of the appointment.</param>
        /// <returns>The updated Appointment object with an 'Attended' status.</returns>
        Appointment MarkAppointmentAsAttended(string doctorDocument, string patientDocument, DateTime date);

        /// <summary>
        /// Retrieves all appointments for a specific patient.
        /// </summary>
        /// <param name="patientDocument">The document ID of the patient.</param>
        /// <returns>An enumerable collection of the patient's appointments.</returns>
        IEnumerable<Appointment> GetAppointmentsByPatient(string patientDocument);

        /// <summary>
        /// Retrieves all appointments for a specific doctor.
        /// </summary>
        /// <param name="doctorDocument">The document ID of the doctor.</param>
        /// <returns>An enumerable collection of the doctor's appointments.</returns>
        IEnumerable<Appointment> GetAppointmentsByDoctor(string doctorDocument);

        /// <summary>
        /// Retrieves all appointments in the system.
        /// </summary>
        /// <returns>An enumerable collection of all appointments.</returns>
        IEnumerable<Appointment> GetAllAppointments();
    }
}
