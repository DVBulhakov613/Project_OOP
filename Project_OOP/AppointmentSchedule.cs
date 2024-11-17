using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP
{
    public class AppointmentSchedule
    {
        private static IDManagement idManager = new IDManagement();
        public Dictionary<int, Appointment> Appointments 
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); } 
        }
        #region Methods
        public void CreateAppointment(int room, DateTime time, List<Staff> staffInvolved, Patient appointee, AppointmentPurpose purpose)
        { throw new NotImplementedException(); }

        public string CancelAppointment(int appID)
        { throw new NotImplementedException(); }

        public Appointment GetAppointment(int appID)
        { throw new NotImplementedException(); }

        public override string ToString()
        { throw new NotImplementedException(); }
        #endregion
    }
}
