using System;

namespace HospitalSanVicente.Model
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Specialty { get; set; }
    }
}
