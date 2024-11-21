using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP
{
    public class Hospital
    {
        #region Properties
        private int ID;
        public string Name
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public string Location
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public int Capacity
        {
            get { throw new NotImplementedException(); }
        }
        public List<Department> Departments
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<int> Rooms
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<Patient> Patient
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<Staff> ActiveStaff
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        #endregion

        #region Methods
        public Hospital(int _ID, string name, string location, List<int> rooms, List<Department>? departments = null, List<Patient>? patient = null, List<Staff>? activeStaff = null)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Room-related
        public void AddRoom(int roomID)
        { throw new NotImplementedException(); }
        public void AddRooms(List<int> rooms)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Department-related
        public void AddDepartment(string name, Staff? head, List<int> rooms)
        { throw new NotImplementedException(); }

        public void RemoveDepartment(string name)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Patient-related
        public void AddPatient(string firstName, string middleName, string lastName, DateTime birthDate, List<MedicalRecord>? medicalHistory, AppointmentSchedule? schedule)
        { throw new NotImplementedException(); }

        public void AddPatient(Patient patient)
        { throw new NotImplementedException(); }

        #endregion

        #region Methods - Staff-related
        public void AddStaff(string firstName, string middleName, string lastName, DateTime birthDate, List<StaffRole> roles, List<Department> departments)
        { throw new NotImplementedException(); }
        public void AddStaff(Staff staff)
        { throw new NotImplementedException(); }
        public void RemoveStaff(int ID)
        { throw new NotImplementedException(); }
        #endregion
    }
}
