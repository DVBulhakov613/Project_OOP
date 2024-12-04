using Project_OOP_WPF;
namespace Project_Testing
{
    public static class TestUtilities
    {
        public static IEnumerable<string>[] CorrectHospitalNames =
        {
            ["Cityscape Medical Center"],
            ["Comfort Haven Clinic"],
            ["Beyondlimit Wellness"]
        };

        public static IEnumerable<string>[] IncorrectHospitalNames =
        {
            [null], 
            [" "], 
            [""], 
            ["1"], 
            ["abcde12345"], 
            ["      Cityscape Medical Center"]
        };

        public static IEnumerable<string>[] CorrectDepartmentNames =
        {
            ["Finance"], 
            ["Surgery"], 
            ["ER"], 
            ["Radiology"], 
            ["Laboratory"], 
            ["Psychiatry"]
        };

        public static IEnumerable<string>[] IncorrectDepartmentNames =
        {
            [null], 
            [" "], 
            [""], 
            ["1"], 
            ["abcde12345"], 
            ["      Finance"]
        };

        public static IEnumerable<string>[] CorrectPersonNames =
        {
            ["Joe"], 
            ["Jill"], 
            ["Andrey"], 
            ["John"], 
            ["Vasiliy"], 
            ["Robert"], 
            ["Bob"]
        };

        public static IEnumerable<string>[] IncorrectPersonNames =
        {
            [null], 
            [" "], 
            [""], 
            ["1"], 
            ["abcde12345"], 
            ["        Joe"]
        };

        public static void TestCleanup()
        {
            Staff.IDManager = new IDManagement();

        }


        public static Hospital DefaultHospital_Testing()
        {
            return new Hospital(
                "TestName", // hospital name
                "TestLocation", // hospital location
                new List<int> { 1, 2, 3 } // rooms
            );
        }

        public static Department DefaultDepartment_Testing(Hospital parent)
        {
            return new Department(
                parent, // tie to a hospital
                "DefaultDepartmentName", // name of the department
                new List<int>() { 1, 2, 3 } // rooms of the department
            );
        }

        public static Patient DefaultPatient_Testing(Hospital parent)
        {
            return new Patient(
                "DefaultName", // first
                "DefaultName", // middle
                "DefaultName", // last names
                DateTime.Now, // DOB
                parent // tie to a hospital
            );
        }

        public static Staff DefaultStaff_Testing(Hospital parent)
        {
            return new Staff(
                "DefaultName", // first
                "DefaultName", // middle
                "DefaultName", // last names
                DateTime.Now, // DOB
                new List<StaffRole>() { StaffRole.Administrator }, // roles
                parent, // hospital tie
                new List<Department>() { DefaultDepartment_Testing(parent) } // department tie
            );
        }
    }

