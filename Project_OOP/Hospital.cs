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
        List<Department> Departments
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        List<int> Rooms
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        List<Patient> Patient
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        List<Staff> ActiveStaff
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        #endregion

        #region Methods
        public Hospital(int _ID, string name, string location, List<Department> departments, List<int> rooms)
        {
            throw new NotImplementedException();
        }

        public Hospital(int _ID, string name, string location, List<Department> departments, List<int> rooms, List<Patient> patient, List<Staff> activeStaff)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods - Department-related
        public void AddDepartment(string name, List<int> rooms)
        { throw new NotImplementedException(); }

        public void AddDepartment(string name, Staff head, List<int> rooms)
        { throw new NotImplementedException(); }

        public void RemoveDepartment(string name)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Patient-related
        public void AddPatient()
        { throw new NotImplementedException(); }

        public void AddPatient(Patient patient)
        { throw new NotImplementedException(); }

        #endregion

        #region Methods - Staff-related
        public void AddStaff()
        { throw new NotImplementedException(); }
        public void AddStaff(Staff staff)
        { throw new NotImplementedException(); }

        public void AddStaff(Department _dep)
        { throw new NotImplementedException(); }

        public void RemoveStaff(int _ID)
        { throw new NotImplementedException(); }
        #endregion
    }
}
