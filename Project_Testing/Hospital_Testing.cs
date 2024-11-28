using Project_OOP_WPF;
namespace Project_Testing
{
    [TestClass]
    public class Hospital_Testing
    {
        List<string> CorrectHospitalNames = new() { "Cityscape Medical Center", "Comfort Haven Clinic", "Beyondlimit Wellness" }; // used for CORRECT hospital names
        List<string> CorrectDepartmentNames = new() { "Finance", "Surgery", "ER", "Radiology", "Laboratory", "Psychiatry" }; // used for CORRECT department names
        List<string> CorrectPersonNames = new() { "Joe", "Jill", "Andrey", "John", "Vasiliy", "Robert", "Bob"}; // used for CORRECT first, middle, and last names
        
        List<string?> IncorrectHospitalNames = new() { null, " ", "", "1", "abcde12345", "      Cityscape Medical Center" };
        List<string?> IncorrectDepartmentNames = new() { null, " ", "", "1", "abcde12345", "      Finance"};
        List<string?> IncorrectPersonNames = new() { null, " ", "", "1", "abcde12345", "        Joe" };

        public Hospital DefaultHospital_Testing()
        {
            return new Hospital(
                "testName", // hospital name
                "testLocation", // hospital location
                new List<int> { 1, 2, 3 }); // 
        }

        public Department DefaultDepartment_Testing(Hospital parent)
        {
            return new Department(
                parent, // tie to a hospital
                CorrectDepartmentNames[0], // name of the department
                new List<int>() { 1, 2, 3 }); // rooms of the department
        }

        public Patient DefaultPatient_Testing(Hospital parent)
        {
            return new Patient(
                CorrectPersonNames[0], // first
                CorrectPersonNames[0], // middle
                CorrectPersonNames[0], // last names
                DateTime.Now, // DOB
                parent); // tie to a hospital
        }

        public Staff DefaultStaff_Testing(Hospital parent)
        {
            return new Staff(
                CorrectPersonNames[0], // first
                CorrectPersonNames[0], // middle
                CorrectPersonNames[0], // last names
                DateTime.Now, // DOB
                new List<StaffRole>() { StaffRole.Administrator }, // roles
                new List<Department>() { DefaultDepartment_Testing(parent) }, // department tie
                parent); // hospital tie
        }

        [TestMethod]
        public void Hospital_Constructor_Test()
        {
            // there's not gonna be any incorrect names, other than
            // idk, an empty string or something

            string location = "this can be anything in any format doesn't really matter";

            // negative numbers will fuck this up, change to unsigned later i guess?
            // or nah just have a check for negatives, best to do it that way to detect
            // when they are negative
            List<int> rooms = new() { 1, 2, 3, 4, 5 };
            #region additional stuff
            //List<Department> departmentList = new() 
            //{ 
            //    new Department(correctDepartmentNames[0], 0),
            //    new Department(correctDepartmentNames[1], 1),
            //    new Department(correctDepartmentNames[2], 2)
            //};
            //List<Patient> patientList = new()
            //{
            //    new Patient(correctPersonNames[0], correctPersonNames[0], correctPersonNames[0], DateTime.Now),
            //    new Patient(correctPersonNames[1], correctPersonNames[1], correctPersonNames[1], DateTime.Now),
            //    new Patient(correctPersonNames[2], correctPersonNames[2], correctPersonNames[2], DateTime.Now),
            //    new Patient(correctPersonNames[3], correctPersonNames[3], correctPersonNames[3], DateTime.Now),
            //    new Patient(correctPersonNames[4], correctPersonNames[4], correctPersonNames[4], DateTime.Now),
            //    new Patient(correctPersonNames[5], correctPersonNames[5], correctPersonNames[5], DateTime.Now),
            //    new Patient(correctPersonNames[6], correctPersonNames[6], correctPersonNames[6], DateTime.Now)
            //};
            //List<StaffRole> staffRoles = new() { StaffRole.Administrator };
            //List<Staff> activeStaffList = new()
            //{
            //    new Staff(correctPersonNames[0], correctPersonNames[0], correctPersonNames[0], DateTime.Now, staffRoles, departmentList),
            //    new Staff(correctPersonNames[1], correctPersonNames[1], correctPersonNames[1], DateTime.Now, staffRoles, departmentList),
            //    new Staff(correctPersonNames[2], correctPersonNames[2], correctPersonNames[2], DateTime.Now, staffRoles, departmentList),
            //    new Staff(correctPersonNames[3], correctPersonNames[3], correctPersonNames[3], DateTime.Now, staffRoles, departmentList),
            //    new Staff(correctPersonNames[4], correctPersonNames[4], correctPersonNames[4], DateTime.Now, staffRoles, departmentList),
            //    new Staff(correctPersonNames[5], correctPersonNames[5], correctPersonNames[5], DateTime.Now, staffRoles, departmentList),
            //    new Staff(correctPersonNames[6], correctPersonNames[6], correctPersonNames[6], DateTime.Now, staffRoles, departmentList)
            //};
            #endregion

            Assert.ThrowsException<NullReferenceException>(() => new Hospital(IncorrectHospitalNames[0]!, location, rooms));
            Assert.ThrowsException<NullReferenceException>(() => new Hospital(IncorrectHospitalNames[1]!, location, rooms));
            Assert.ThrowsException<NullReferenceException>(() => new Hospital(IncorrectHospitalNames[2]!, location, rooms));
            Assert.ThrowsException<ArgumentException>(() => new Hospital(IncorrectHospitalNames[3]!, location, rooms));
            Assert.ThrowsException<ArgumentException>(() => new Hospital(IncorrectHospitalNames[4]!, location, rooms));
            Assert.ThrowsException<ArgumentException>(() => new Hospital(IncorrectHospitalNames[5]!, location, rooms));

            Assert.ThrowsException<ArgumentException>(() => new Hospital(CorrectHospitalNames[0], location, new List<int>() { -1, 0, 1, 2 }));
        }

