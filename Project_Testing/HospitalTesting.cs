namespace Project_Testing
{
    [TestClass]
    public class HospitalTesting
    {
        [TestMethod]
        public void Hospital_ConstructorTesting()
        {
            // there's not gonna be any incorrect names, other than
            // idk, an empty string or something

            string correctName = "Cityscape Medical Center";
            string? incorrectName1 = null;
            string incorrectName2 = " ";
            string incorrectname3 = "";
            string incorrectName4 = "1";
            string incorrectName5 = "abcde12345";
            string incorrectName6 = $"      {correctName}";

            string location = "this can be anything in any format doesn't really matter";

            List<int> rooms = new() { 1, 2, 3, 4, 5 }; // negative numbers will fuck this up
            
        }
    }
}