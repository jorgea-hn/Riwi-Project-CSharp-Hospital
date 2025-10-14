using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Linq;

namespace HospitalSanVicente.UI
{
    public class PatientUI
    {
        private readonly IPatientService _patientService;

        public PatientUI(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Patient Management ---");
                Console.WriteLine("1. Register Patient");
                Console.WriteLine("2. Edit Patient");
                Console.WriteLine("3. List all Patients");
                Console.WriteLine("4. Back to main menu");

                var choice = ConsoleReader.ReadMenuOption("Select an option: ", 4);

                switch (choice)
                {
                    case 1: RegisterPatient(); break;
                    case 2: EditPatient(); break;
                    case 3: ListAllPatients(); break;
                    case 4: return;
                }
            }
        }

        private void RegisterPatient()
        {
            var name = ConsoleReader.ReadString("Patient Name: ");
            var document = ConsoleReader.ReadString("Patient Document: ");
            var email = ConsoleReader.ReadString("Patient Email: ");

            var patient = new Patient { Name = name, Document = document, Email = email };
            _patientService.Create(patient);
            Console.WriteLine("Patient registered successfully.");
        }

        private void EditPatient()
        {
            ListAllPatients();
            var document = ConsoleReader.ReadString("Enter the document of the patient to edit: ");
            var patient = _patientService.Get(document);

            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            Console.Write($"New name (current: {patient.Name}): ");
            var newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) patient.Name = newName;

            Console.Write($"New email (current: {patient.Email}): ");
            var newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail)) patient.Email = newEmail;

            _patientService.Update(patient);
            Console.WriteLine("Patient updated successfully.");
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
    }
}
