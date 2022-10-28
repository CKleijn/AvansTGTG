namespace Portal.Tests
{
    public class StudentRegisterViewModelTests
    {
        [Fact]
        public void Given_No_FirstName_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = null,
                LastName = "Doe",
                DateOfBirth = DateTime.Today,
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "0612345678",
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Voornaam is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_LastName_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = null,
                DateOfBirth = DateTime.Today,
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "0612345678",
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Achternaam is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_DateOfBirth_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = null,
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "0612345678",
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Geboortedatum is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_DateOfBirth_In_Future_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Now.AddDays(1),
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "0612345678",
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Deze datum is onmogelijk!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Age_Above_16_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2010, 10, 10, 10, 10, 10),
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "0612345678",
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Je moet 16 jaar of ouder zijn voor een account!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_EmployeeNumber_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Today,
                StudentNumber = null,
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "0612345678",
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Studentennummer is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_EmailAddress_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Today,
                StudentNumber = "28102022",
                EmailAddress = null,
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "0612345678",
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Emailadres is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_Wrong_Format_EmailAddress_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Today,
                StudentNumber = "28102022",
                EmailAddress = "johndoegmailcom",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "0612345678",
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Voer een geldig emailadres in!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_StudyCity_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Today,
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = null,
                PhoneNumber = "0612345678",
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Studentenstad is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_PhoneNumber_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Today,
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = null,
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Telefoonnummer is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_Wrong_Format_PhoneNumber_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Today,
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "aaaaaaaa",
                Password = "@BCdefg123"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Voor een geldig telefoonnummer in!");

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void Given_No_Password_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Today,
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "0612345678",
                Password = null
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Wachtwoord is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Correct_Password_Should_Return_Exception()
        {
            //Arrange
            var studentRegisterViewModel = new StudentRegisterViewModel()
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Today,
                StudentNumber = "28102022",
                EmailAddress = "johndoe@gmail.com",
                StudyCity = Core.Domain.Enums.Cities.Breda,
                PhoneNumber = "0612345678",
                Password = "@BCde"
            };

            //Act
            var result = ValidateModel(studentRegisterViewModel).Any(c => c.ErrorMessage == "Je wachtwoord moet minimaal bestaan uit 8 karakters waarvan 1 cijfer en 1 speciale karakter!");

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

