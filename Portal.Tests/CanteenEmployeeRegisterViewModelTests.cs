namespace Core.Domain.Tests
{
    public class CanteenEmployeeRegisterViewModelTests
    {
        [Fact]
        public void Given_No_FirstName_Should_Return_Exception()
        {
            //Arrange
            var canteenEmployeeRegisterViewModel = new CanteenEmployeeRegisterViewModel()
            {
                FirstName = null,
                LastName = "Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA,
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(canteenEmployeeRegisterViewModel).Any(c => c.ErrorMessage == "Voornaam is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_LastName_Should_Return_Exception()
        {
            //Arrange
            var canteenEmployeeRegisterViewModel = new CanteenEmployeeRegisterViewModel()
            {
                FirstName = "John",
                LastName = null,
                EmployeeNumber = "20221008",
                Location = Location.LA,
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(canteenEmployeeRegisterViewModel).Any(c => c.ErrorMessage == "Achternaam is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_EmployeeNumber_Should_Return_Exception()
        {
            //Arrange
            var canteenEmployeeRegisterViewModel = new CanteenEmployeeRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                EmployeeNumber = null,
                Location = Location.LA,
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(canteenEmployeeRegisterViewModel).Any(c => c.ErrorMessage == "Personeelsnummer is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Location_Should_Return_Exception()
        {
            //Arrange
            var canteenEmployeeRegisterViewModel = new CanteenEmployeeRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                EmployeeNumber = "20221008",
                Location = null,
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(canteenEmployeeRegisterViewModel).Any(c => c.ErrorMessage == "Kantine is verplicht!");

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void Given_No_Password_Should_Return_Exception()
        {
            //Arrange
            var canteenEmployeeRegisterViewModel = new CanteenEmployeeRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA,
                Password = null
            };

            //Act
            var result = ValidateModel(canteenEmployeeRegisterViewModel).Any(c => c.ErrorMessage == "Wachtwoord is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Correct_Password_Should_Return_Exception()
        {
            //Arrange
            var canteenEmployeeRegisterViewModel = new CanteenEmployeeRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA,
                Password = "@BCde"
            };

            //Act
            var result = ValidateModel(canteenEmployeeRegisterViewModel).Any(c => c.ErrorMessage == "Je wachtwoord moet minimaal bestaan uit 8 karakters waarvan 1 cijfer en 1 speciale karakter!");

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
