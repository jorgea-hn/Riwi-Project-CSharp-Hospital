using HospitalSanVicente.Model;
using System.Collections.Generic;

namespace HospitalSanVicente.Interfaces
{
    public interface IPatientService
    {
        Patient RegisterPatient(Patient patient);
        Patient FindPatientByDocument(string document);
        IEnumerable<Patient> GetAllPatients();
        Patient UpdatePatient(Patient patient);
    }
}