    [TestClass]
    public class Hospital_Testing
    {
        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectHospitalNames), typeof(TestUtilities))]
        public void Hospital_Constructor_IncorrectNames_Test(string IncorrectHospitalName)
        {
            string location = "this can be anything in any format doesn't really matter";

            List<int> rooms = new() { 1, 2, 3, 4, 5 };

            if (string.IsNullOrWhiteSpace(IncorrectHospitalName))
                Assert.ThrowsException<NullReferenceException>(() => new Hospital(IncorrectHospitalName, location, rooms), "Not checking for empty strings");
            else
                Assert.ThrowsException<ArgumentException>(() => new Hospital(IncorrectHospitalName, location, rooms), "Not checking for name format");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectHospitalNames), typeof(TestUtilities))]
        public void Hospital_Constructor_CorrectNames_Test(string CorrectHospitalName)
        {
            string location = "this can be anything in any format doesn't really matter";

            List<int> rooms = new() { 1, 2, 3, 4, 5 };

            new Hospital(CorrectHospitalName, location, rooms); // will throw an exception if it doesnt go through
        }

        [TestMethod]
        public void Hospital_Constructor_Rooms_Test()
        {
            string location = "this can be anything in any format doesn't really matter";

            Assert.ThrowsException<ArgumentException>(() => new Hospital("TestHospital", location, new List<int>() { -1 }), "Not checking for correct room format");
            Assert.ThrowsException<ArgumentException>(() => new Hospital("TestHospital", location, new List<int>() { 0 }), "Not checking for correct room format");
            Assert.ThrowsException<ArgumentException>(() => new Hospital("TestHospital", location, new List<int>()), "Not checking for correct room format");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectHospitalNames), typeof(TestUtilities))]
        public void Hospital_CorrectNameChange_Test(string CorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            testHospital.ChangeName(CorrectName);
            Assert.AreEqual(CorrectName, testHospital.Name, "Name assignment is incorrect");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectHospitalNames), typeof(TestUtilities))]
        public void Hospital_IncorrectNameChange_Test(string IncorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            if (string.IsNullOrWhiteSpace(IncorrectName))
                Assert.ThrowsException<NullReferenceException>(() => testHospital.ChangeName(IncorrectName), "Not checking for empty strings");
            else
                Assert.ThrowsException<ArgumentException>(() => testHospital.ChangeName(IncorrectName), "Not checking for name format");
        }

        [TestMethod]
        public void Hospital_AddRoom_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            testHospital.AddRoom(4);
            Assert.AreEqual(new List<int> { 1, 2, 3, 4 }, testHospital.Rooms, "Not adding rooms up correctly?");
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRoom(0), "Not checking for incorrect value range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRoom(-1), "Not checking for incorrect value range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRoom(1), "Not checking for overlap");
        }

        [TestMethod]
        public void Hospital_AddRooms_Test(List<int> rooms)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            testHospital.AddRooms(new List<int>() { 4, 5 });
            Assert.AreEqual(new List<int>() { 1, 2, 3, 4, 5 }, testHospital.Rooms );
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new List<int>() { -1, 0 }), "Not checking for incorrect value range"); // 0 and negative room id's are not allowed
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new List<int>() { 1 }), "Not checking for overlap"); // overlap is not allowed
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new List<int>() { 1, 2 }), "Not checking for overlap");
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddRooms(new List<int>() { 1, 2, 3 }), "Not checking for overlap");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectDepartmentNames), typeof(TestUtilities))]
        public void Hospital_AddIncorrectDepartmentName_Test(string IncorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            // incorrect name formats
            if (string.IsNullOrWhiteSpace(IncorrectName))
                Assert.ThrowsException<NullReferenceException>(() => testHospital.AddDepartment(IncorrectName, new List<int>() { 1, 2, 3 }), "Not checking for empty strings");
            else
                Assert.ThrowsException<ArgumentException>(() => testHospital.AddDepartment(IncorrectName, new List<int>() { 1, 2, 3 }), "Not checking for incorrect name format");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectDepartmentNames), typeof(TestUtilities))]
        public void Hospital_AddCorrectDepartmentName_Test(string CorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            // cannot add rooms that do not exist within the hospital
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddDepartment(CorrectName, new List<int>() { 4, 5 }), "Allowing non-existent rooms");

            testHospital.AddDepartment(CorrectName, new List<int>() { 1, 2, 3 });
            Assert.AreEqual(1, testHospital.Departments.Count);

            // must avoid duplicate names
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddDepartment(CorrectName, new List<int>() { 1, 2, 3 }), "Allowing duplicate names");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectDepartmentNames), typeof(TestUtilities))]
        public void Hospital_RemoveDepartmentNullInput_Test(string IncorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            if(string.IsNullOrEmpty(IncorrectName))
                Assert.ThrowsException<NullReferenceException>(() => testHospital.RemoveDepartment(IncorrectName), "Not checking for empty strings");

        }

        [TestMethod]
        public void Hospital_RemoveDepartmentIncorrectInput_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
            testHospital.AddDepartment("CorrectName1", new List<int>() { 1, 2, 3 });
            testHospital.AddDepartment("CorrectName2", new List<int>() { 1, 2, 3 });

            Assert.AreEqual(3, testHospital.Departments.Count, "Not adding correctly?");

            testHospital.RemoveDepartment("CorrectName1");

            Assert.AreEqual(2, testHospital.Departments.Count, "Not actually deleting the object?");
            Assert.ThrowsException<ArgumentException>(() => testHospital.RemoveDepartment("CorrectName1"), "Can delete what is not there anymore");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddNewPatientIncorrectName_Test(string IncorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            if(string.IsNullOrWhiteSpace(IncorrectName))
                Assert.ThrowsException<NullReferenceException>(() => testHospital.AddPatient(IncorrectName, IncorrectName, IncorrectName, DateTime.Now), "Not checking for empty strings");
            else
                Assert.ThrowsException<ArgumentException>(() => testHospital.AddPatient(IncorrectName, IncorrectName, IncorrectName, DateTime.Now), "Wrong algorithm for checking over name formats");

            testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);
            Assert.AreNotEqual(testHospital.Patients[0].ID, testHospital.Patients[1].ID, "Patient ID should always stay unique");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddNewPatientIncorrectDate_Test(string CorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now);
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now.AddYears(-100)), "Not checking for year range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now.AddYears(100)), "Not checking for year range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now.AddDays(1)), "Not checking for day range");

            Assert.AreEqual(1, testHospital.Patients.Count, "Either adding is broken, or you are adding objects despite exceptions");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddNewPatientCorrect_Test(string CorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now);

            Assert.AreEqual(1, testHospital.Patients.Count, "Something wrong with how you add things");

            testHospital.AddPatient(CorrectName, CorrectName, CorrectName, DateTime.Now);
            Assert.AreNotEqual(testHospital.Patients[0].ID, testHospital.Patients[1].ID);
        }

        [TestMethod]
        public void Hospital_AddExistingPatient_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);

            Assert.AreEqual(1, testHospital.Patients.Count, "Something wrong with how you add things");

            Patient patient = testHospital.Patients[0];

            // can't add the same patient twice
            Assert.ThrowsException<ArgumentException>(() => testHospital.AddPatient(patient), "You are allowing duplicate patients");

            // the id should stay the same across all hospitals
            int patientID = testHospital.Patients[0].ID;
            Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();
            testHospital2.AddPatient(patient);

            Assert.AreEqual(patientID, testHospital2.Patients[0].ID, "Patient ID must stay the same");
        }

        [TestMethod]
        public void Hospital_TransferPatient_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();
            testHospital.AddPatient("CorrectName", "CorrectName", "CorrectName", DateTime.Now);

            // cannot transfer a patient to the same hospital
            Assert.ThrowsException<ArgumentException>(() => testHospital.TransferPatient(testHospital.Patients[0], testHospital), "Can transfer patients to the same hospital");

            // the id should stay the same
            int patientID = testHospital.Patients[0].ID;
            testHospital.TransferPatient(testHospital.Patients[0], testHospital2);

            Assert.AreEqual(0, testHospital.Patients.Count, "Not removing patients after transfer");
            Assert.AreEqual(patientID, testHospital2.Patients[0].ID, "Patient ID must stay the same");
        }

        //[DataTestMethod]
        //[DynamicData(nameof())]
        //public void Hospital_AddNewStaff_Test()
        //{
        //    Hospital testHospital = DefaultHospital_Testing();
        //    testHospital.AddStaff(CorrectPersonNames[0], CorrectPersonNames[0], CorrectPersonNames[0], DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>());

        //    Assert.AreEqual(1, testHospital.ActiveStaff.Count());

        //    Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff(IncorrectPersonNames[0], IncorrectPersonNames[0], IncorrectPersonNames[0], DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>()));
        //    Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff(IncorrectPersonNames[1], IncorrectPersonNames[1], IncorrectPersonNames[1], DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>()));
        //    Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff(IncorrectPersonNames[2], IncorrectPersonNames[2], IncorrectPersonNames[2], DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>()));
        //    Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff(CorrectPersonNames[0], CorrectPersonNames[0], CorrectPersonNames[0], DateTime.Now, new List<StaffRole>() {  }, new List<Department>()));
        //    Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff(CorrectPersonNames[1], CorrectPersonNames[1], CorrectPersonNames[1], DateTime.Now, new List<StaffRole>() {  }, new List<Department>()));
        //    Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff(CorrectPersonNames[2], CorrectPersonNames[2], CorrectPersonNames[2], DateTime.Now, new List<StaffRole>() {  }, new List<Department>()));
        //    Assert.ThrowsException<ArgumentException>(() => testHospital.AddStaff(IncorrectPersonNames[3], IncorrectPersonNames[3], IncorrectPersonNames[3], DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>()));
        //    Assert.ThrowsException<ArgumentException>(() => testHospital.AddStaff(IncorrectPersonNames[4], IncorrectPersonNames[4], IncorrectPersonNames[4], DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>()));
        //    Assert.ThrowsException<ArgumentException>(() => testHospital.AddStaff(IncorrectPersonNames[5], IncorrectPersonNames[5], IncorrectPersonNames[5], DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>()));

        //    testHospital.AddStaff(CorrectPersonNames[0], CorrectPersonNames[0], CorrectPersonNames[0], DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>());
        //    Assert.AreNotEqual(testHospital.ActiveStaff[0].ID, testHospital.ActiveStaff[1].ID);
        //}

        //[TestMethod]
        //public void Hospital_AddExistingStaff_Test()
        //{
        //    Hospital testHospital = DefaultHospital_Testing();
        //    testHospital.AddStaff(DefaultStaff_Testing(testHospital));

        //    Assert.AreEqual(1, testHospital.ActiveStaff.Count());

        //    testHospital.AddStaff(DefaultStaff_Testing(testHospital));
        //    Assert.AreNotEqual(testHospital.ActiveStaff[0].ID, testHospital.ActiveStaff[1].ID);
        //}

        //[TestMethod]
        //public void Hospital_RemoveStaff_Test()
        //{
        //    Hospital testHospital = DefaultHospital_Testing();
        //    testHospital.AddStaff(DefaultStaff_Testing(testHospital));

        //    Assert.ThrowsException<ArgumentException>(() => testHospital.RemoveStaff(-1));
        //    Assert.ThrowsException<ArgumentException>(() => testHospital.RemoveStaff(10));

        //    testHospital.AddStaff(DefaultStaff_Testing(testHospital));
        //    testHospital.RemoveStaff(0);

        //    Assert.AreEqual(1, testHospital.ActiveStaff[0].ID);
        //}

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddNewStaffInvalidNames_Test(string incorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff(incorrectName, incorrectName, incorrectName, DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, new List<Department>()));
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddNewStaff_EmptyRoles_Test(string CorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff(CorrectName, CorrectName, CorrectName, DateTime.Now, new List<StaffRole>(), new List<Department>()));
        }

        [TestMethod]
        public void Hospital_AddNewStaffValidStaff_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, new List<Department>());

            Assert.AreEqual(1, testHospital.ActiveStaff.Count, "Staff count mismatch");

            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, new List<Department>());
            Assert.AreNotEqual(testHospital.ActiveStaff[0].ID, testHospital.ActiveStaff[1].ID, "Staff IDs should always be unique");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddExistingStaff_Test(string correctName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff(correctName, correctName, correctName, DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, new List<Department>());

            Assert.AreEqual(1, testHospital.ActiveStaff.Count, "Something wrong with how you add things");

            testHospital.AddStaff(correctName, correctName, correctName, DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, new List<Department>());

            Assert.AreNotEqual(testHospital.ActiveStaff[0].ID, testHospital.ActiveStaff[1].ID, "Staff ID must always stay the same and unique");
        }

        [TestMethod]
        public void Hospital_RemoveStaff_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[0] });

            Assert.ThrowsException<ArgumentException>(() => testHospital.RemoveStaff(-1), "Negative staff ID should throw an exception");
            Assert.ThrowsException<ArgumentException>(() => testHospital.RemoveStaff(10), "Non-existent staff ID should throw an exception");

            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[0] });
            testHospital.RemoveStaff(0);

            Assert.AreEqual(1, testHospital.ActiveStaff[0].ID, "Incorrect staff removal logic");
        }
    }

    [TestClass]
    public class Department_Testing
    {
        Hospital testHospital = TestUtilities.DefaultHospital_Testing();

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectDepartmentNames), typeof(TestUtilities))]
        public void Department_ConstructorIncorrectNames_Test(string IncorrectNames)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Assert.ThrowsException<ArgumentException>(() => new Department(testHospital, IncorrectNames, new List<int>() { 1, 2, 3 }), "Not checking format");
        }

        [TestMethod]
        public void Department_ConstructorIncorrectRooms_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Assert.ThrowsException<ArgumentException>(() => new Department(testHospital, "CorrectName", new List<int>() { 4, 5 }), "Not checking available rooms");
            Assert.ThrowsException<ArgumentException>(() => new Department(testHospital, "CorrectName", new List<int>() { -1 }), "Not checking room value range");
            Assert.ThrowsException<ArgumentException>(() => new Department(testHospital, "CorrectName", new List<int>() { 0 }), "Not checking room value range");
            Assert.ThrowsException<NullReferenceException>(() => new Department(testHospital, "CorrectName", new List<int>() { }), "Not checking for empty strings");
        }

        [TestMethod]
        public void Department_ChangeHead_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[0] });
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[0] });
            testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 }, testHospital.ActiveStaff[0]);

            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].ChangeHead(testHospital.ActiveStaff[0].ID), "Cannot assing staff to a position they are already occupying");
            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].ChangeHead(10), "Not checking for value range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].ChangeHead(-1), "Not checking for value range");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectDepartmentNames), typeof(TestUtilities))]
        public void Department_ChangeName_Test(string IncorrectNames)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
            testHospital.AddDepartment("CorrectName2", new List<int>() { 1, 2, 3 });

            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].ChangeName("CorrectName2"), "Cannot allow duplicate department names in the same hospital");

            if (string.IsNullOrWhiteSpace(IncorrectNames))
                Assert.ThrowsException<NullReferenceException>(() => testHospital.Departments[0].ChangeName(IncorrectNames), "Not checking for empty strings");
            else
                Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].ChangeName(IncorrectNames), "Not checking for format");

        }

        [TestMethod]
        public void Department_AddStaff_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[0] });

            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].AddStaff(-1), "Not checking for ID range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].AddStaff(10), "Not checking for ID range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].AddStaff(testHospital.ActiveStaff[0].ID), "Not checking for ID range");
        }

        [TestMethod]
        public void Department_RemoveStaff_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
            testHospital.AddDepartment("CorrectName2", new List<int>() { 1, 2, 3 });
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[0] });
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[0] });
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[0] });
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[1] });


            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].RemoveStaff(-1), "Not checking for ID range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].RemoveStaff(10), "Not checking for ID range");
            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].RemoveStaff(testHospital.ActiveStaff[0].ID), "Not checking for ID range");

            Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();
            testHospital2.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
            testHospital2.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital2.Departments[0] });

            Assert.ThrowsException<ArgumentException>(() => testHospital.Departments[0].RemoveStaff(testHospital2.ActiveStaff[0].ID), "Not checking for whether the staff belongs to this hospital");
        }
    }
}