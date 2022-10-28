namespace Core.Domain.Tests
{
    public class CanteenTests
    {
        [Fact]
        public void Given_No_City_Should_Return_Exception()
        {
            //Arrange
            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = null,
                Location = Location.LA,
                OfferingHotMeals = false
            };

            //Act
            var result = ValidateModel(canteen).Any(c => c.ErrorMessage == "Stad is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Location_Should_Return_Exception()
        {
            //Arrange
            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = null,
                OfferingHotMeals = false
            };

            //Act
            var result = ValidateModel(canteen).Any(c => c.ErrorMessage == "Locatie is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Offering_Hotmeals_Should_Return_Exception()
        {
            //Arrange
            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.LA,
                OfferingHotMeals = null
            };

            //Act
            var result = ValidateModel(canteen).Any(c => c.ErrorMessage == "Aangeven ofdat een kantine warme maaltijden verkoopt is verplicht!");

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