        [TestMethod]
        public void Hospital_NameChange_Test()
        {
            Hospital testHospital = DefaultHospital_Testing();
            
            testHospital.ChangeName(CorrectHospitalNames[0]);
            Assert.AreEqual(CorrectHospitalNames[0], testHospital.Name);
            testHospital.ChangeName(CorrectHospitalNames[1]);
            Assert.AreEqual(CorrectHospitalNames[1], testHospital.Name);
            testHospital.ChangeName(CorrectHospitalNames[2]);
            Assert.AreEqual(CorrectHospitalNames[2], testHospital.Name);

            Assert.ThrowsException<NullReferenceException>(() => testHospital.ChangeName(IncorrectHospitalNames[0]!));
            Assert.ThrowsException<NullReferenceException>(() => testHospital.ChangeName(IncorrectHospitalNames[1]!));
            Assert.ThrowsException<NullReferenceException>(() => testHospital.ChangeName(IncorrectHospitalNames[2]!));
            Assert.ThrowsException<ArgumentException>(() => testHospital.ChangeName(IncorrectHospitalNames[3]!));
            Assert.ThrowsException<ArgumentException>(() => testHospital.ChangeName(IncorrectHospitalNames[4]!));
            Assert.ThrowsException<ArgumentException>(() => testHospital.ChangeName(IncorrectHospitalNames[5]!));
        }

