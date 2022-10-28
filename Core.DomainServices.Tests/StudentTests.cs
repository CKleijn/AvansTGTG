namespace Core.DomainServices.Tests
{
    public class StudentTests
    {
        [Fact]
        public async Task Get_Correct_Student_By_Given_Student_Number()
        {
            //Arrange
            var studentRepoMock = new Mock<IStudentRepository>();

            var studentService = new StudentService(studentRepoMock.Object);

            string studentNumber = "25102022";

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = DateTime.Now,
                StudentNumber = studentNumber,
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            studentRepoMock.Setup(s => s.GetStudentByStudentNumberAsync(studentNumber)).ReturnsAsync(student);

            //Act
            var result = await studentService.GetStudentByStudentNumberAsync(studentNumber);

            //Assert
            Assert.Equal(student, result);
        }

        [Fact]
        public void Get_Exception_By_Given_Wrong_Student_Number()
        {
            //Arrange
            var studentRepoMock = new Mock<IStudentRepository>();

            var studentService = new StudentService(studentRepoMock.Object);

            string studentNumber = "25102022";

            Student? wrongStudent = null;

            studentRepoMock.Setup(s => s.GetStudentByStudentNumberAsync(studentNumber)).ReturnsAsync(wrongStudent);

            //Act
            var result = Record.ExceptionAsync(async () => await studentService.GetStudentByStudentNumberAsync(studentNumber));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen student met dit studentennummer!");
        }

        [Fact]
        public async Task Create_Student_By_Given_Student_Return_Student()
        {
            //Arrange
            var studentRepoMock = new Mock<IStudentRepository>();

            var studentService = new StudentService(studentRepoMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = DateTime.Now,
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            studentRepoMock.Setup(s => s.CreateStudentAsync(student)).ReturnsAsync(true);

            //Act
            var result = await studentService.CreateStudentAsync(student);

            //Assert
            Assert.Equal(student, result);
        }

        [Fact]
        public void Create_Student_By_Given_Student_Return_Exception()
        {
            //Arrange
            var studentRepoMock = new Mock<IStudentRepository>();

            var studentService = new StudentService(studentRepoMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = DateTime.Now,
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            studentRepoMock.Setup(s => s.CreateStudentAsync(student)).ReturnsAsync(false);

            //Act
            var result = Record.ExceptionAsync(async () => await studentService.CreateStudentAsync(student));

            //Arrange
            Assert.True(result.Result.Message == "De student is niet aangemaakt!");
        }
    }
}
