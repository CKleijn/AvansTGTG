namespace Core.DomainServices.Tests
{
    public class CanteenEmployeeTests
    {
        [Fact]
        public async Task Get_Correct_Canteen_Employee_By_Given_Employee_Number()
        {
            //Arrange
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();

            var canteenEmployeeService = new CanteenEmployeeService(canteenEmployeeRepoMock.Object);

            string employeeNumber = "20221008";

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = employeeNumber,
                Location = Location.LA
            };

            canteenEmployeeRepoMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber)).ReturnsAsync(canteenEmployee);

            //Act
            var result = await canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            //Assert
            Assert.Equal(canteenEmployee, result);
        }

        [Fact]
        public void Get_Wrong_Canteen_Employee_By_Given_Employee_Number()
        {
            //Arrange
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();

            var canteenEmployeeService = new CanteenEmployeeService(canteenEmployeeRepoMock.Object);

            string employeeNumber = "20221008";

            CanteenEmployee? wrongCanteenEmployee = null;

            canteenEmployeeRepoMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber)).ReturnsAsync(wrongCanteenEmployee);

            //Act
            var result = Record.ExceptionAsync(async () => await canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen kantine medewerker met dit personeelsnummer!");
        }

        [Fact]
        public async Task Create_Canteen_Employee_By_Given_Canteen_Employee_Return_Canteen_Employee()
        {
            //Arrange
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();

            var canteenEmployeeService = new CanteenEmployeeService(canteenEmployeeRepoMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            canteenEmployeeRepoMock.Setup(c => c.CreateCanteenEmployeeAsync(canteenEmployee)).ReturnsAsync(true);

            //Act
            var result = await canteenEmployeeService.CreateCanteenEmployeeAsync(canteenEmployee);

            //Assert
            Assert.Equal(canteenEmployee, result);
        }

        [Fact]
        public void Create_Canteen_Employee_By_Given_Canteen_Employee_Return_Exception()
        {
            //Arrange
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();

            var canteenEmployeeService = new CanteenEmployeeService(canteenEmployeeRepoMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            canteenEmployeeRepoMock.Setup(c => c.CreateCanteenEmployeeAsync(canteenEmployee)).ReturnsAsync(false);

            //Act
            var result = Record.ExceptionAsync(async () => await canteenEmployeeService.CreateCanteenEmployeeAsync(canteenEmployee));

            //Arrange
            Assert.True(result.Result.Message == "De kantine medewerker is niet aangemaakt!");
        }
    }
}
