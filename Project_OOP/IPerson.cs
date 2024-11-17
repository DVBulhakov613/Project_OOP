using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP
{
    public interface IPerson
    {
        public int ID { get; set; }
        static IDManagement idManager = new IDManagement();
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public string GetFullName();
        public void ChangeInfo();
        public string ToString();
    }
}
