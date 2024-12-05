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

        public static IEnumerable<string>[] IncorrectMedicalRecordInfo =
        {
            [null],
            [" "],
            [""],
            ["1"],
            ["abcde12345"],
            ["      boo"]
        };

        public static void TestCleanup()
        {
            Staff.IDManager = new IDManagement();
            Patient.IDManager = new IDManagement();
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
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

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
            Assert.AreEqual(new List<int>() { 1, 2, 3, 4, 5 }, testHospital.Rooms);
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

            if (string.IsNullOrEmpty(IncorrectName))
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

            if (string.IsNullOrWhiteSpace(IncorrectName))
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
        public void Hospital_AddNewStaff_InvalidNames(string incorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff(incorrectName, incorrectName, incorrectName, DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, new List<Department>()));
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Hospital_AddNewStaff_EmptyRoles(string CorrectName)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();

            Assert.ThrowsException<NullReferenceException>(() => testHospital.AddStaff(CorrectName, CorrectName, CorrectName, DateTime.Now, new List<StaffRole>(), new List<Department>()));
        }

        [TestMethod]
        public void Hospital_AddNewStaff_ValidStaff()
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
            testHospital.AddStaff(correctName, correctName, correctName, DateTime.Now, new List<StaffRole> { StaffRole.Administrator });

            Assert.AreEqual(1, testHospital.ActiveStaff.Count, "Something wrong with how you add things");

            testHospital.AddStaff(correctName, correctName, correctName, DateTime.Now, new List<StaffRole> { StaffRole.Administrator });

            Assert.AreNotEqual(testHospital.ActiveStaff[0].ID, testHospital.ActiveStaff[1].ID, "Staff ID must always stay the same and unique");
        }

        [TestMethod]
        public void Hospital_TransferStaff_Test()
        {

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
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectDepartmentNames), typeof(TestUtilities))]
        public void Department_Constructor_IncorrectNames(string IncorrectNames)
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
        public void Department_ChangeHead_InvalidCases()
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

        [TestMethod]
        public void Department_TransferStaff_DifferentHospital()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
            testHospital.AddDepartment("CorrectName2", new List<int>() { 1, 2, 3 });
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[0] });
            testHospital.AddStaff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole>() { StaffRole.Administrator }, new List<Department>() { testHospital.Departments[1] });

            Assert.AreEqual(2, testHospital.Departments.Count, "Incorrectly adding departments");
            Assert.AreEqual(2, testHospital.ActiveStaff.Count, "Incorrectly adding staff");

            try { testHospital.Departments[0].TransferStaff(0, testHospital.Departments[1]); }
            catch { Assert.Fail("Not allowing transfer of staff between departments"); }

            Hospital testHospital2 = TestUtilities.DefaultHospital_Testing();
            testHospital.AddDepartment("CorrectName", new List<int>() { 1, 2, 3 });
        }
    }

    [TestClass]
    public class IDManagement_Testing
    {

    }

    [TestClass]
    public class Staff_Testing
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Staff_Constructor_ValidParameters(string CorrectNames)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Staff testStaff = new Staff(CorrectNames, CorrectNames, CorrectNames, DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, testHospital);
            Assert.AreEqual(CorrectNames, testStaff.FirstName, "Somehow assigning a wrong first name");
            Assert.AreEqual(CorrectNames, testStaff.MiddleName, "Somehow assigning a wrong middle name");
            Assert.AreEqual(CorrectNames, testStaff.LastName, "Somehow assigning a wrong last name");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Staff_Constructor_InvalidParameters(string IncorrectNames)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            if (string.IsNullOrWhiteSpace(IncorrectNames))
            {
                Assert.ThrowsException<NullReferenceException>(() => new Staff(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, testHospital), "Not checking for empty strings in first name");
                Assert.ThrowsException<NullReferenceException>(() => new Staff("CorrectName", IncorrectNames, "CorrectName", DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, testHospital), "Not checking for empty strings in middle name");
                Assert.ThrowsException<NullReferenceException>(() => new Staff("CorrectName", "CorrectName", IncorrectNames, DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, testHospital), "Not checking for empty strings in last name");
            }
            else
            {
                Assert.ThrowsException<ArgumentException>(() => new Staff(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, testHospital), "Allowing wrong format on first name");
                Assert.ThrowsException<ArgumentException>(() => new Staff("CorrectName", IncorrectNames, "CorrectName", DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, testHospital), "Allowing wrong format on middle name");
                Assert.ThrowsException<ArgumentException>(() => new Staff("CorrectName", "CorrectName", IncorrectNames, DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, testHospital), "Allowing wrong format on last name");
            }

            Assert.ThrowsException<ArgumentException>(() => new Staff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(1), new List<StaffRole> { StaffRole.Administrator }, testHospital), "Not checking for DOB range");
            Assert.ThrowsException<ArgumentException>(() => new Staff("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-120), new List<StaffRole> { StaffRole.Administrator }, testHospital), "Not checking for DOB range");
            Assert.ThrowsException<ArgumentException>(() => new Staff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole> { }, testHospital), "Not checking for empty role assignments");

            Assert.AreEqual(0, Staff.IDManager.GenerateID(), "Adding ID's despite incorrect object creation");
        }

        [TestMethod]
        public void Staff_ChangeRoles_ValidParameters()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Staff testStaff = TestUtilities.DefaultStaff_Testing(testHospital);
            List<StaffRole> currentStaffRoles = testStaff.Roles;

            testStaff.ChangeRoles(new List<StaffRole>() { StaffRole.Therapist });

            Assert.AreNotEqual(currentStaffRoles, testStaff.Roles, "Not assigning the staff roles correctly");
        }

        [TestMethod]
        public void Staff_GetFullName_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Staff testStaff = TestUtilities.DefaultStaff_Testing(testHospital);

            Assert.AreEqual($"DefaultName DefaultName DefaultName", testStaff.GetFullName(), "Probably incorrect output format");
        }

        [TestMethod]
        public void Staff_ChangeInfo_ValidParameters()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Staff testStaff = TestUtilities.DefaultStaff_Testing(testHospital);

            DateTime staffDOB = testStaff.BirthDate;
            DateTime time = DateTime.Now.AddYears(-5);
            testStaff.ChangeInfo("DefaultName2", "DefaultName2", "DefaultName2", time);

            Assert.AreEqual("DefaultName2", testStaff.FirstName, "Not assigning a new first name");
            Assert.AreEqual("DefaultName2", testStaff.MiddleName, "Not assigning a new middle name");
            Assert.AreEqual("DefaultName2", testStaff.LastName, "Not assigning a new last name");
            Assert.AreNotEqual(staffDOB, testStaff.BirthDate, "Not assigning a new date of birth");

        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Staff_ChangeInfo_InvalidParameters(string names)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Staff testStaff = TestUtilities.DefaultStaff_Testing(testHospital);

            Assert.ThrowsException<ArgumentException>(() => testStaff.ChangeInfo(names, "CorrectName", "CorrectName", DateTime.Now), "Not checking format for first name");
            Assert.ThrowsException<ArgumentException>(() => testStaff.ChangeInfo("CorrectName", names, "CorrectName", DateTime.Now), "Not checking format for middle name");
            Assert.ThrowsException<ArgumentException>(() => testStaff.ChangeInfo("CorrectName", "CorrectName", names, DateTime.Now), "Not checking format for last name");
            Assert.ThrowsException<ArgumentException>(() => testStaff.ChangeInfo("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-120)));
            Assert.ThrowsException<ArgumentException>(() => testStaff.ChangeInfo("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(1)));
        }
        // there should also be a ToString() test but im not sure of what the format should be just yet
    }

    [TestClass]
    public class Patient_Testing
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.CorrectPersonNames), typeof(TestUtilities))]
        public void Patient_Constructor_ValidParameters(string CorrectNames)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Patient testPatient = new Patient(CorrectNames, CorrectNames, CorrectNames, DateTime.Now, testHospital);
            Assert.AreEqual(CorrectNames, testPatient.FirstName, "Somehow assigning a wrong first name");
            Assert.AreEqual(CorrectNames, testPatient.MiddleName, "Somehow assigning a wrong middle name");
            Assert.AreEqual(CorrectNames, testPatient.LastName, "Somehow assigning a wrong last name");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectPersonNames), typeof(TestUtilities))]
        public void Patient_Constructor_InvalidParameters(string IncorrectNames)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            if (string.IsNullOrWhiteSpace(IncorrectNames))
            {
                Assert.ThrowsException<NullReferenceException>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Not checking for empty strings in first name");
                Assert.ThrowsException<NullReferenceException>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Not checking for empty strings in middle name");
                Assert.ThrowsException<NullReferenceException>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Not checking for empty strings in last name");
            }
            else
            {
                Assert.ThrowsException<ArgumentException>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Allowing wrong format on first name");
                Assert.ThrowsException<ArgumentException>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Allowing wrong format on middle name");
                Assert.ThrowsException<ArgumentException>(() => new Patient(IncorrectNames, "CorrectName", "CorrectName", DateTime.Now, testHospital), "Allowing wrong format on last name");
            }

            Assert.ThrowsException<ArgumentException>(() => new Patient("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(1), testHospital), "Not checking for DOB range");
            Assert.ThrowsException<ArgumentException>(() => new Patient("CorrectName", "CorrectName", "CorrectName", DateTime.Now.AddYears(-120), testHospital), "Not checking for DOB range");
            Assert.ThrowsException<ArgumentException>(() => new Patient("CorrectName", "CorrectName", "CorrectName", DateTime.Now, testHospital), "Not checking for empty role assignments");

            Assert.AreEqual(0, Patient.IDManager.GenerateID(), "Adding ID's despite incorrect object creation");
        }

        [TestMethod]
        public void Patient_GenerateCompositeID_Test()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);

            DateTime testDate = DateTime.Now;
            string result = testPatient.GenerateCompositeID(testDate);
            string expected = $"0-{testDate.Year}:{testDate.Month:D2}:{testDate.Day:D2}:{testDate.Hour:D2}";
            Assert.AreEqual(expected, result, "Incorrect ID format");

            result = testPatient.GenerateCompositeID(testDate);
            expected = $"1-{testDate.Year}:{testDate.Month:D2}:{testDate.Day:D2}:{testDate.Hour:D2}";

            Assert.AreEqual(expected, result, "Incorrect ID numeration");
        }

        [TestMethod]
        public void Patient_AddMedicalRecord_ValidParameters()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff(new Staff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, testHospital));
            Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);

            DateTime testDate = DateTime.Now;
            testPatient.AddMedicalRecord(
                new List<Staff> { testHospital.ActiveStaff[0] },
                new List<string> { "diagnoses" },
                new List<string> { "treatments" },
                new List<string> { "medications" },
                testDate);
            string expectedCompositeID = $"0-{testDate.Year}:{testDate.Month:D2}:{testDate.Day:D2}:{testDate.Hour:D2}";
            
            Assert.AreEqual(expectedCompositeID, testPatient.MedicalHistory.First().Key, "Incorrect composite key generation");
            Assert.AreEqual(new List<Staff> { testHospital.ActiveStaff[0] }, testPatient.MedicalHistory.First().Value.ParticipatingStaff, "Incorrect staff assignment");
            Assert.AreEqual(new List<string> { "diagnoses" }, testPatient.MedicalHistory.First().Value.Diagnoses, "Incorrect diagnoses assignment");
            Assert.AreEqual(new List<string> { "treatments" }, testPatient.MedicalHistory.First().Value.Treatments, "Incorrect treatments assignment");
            Assert.AreEqual(new List<string> { "medications" }, testPatient.MedicalHistory.First().Value.Medications, "Incorrect medications assignment");
        }

        [DataTestMethod]
        [DynamicData(nameof(TestUtilities.IncorrectMedicalRecordInfo), typeof(TestUtilities))]
        public void Patient_AddMedicalRecord_InvalidParameters(string invalidMedRecInfo)
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff(new Staff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, testHospital));
            Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);

            DateTime testDate = DateTime.Now;
            Assert.ThrowsException<ArgumentException>(
                () => testPatient.AddMedicalRecord(
                        new List<Staff> { },
                        new List<string> { "diagnoses" },
                        new List<string> { "treatments" },
                        new List<string> { "medications" },
                        testDate )
                , "Not checking for empty staff list");
            Assert.ThrowsException<ArgumentException>(
                () => testPatient.AddMedicalRecord(
                        new List<Staff> { },
                        new List<string> { "diagnoses" },
                        new List<string> { "treatments" },
                        new List<string> { "medications" },
                        DateTime.Now.AddYears(1))
                , "Not checking for date value range");
            Assert.ThrowsException<ArgumentException>(
                () => testPatient.AddMedicalRecord(
                        new List<Staff> { },
                        new List<string> { "diagnoses" },
                        new List<string> { "treatments" },
                        new List<string> { "medications" },
                        DateTime.Now.AddHours(1))
                , "Not checking for date value range");

            if (string.IsNullOrEmpty(invalidMedRecInfo))
            {
                Assert.ThrowsException<NullReferenceException>(
                    () => testPatient.AddMedicalRecord(
                            new List<Staff> { testHospital.ActiveStaff[0] },
                            new List<string> { invalidMedRecInfo },
                            new List<string> { "treatments" },
                            new List<string> { "medications" },
                            testDate)
                    , "Not checking for empty diagnoses list");

                Assert.ThrowsException<NullReferenceException>(
                    () => testPatient.AddMedicalRecord(
                            new List<Staff> { testHospital.ActiveStaff[0] },
                            new List<string> { "diagnoses" },
                            new List<string> { invalidMedRecInfo },
                            new List<string> { "medications" },
                            testDate)
                    , "Not checking for empty treatments list");

                Assert.ThrowsException<NullReferenceException>(
                    () => testPatient.AddMedicalRecord(
                            new List<Staff> { testHospital.ActiveStaff[0] },
                            new List<string> { "diagnoses" },
                            new List<string> { "treatments" },
                            new List<string> { invalidMedRecInfo },
                            testDate)
                    , "Not checking for empty medications list");
            }
            else
            {
                Assert.ThrowsException<ArgumentException>(
                    () => testPatient.AddMedicalRecord(
                            new List<Staff> { testHospital.ActiveStaff[0] },
                            new List<string> { invalidMedRecInfo },
                            new List<string> { "treatments" },
                            new List<string> { "medications" },
                            testDate)
                    , "Not checking for invalid diagnoses format");

                Assert.ThrowsException<ArgumentException>(
                    () => testPatient.AddMedicalRecord(
                            new List<Staff> { testHospital.ActiveStaff[0] },
                            new List<string> { "diagnoses" },
                            new List<string> { invalidMedRecInfo },
                            new List<string> { "medications" },
                            testDate)
                    , "Not checking for invalid treatments format");

                Assert.ThrowsException<ArgumentException>(
                    () => testPatient.AddMedicalRecord(
                            new List<Staff> { testHospital.ActiveStaff[0] },
                            new List<string> { "diagnoses" },
                            new List<string> { "treatments" },
                            new List<string> { invalidMedRecInfo },
                            testDate)
                    , "Not checking for invalid medications format");
            }
        }

        [TestMethod]
        public void Patient_AddAppointment_ValidParameters()
        {
            Hospital testHospital = TestUtilities.DefaultHospital_Testing();
            testHospital.AddStaff(new Staff("CorrectName", "CorrectName", "CorrectName", DateTime.Now, new List<StaffRole> { StaffRole.Administrator }, testHospital));
            Patient testPatient = TestUtilities.DefaultPatient_Testing(testHospital);


        }

        [TestMethod]
        public void Patient_AddAppointment_InvalidParameters()
        {

        }

        [TestMethod]
        public void Patient_GetFullName_Test()
        {

        }

        [TestMethod]
        public void Patient_ChangeInfo_InvalidParameters()
        {

        }

        [TestMethod]
        public void Patient_ToString_Test()
        {

        }
    }

    [TestClass]
    public class MedicalRecord_Testing 
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

        [TestMethod]
        public void MedicalRecord_Constructor_ValidParameters()
        {

        }

        [TestMethod]
        public void MedicalRecord_Constructor_InvalidParameters()
        {

        }

        [TestMethod]
        public void MedicalRecord_ToString_Test()
        {

        }
    }

    [TestClass]
    public class AppointmentSchedule_Testing
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }


    }

    [TestClass]
    public class Appointment_Testing
    {
        [TestInitialize]
        public void Initialize()
        {
            TestUtilities.TestCleanup();
        }

    }
}