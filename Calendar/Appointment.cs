using System;
namespace Calendar
{
    public class Appointment
    {
        public enum AppointmentType
        {
            Vaccine = 1,
            Vermifuge = 2, 
            Ectoparasite = 3,
            Medicine = 4,
            Bath = 5,
            Veterinarian = 6,
            Shopping = 7,
            Other = 8
        }

        public Appointment(AppointmentType type)
        {
            this.Type = type;
        }

        public AppointmentType Type { get; private set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentDescription { get; set; }
    }
}
