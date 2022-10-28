namespace Core.Domain.Tests
{
    public class CanteenEmployeeTests
    {
        [Fact]
        public void Given_No_Name_Should_Return_Exception()
        {
            //Arrange
            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = null,
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            //Act
            var result = ValidateModel(canteenEmployee).Any(c => c.ErrorMessage == "Naam is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Employee_Number_Should_Return_Exception()
        {
            //Arrange
            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = null,
                Location = Location.HA
            };

            //Act
            var result = ValidateModel(canteenEmployee).Any(c => c.ErrorMessage == "Personeelsnummer is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Location_Should_Return_Exception()
        {
            //Arrange
            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = null
            };

            //Act
            var result = ValidateModel(canteenEmployee).Any(c => c.ErrorMessage == "Locatie is verplicht!");

            //Assert
            Assert.True(result);
        }

        private static IList<ValidationResult> ValidateModel(object obj)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj, null, null);

            Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return validationResults;
        }
    }
}
