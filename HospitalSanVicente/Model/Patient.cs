using System;

namespace HospitalSanVicente.Model
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
    }
}
