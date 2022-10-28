namespace Portal.Tests
{
    public class LoginViewModelTests
    {
        [Fact]
        public void Given_No_IdentificationNumber_Should_Return_Exception()
        {
            //Arrange
            var loginViewModel = new LoginViewModel()
            {
                IdentificationNumber = null,
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(loginViewModel).Any(c => c.ErrorMessage == "Identificatienummer is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Password_Should_Return_Exception()
        {
            //Arrange
            var loginViewModel = new LoginViewModel()
            {
                IdentificationNumber = "20221008",
                Password = null
            };

            //Act
            var result = ValidateModel(loginViewModel).Any(c => c.ErrorMessage == "Wachtwoord is verplicht!");

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
