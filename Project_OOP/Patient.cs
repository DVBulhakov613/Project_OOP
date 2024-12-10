using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP
{
    public class Patient : IPerson
    {
        #region Properties - Person info
        public int ID
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public static IDManagement IDManager = new IDManagement();
        public string FirstName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public string MiddleName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public string LastName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public DateTime BirthDate
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public Hospital CurrentHospital
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        #endregion

        #region Properties - Appointment info
        private int nextID { get; set; }
        public Dictionary<string, MedicalRecord> MedicalHistory { get; private set; }
        public AppointmentSchedule Schedule
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        #endregion

        #region Methods - Patient-specific methods
        public Patient(string firstName, string middleName, string lastName, DateTime birthDate, Hospital hospital, List<MedicalRecord>? medicalHistory = null)
        { throw new NotImplementedException(); }

        public string GenerateCompositeID()
        { throw new NotImplementedException(); }

        public void AddMedicalRecord(List<Staff> staff, List<string> diagnoses, List<string> treatments, List<string> medications, Hospital hospital)
        { throw new NotImplementedException(); }
        public void AddAppointment(int roomID, DateTime time, List<int> staffInvolved, AppointmentPurpose purpose)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Inherited from Person interface
        public string GetFullName()
        { throw new NotImplementedException(); }

        public void ChangeFirstName(string firstName)
        { throw new NotImplementedException(); }

        public void ChangeMiddleName(string middleName)
        { throw new NotImplementedException(); }

        public void ChangeLastName(string lastName)
        { throw new NotImplementedException(); }

        public void ChangeBirthDate(DateTime birthDate)
        { throw new NotImplementedException(); }

        public override string ToString()
        { throw new NotImplementedException(); }
        #endregion
    }
}
