namespace Core.Domain.Tests
{
    public class PacketTests
    {
        [Fact]
        public void Given_No_Name_Should_Return_Exception()
        {
            //Arrange
            var packet = new Packet()
            {
                PacketId = 1,
                Name = null,
                Products = null,
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 2,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            //Act
            var result = ValidateModel(packet).Any(c => c.ErrorMessage == "Naam is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Pick_Up_DateTime_Should_Return_Exception()
        {
            //Arrange
            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 2,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = null,
                LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            //Act
            var result = ValidateModel(packet).Any(c => c.ErrorMessage == "Ophaaldatum en tijdstip is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Latest_Pick_Up_Time_Should_Return_Exception()
        {
            //Arrange
            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 2,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                LatestPickUpTime = null,
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            //Act
            var result = ValidateModel(packet).Any(c => c.ErrorMessage == "Uiterlijke ophaal tijdstip is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Price_Should_Return_Exception()
        {
            //Arrange
            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 2,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                IsEightteenPlusPacket = false,
                Price = null,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            //Act
            var result = ValidateModel(packet).Any(c => c.ErrorMessage == "Prijs is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_MealType_Should_Return_Exception()
        {
            //Arrange
            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 2,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = null,
                ReservedBy = null
            };

            //Act
            var result = ValidateModel(packet).Any(c => c.ErrorMessage == "Maaltijd type is verplicht!");

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
