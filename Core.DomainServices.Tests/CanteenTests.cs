namespace Core.DomainServices.Tests
{
    public class CanteenTests
    {
        [Fact]
        public async Task Get_Correct_Canteen_By_Given_Location()
        {
            //Arrange
            var canteenRepoMock = new Mock<ICanteenRepository>();

            var canteenService = new CanteenService(canteenRepoMock.Object);

            var location = Location.LA;

            var canteen = new Canteen() {
                CanteenId = 1,
                City = Cities.Breda,
                Location = location,
                OfferingHotMeals = false
            };

            canteenRepoMock.Setup(c => c.GetCanteenByLocationAsync(location)).ReturnsAsync(canteen);

            //Act
            var result = await canteenService.GetCanteenByLocationAsync(location);

            //Assert
            Assert.Equal(canteen, result);
        }

        [Fact]
        public void Get_Exception_By_Given_Location()
        {
            //Arrange
            var canteenRepoMock = new Mock<ICanteenRepository>();

            var canteenService = new CanteenService(canteenRepoMock.Object);

            var location = Location.PA;

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Tilburg,
                Location = location,
                OfferingHotMeals = false
            };

            Canteen? wrongCanteen = null;

            canteenRepoMock.Setup(c => c.GetCanteenByLocationAsync(location)).ReturnsAsync(wrongCanteen);

            //Act
            var result = Record.ExceptionAsync(async () => await canteenService.GetCanteenByLocationAsync(location));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen kantine met deze locatie!");
        }

        [Fact]
        public async Task Get_List_Of_All_Canteens()
        {
            //Arrange
            var canteenRepoMock = new Mock<ICanteenRepository>();

            var canteenService = new CanteenService(canteenRepoMock.Object);

            var canteens = new List<Canteen>()
            {
                new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.LD,
                    OfferingHotMeals = false
                },
                new Canteen()
                {
                    CanteenId = 2,
                    City = Cities.DenBosch,
                    Location = Location.HD,
                    OfferingHotMeals = true
                }
            };

            canteenRepoMock.Setup(c => c.GetCanteensAsync()).ReturnsAsync(canteens);

            //Act
            var result = await canteenService.GetCanteensAsync();

            //Assert
            Assert.Equal(canteens, result);
            Assert.Equal(2, result.Count());
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Get_Empty_List_Of_All_Canteens()
        {
            //Arrange
            var canteenRepoMock = new Mock<ICanteenRepository>();

            var canteenService = new CanteenService(canteenRepoMock.Object);

            var canteens = new List<Canteen>()
            {

            };

            canteenRepoMock.Setup(c => c.GetCanteensAsync()).ReturnsAsync(canteens);

            //Act
            var result = await canteenService.GetCanteensAsync();

            //Assert
            Assert.Equal(canteens, result);
            Assert.Empty(result);
        }
    }
}
