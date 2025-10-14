using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;

namespace HospitalSanVicente.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        IEnumerable<Appointment> GetByPatient(Guid patientId);
        IEnumerable<Appointment> GetByDoctor(Guid doctorId);
        Appointment GetByPatientAndDate(Guid patientId, DateTime date);
    }
}
