using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Calendar
{
    public partial class MainTestPage : ContentPage
    {
        public MainTestPage()
        {
            InitializeComponent();

            CalendarCtrl.AddAppointment(new Appointment(Appointment.AppointmentType.Bath));
            CalendarCtrl.AddAppointment(new Appointment(Appointment.AppointmentType.Medicine));
            CalendarCtrl.AddAppointment(new Appointment(Appointment.AppointmentType.Shopping));
            CalendarCtrl.AddAppointment(new Appointment(Appointment.AppointmentType.Veterinarian));
        }
    }
}
