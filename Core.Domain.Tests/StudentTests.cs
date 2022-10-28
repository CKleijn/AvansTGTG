namespace Core.Domain.Tests
{
    public class StudentTests
    {
        [Fact]
        public void Given_No_Name_Should_Return_Exception()
        {
            //Arrange
            var student = new Student()
            {
                StudentId = 1,
                Name = null,
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Cities.Breda,
                PhoneNumber = "0612345678"
            };

            //Act
            var result = ValidateModel(student).Any(c => c.ErrorMessage == "Naam is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_DateOfBirth_Should_Return_Exception()
        {
            //Arrange
            var student = new Student()
            {
                StudentId = 1,
                Name = "John Doe",
                DateOfBirth = null,
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Cities.Breda,
                PhoneNumber = "0612345678"
            };

            //Act
            var result = ValidateModel(student).Any(c => c.ErrorMessage == "Geboortedatum is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_StudentNumber_Should_Return_Exception()
        {
            //Arrange
            var student = new Student()
            {
                StudentId = 1,
                Name = "John Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = null,
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Cities.Breda,
                PhoneNumber = "0612345678"
            };

            //Act
            var result = ValidateModel(student).Any(c => c.ErrorMessage == "Studentennummer is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_EmailAddress_Should_Return_Exception()
        {
            //Arrange
            var student = new Student()
            {
                StudentId = 1,
                Name = "John Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "28102022",
                EmailAddress = null,
                StudyCity = Cities.Breda,
                PhoneNumber = "0612345678"
            };

            //Act
            var result = ValidateModel(student).Any(c => c.ErrorMessage == "Emailadres is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_Wrong_Format_EmailAddress_Should_Return_Exception()
        {
            //Arrange
            var student = new Student()
            {
                StudentId = 1,
                Name = "John Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "28102022",
                EmailAddress = "johndoegmailcom",
                StudyCity = Cities.Breda,
                PhoneNumber = "0612345678"
            };

            //Act
            var result = ValidateModel(student).Any(c => c.ErrorMessage == "Voer een geldig emailadres in!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_StudyCity_Should_Return_Exception()
        {
            //Arrange
            var student = new Student()
            {
                StudentId = 1,
                Name = "John Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = null,
                PhoneNumber = "0612345678"
            };

            //Act
            var result = ValidateModel(student).Any(c => c.ErrorMessage == "Studentenstad is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_PhoneNumber_Should_Return_Exception()
        {
            //Arrange
            var student = new Student()
            {
                StudentId = 1,
                Name = "John Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Cities.Breda,
                PhoneNumber = null
            };

            //Act
            var result = ValidateModel(student).Any(c => c.ErrorMessage == "Telefoonnummer is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_Wrong_Format_PhoneNumber_Should_Return_Exception()
        {
            //Arrange
            var student = new Student()
            {
                StudentId = 1,
                Name = "John Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Cities.Breda,
                PhoneNumber = "aaaaaaaa"
            };

            //Act
            var result = ValidateModel(student).Any(c => c.ErrorMessage == "Voor een geldig telefoonnummer in!");

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
