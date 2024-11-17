using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP
{
    public class Department
    {
        #region Properties
        
        public string Name
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public Staff HeadOfDepartment
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<Staff> DepartmentStaff
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        public List<int> DepartmentRooms
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }
        
        #endregion

        #region Methods

        public Department(string name, Staff head)
        { throw new NotImplementedException(); }

        public Department(string name, Staff head, List<int> rooms)
        { throw new NotImplementedException(); }

        public Department(string name, Staff head, List<Staff> staff, List<int> rooms)
        { throw new NotImplementedException(); }

        public void ChangeHead(Staff newHead)
        { throw new NotImplementedException(); }

        public void ChangeName(string newName) { throw new NotImplementedException(); }
        public void AddStaff(Staff staff) { throw new NotImplementedException(); }
        public void RemoveStaff(int _ID) { throw new NotImplementedException(); }
        public void TransferStaff(int _ID, Department _dep) { throw new NotImplementedException(); }
        public string StaffInfo() { throw new NotImplementedException(); }
        
        #endregion
    }
}
