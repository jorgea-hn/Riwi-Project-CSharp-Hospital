using HospitalSanVicente.Interfaces;
using HospitalSanVicente.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSanVicente.UI
{
    public class DoctorUI
    {
        private readonly IDoctorService _doctorService;

        public DoctorUI(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Doctor Management ---");
                Console.WriteLine("1. Register Doctor");
                Console.WriteLine("2. Edit Doctor");
                Console.WriteLine("3. List all Doctors");
                Console.WriteLine("4. List Doctors by Specialty");
                Console.WriteLine("5. Back to main menu");

                var choice = ConsoleReader.ReadMenuOption("Select an option: ", 5);

                switch (choice)
                {
                    case 1: RegisterDoctor(); break;
                    case 2: EditDoctor(); break;
                    case 3: ListAllDoctors(null); break;
                    case 4:
                        var specialty = ConsoleReader.ReadString("Enter specialty: ");
                        ListAllDoctors(specialty);
                        break;
                    case 5: return;
                }
            }
        }

        private void RegisterDoctor()
        {
            var name = ConsoleReader.ReadString("Doctor Name: ");
            var document = ConsoleReader.ReadString("Doctor Document: ");
            var specialty = ConsoleReader.ReadString("Doctor Specialty: ");

            var doctor = new Doctor { Name = name, Document = document, Specialty = specialty };
            _doctorService.Create(doctor);
            Console.WriteLine("Doctor registered successfully.");
        }

        private void EditDoctor()
        {
            ListAllDoctors(null);
            var document = ConsoleReader.ReadString("Enter the document of the doctor to edit: ");
            var doctor = _doctorService.Get(document);

            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            Console.Write($"New name (current: {doctor.Name}): ");
            var newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) doctor.Name = newName;

            Console.Write($"New specialty (current: {doctor.Specialty}): ");
            var newSpecialty = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSpecialty)) doctor.Specialty = newSpecialty;

            _doctorService.Update(doctor);
            Console.WriteLine("Doctor updated successfully.");
        }

        private void ListAllDoctors(string specialty)
        {
            var doctors = string.IsNullOrEmpty(specialty) ? _doctorService.GetAll() : _doctorService.FindDoctorsBySpecialty(specialty);
            var title = string.IsNullOrEmpty(specialty) ? "All Doctors" : $"Doctors with specialty: {specialty}";
            
            Console.WriteLine($"\n--- {title} ---");
            if (!doctors.Any())
            {
                Console.WriteLine("No doctors found.");
                return;
            }

            foreach (var d in doctors)
            {
                Console.WriteLine($"- Name: {d.Name}, Document: {d.Document}, Specialty: {d.Specialty}");
            }
        }
    }
}
