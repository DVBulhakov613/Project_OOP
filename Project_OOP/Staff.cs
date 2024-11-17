﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP
{
    public class Staff : IPerson
    {
        #region Properties - Person info
        public int ID
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public static IDManagement idManager = new IDManagement();
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
        #endregion

        #region Properties - Staff-specific properties
        public List<StaffRole> Roles
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<Department> Departments
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public AppointmentSchedule Schedule
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        #endregion

        #region Methods - Staff-specific
        public Staff(string firstName, string middleName, string lastName, DateTime birthDate, List<StaffRole> roles, List<Department> departments)
        { throw new NotImplementedException(); }
        public void ChangeRoles(List<StaffRole> roles)
        { throw new NotImplementedException(); }
        #endregion

        #region Methods - Inherited from the Person interface
        public string GetFullName()
        { throw new NotImplementedException(); }
        public void ChangeInfo()
        { throw new NotImplementedException(); }
        public string ToString()
        { throw new NotImplementedException(); }
        #endregion
    }
}