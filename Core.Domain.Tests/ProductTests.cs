namespace Core.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Given_No_Name_Should_Return_Exception()
        {
            //Arrange
            var product = new Product()
            {
                ProductId = 1,
                Name = null,
                IsAlcoholic = false,
                Picture = new byte[1],
                Packets = new List<Packet>()
            };

            //Act
            var result = ValidateModel(product).Any(c => c.ErrorMessage == "Naam is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_IsAlcoholic_Should_Return_Exception()
        {
            //Arrange
            var product = new Product()
            {
                ProductId = 1,
                Name = "Product1",
                IsAlcoholic = null,
                Picture = new byte[1],
                Packets = new List<Packet>()
            };

            //Act
            var result = ValidateModel(product).Any(c => c.ErrorMessage == "Het aangeven van ofdat een product alcohol bevat is verplicht!");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_No_Picture_Should_Return_Exception()
        {
            //Arrange
            var product = new Product()
            {
                ProductId = 1,
                Name = "Product1",
                IsAlcoholic = false,
                Picture = null,
                Packets = new List<Packet>()
            };

            //Act
            var result = ValidateModel(product).Any(c => c.ErrorMessage == "Foto van een product is verplicht!");

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