        [TestMethod]
        public void Hospital_AddRoom_Test()
        {
            Hospital testHospital = DefaultHospital_Testing();

            testHospital.AddRoom(4);
            Assert.AreEqual(new List<int> { 1, 2, 3, 4 }, testHospital.Rooms);
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRoom(0));
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRoom(-1));
        }

        [TestMethod]
        public void Hospital_AddRooms_Test()
        {
            Hospital testHospital = DefaultHospital_Testing();

            testHospital.AddRooms(new List<int>() { 4, 5 });
            Assert.AreEqual(new List<int>() { 1, 2, 3, 4, 5 }, testHospital.Rooms );
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new List<int>() { -1, 0 })); // 0 and negative room id's are not allowed
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new List<int>() { 1 })); // overlap is not allowed
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new List<int>() { 1, 2 }));
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new List<int>() { 1, 2, 3 }));
        }

        [TestMethod]
        public void Hospital_AddDepartment_Test()
        {
            Hospital testHospital = DefaultHospital_Testing();

            // incorrect name formats
            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddDepartment(IncorrectDepartmentNames[0], new List<int>() { 1, 2, 3 }));
            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddDepartment(IncorrectDepartmentNames[1], new List<int>() { 1, 2, 3 }));
            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddDepartment(IncorrectDepartmentNames[2], new List<int>() { 1, 2, 3 }));
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddDepartment(IncorrectDepartmentNames[3], new List<int>() { 1, 2, 3 }));
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddDepartment(IncorrectDepartmentNames[4], new List<int>() { 1, 2, 3 }));
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddDepartment(IncorrectDepartmentNames[5], new List<int>() { 1, 2, 3 }));
            // cannot add rooms that do not exist within the hospital
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddDepartment(CorrectDepartmentNames[0], new List<int>() { 4, 5 }));

            testHospital.AddDepartment(CorrectDepartmentNames[0], new List<int>() { 1, 2, 3 });
            Assert.AreEqual(1, testHospital.Departments.Count());

        }

        [TestMethod]
        public void Hospital_RemoveDepartment_Test()
        {
            Hospital testHospital = DefaultHospital_Testing();
            testHospital.AddDepartment(CorrectDepartmentNames[0], new List<int>() { 1, 2, 3 });
            testHospital.AddDepartment(CorrectDepartmentNames[1], new List<int>() { 1, 2, 3 });
            testHospital.AddDepartment(CorrectDepartmentNames[2], new List<int>() { 1, 2, 3 });

            Assert.AreEqual(3, testHospital.Departments.Count());

            Assert.ThrowsException<NullReferenceException>(() => testHospital.RemoveDepartment(IncorrectDepartmentNames[0]));
            Assert.ThrowsException<NullReferenceException>(() => testHospital.RemoveDepartment(IncorrectDepartmentNames[1]));
            Assert.ThrowsException<NullReferenceException>(() => testHospital.RemoveDepartment(IncorrectDepartmentNames[2]));
            Assert.ThrowsException<ArgumentException>(() => testHospital.RemoveDepartment(IncorrectDepartmentNames[3]));

            testHospital.RemoveDepartment(CorrectDepartmentNames[0]);

            Assert.AreEqual(2, testHospital.Departments.Count());
            Assert.ThrowsException<ArgumentException>(() => testHospital.RemoveDepartment(CorrectDepartmentNames[0]));
        }

        [TestMethod]
        public void Hospital_AddNewPatient_Test()
        {
            Hospital testHospital = DefaultHospital_Testing();
            testHospital.AddPatient(CorrectPersonNames[0], CorrectPersonNames[0], CorrectPersonNames[0], DateTime.Now);

            Assert.AreEqual(1, testHospital.Patients.Count());

            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddPatient(IncorrectPersonNames[0], IncorrectPersonNames[0], IncorrectPersonNames[0], DateTime.Now));
            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddPatient(IncorrectPersonNames[1], IncorrectPersonNames[1], IncorrectPersonNames[1], DateTime.Now));
            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddPatient(IncorrectPersonNames[2], IncorrectPersonNames[2], IncorrectPersonNames[2], DateTime.Now));
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddPatient(IncorrectPersonNames[3], IncorrectPersonNames[3], IncorrectPersonNames[3], DateTime.Now));
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddPatient(IncorrectPersonNames[4], IncorrectPersonNames[4], IncorrectPersonNames[4], DateTime.Now));
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddPatient(IncorrectPersonNames[5], IncorrectPersonNames[5], IncorrectPersonNames[5], DateTime.Now));

            testHospital.AddPatient(CorrectPersonNames[0], CorrectPersonNames[0], CorrectPersonNames[0], DateTime.Now);
            Assert.AreNotEqual(testHospital.Patients[0].ID, testHospital.Patients[1].ID);
        }

        [TestMethod]
        public void Hospital_AddExistingPatient_Test()
        {
            Hospital testHospital = DefaultHospital_Testing();
            testHospital.AddPatient(CorrectPersonNames[0], CorrectPersonNames[0], CorrectPersonNames[0], DateTime.Now);

            Assert.AreEqual(1, testHospital.Patients.Count());

            Patient patient = testHospital.Patients[0];

            // can't add the same patient twice
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddPatient(patient));

            // the id should stay the same across all hospitals
            int patientID = testHospital.Patients[0].ID;
            Hospital testHospital2 = DefaultHospital_Testing();
            testHospital2.AddPatient(patient);

            Assert.AreEqual(patientID, testHospital2.Patients[0].ID);
        }

        [TestMethod]
        public void Hospital_TransferPatient_Test()
        {
            Hospital testHospital = DefaultHospital_Testing();
            Hospital testHospital2 = DefaultHospital_Testing();
            testHospital.AddPatient(CorrectPersonNames[0], CorrectPersonNames[0], CorrectPersonNames[0], DateTime.Now);

            Assert.ThrowsException<ArgumentException>(() => testHospital.TransferPatient(testHospital.Patients[0], testHospital));

            int patientID = testHospital.Patients[0].ID;
            testHospital.TransferPatient(testHospital.Patients[0], testHospital2);

            Assert.AreEqual(0, testHospital.Patients.Count());
            Assert.AreEqual(patientID, testHospital2.Patients[0].ID);
        }

        [TestMethod]
        public void Hospital_AddNewStaff_Test()
        {
            Hospital testHospital = DefaultHospital_Testing();
            testHospital.AddStaff(CorrectPersonNames[0], CorrectPersonNames[0], CorrectPersonNames[0], DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>());

        }
    }
}