using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.DomainServices.Interfaces.Repositories;
using Core.DomainServices.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;

namespace Core.DomainServices.Tests
{
    public class ProductTests
    {
        [Fact]
        public async Task Get_All_Products_And_Return_In_SelectList()
        {
            //Arrange
            var productRepoMock = new Mock<IProductRepository>();

            var productService = new ProductService(productRepoMock.Object);

            var productOne = new Product()
            {
                ProductId = 1,
                Name = "Bier",
                IsAlcoholic = true,
                Picture = null
            };

            var productTwo = new Product()
            {
                ProductId = 2,
                Name = "Kiwi",
                IsAlcoholic = false,
                Picture = null
            };

            var products = new List<Product>
            {
                productOne,
                productTwo
            };

            var productSelectList = new List<SelectListItem>
            {
                new SelectListItem { Text = productOne.Name, Value = productOne.Name },
                new SelectListItem { Text = productTwo.Name, Value = productTwo.Name }
            };

            productRepoMock.Setup(p => p.GetProductsAsync()).ReturnsAsync(products);

            //Act
            var result = await productService.GetAllProductsInSelectListAsync();

            //Assert
            Assert.Equal(productSelectList[0].Text, result[0].Text);
            Assert.Equal(productSelectList[1].Text, result[1].Text);
            Assert.Equal(productSelectList[0].Value, result[0].Value);
            Assert.Equal(productSelectList[1].Value, result[1].Value);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Get_All_Products_And_Return_Empty_SelectList()
        {
            //Arrange
            var productRepoMock = new Mock<IProductRepository>();

            var productService = new ProductService(productRepoMock.Object);

            var products = new List<Product>
            {

            };

            var productSelectList = new List<SelectListItem>
            {

            };

            productRepoMock.Setup(p => p.GetProductsAsync()).ReturnsAsync(products);

            //Act
            var result = await productService.GetAllProductsInSelectListAsync();

            //Assert
            Assert.Equal(productSelectList, result);
            Assert.Empty(result);
        }

        [Fact]
        public void Get_All_Products_And_Return_In_String_List()
        {
            //Arrange
            var productRepoMock = new Mock<IProductRepository>();

            var productService = new ProductService(productRepoMock.Object);

            var productOne = new Product()
            {
                ProductId = 1,
                Name = "Bier",
                IsAlcoholic = true,
                Picture = null
            };

            var productTwo = new Product()
            {
                ProductId = 2,
                Name = "Kiwi",
                IsAlcoholic = false,
                Picture = null
            };

            var products = new List<string>
            {
                productOne.Name,
                productTwo.Name
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>
                {
                    productOne,
                    productTwo
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Domain.Enums.Location.HA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = new DateTime(2000, 10, 10, 10, 10, 10),
                LatestPickUpTime = new DateTime(2000, 10, 10, 10, 10, 20),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            //Act
            var result = productService.GetProductsFromPacketInList(packet);

            //Assert
            Assert.Equal(products, result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Get_All_Products_And_Return_Empty_String_List()
        {
            //Arrange
            var productRepoMock = new Mock<IProductRepository>();

            var productService = new ProductService(productRepoMock.Object);

            var products = new List<string>
            {

            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>
                {

                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Domain.Enums.Location.HA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = new DateTime(2000, 10, 10, 10, 10, 10),
                LatestPickUpTime = new DateTime(2000, 10, 10, 10, 10, 20),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            //Act
            var result = productService.GetProductsFromPacketInList(packet);

            //Assert
            Assert.Equal(products, result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_All_Products_And_Return_In_Product_List()
        {
            //Arrange
            var productRepoMock = new Mock<IProductRepository>();

            var productService = new ProductService(productRepoMock.Object);

            var product = new Product()
            {
                ProductId = 1,
                Name = "Bier",
                IsAlcoholic = true,
                Picture = null
            };

            var products = new List<Product>
            {
                product
            };

            var productsString = new List<string>
            {
                product.Name,
            };

            productRepoMock.Setup(p => p.GetProductByNameAsync(product.Name)).ReturnsAsync(product);

            //Act
            var result = await productService.ReturnProductListAsync(productsString);

            //Assert
            Assert.Equal(products, result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Get_All_Products_And_Return_Empty_Product_List()
        {
            //Arrange
            var productRepoMock = new Mock<IProductRepository>();

            var productService = new ProductService(productRepoMock.Object);

            var products = new List<Product>
            {

            };

            var productsString = new List<string>
            {

            };

            //Act
            var result = await productService.ReturnProductListAsync(productsString);

            //Assert
            Assert.Equal(products, result);
            Assert.Empty(result);
        }

        [Fact]
        public void Check_Products_On_Alcohol_Return_True()
        {
            //Arrange
            var productRepoMock = new Mock<IProductRepository>();

            var productService = new ProductService(productRepoMock.Object);

            var product = new Product()
            {
                ProductId = 1,
                Name = "Bier",
                IsAlcoholic = true,
                Picture = null
            };

            var products = new List<Product>
            {
                product
            };

            //Act
            var result = productService.CheckAlcoholReturnBoolean(products);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Check_Products_On_Alcohol_Return_False()
        {
            //Arrange
            var productRepoMock = new Mock<IProductRepository>();

            var productService = new ProductService(productRepoMock.Object);

            var product = new Product()
            {
                ProductId = 1,
                Name = "Kaas",
                IsAlcoholic = false,
                Picture = null
            };

            var products = new List<Product>
            {
                product
            };

            //Act
            var result = productService.CheckAlcoholReturnBoolean(products);

            //Assert
            Assert.False(result);
        }
    }
}
