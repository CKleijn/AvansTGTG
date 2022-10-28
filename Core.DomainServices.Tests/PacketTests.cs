namespace Core.DomainServices.Tests
{
    public class PacketTests
    {
        [Fact]
        public async Task Get_Correct_Packet_By_Given_Packet_Id()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            int packetId = 1;

            var packet = new Packet()
            {
                PacketId = packetId,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Domain.Enums.Location.HA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                IsEightteenPlusPacket = false,
                Price = (decimal?) 2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };
            
            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packetId)).ReturnsAsync(packet);

            //Act
            var result = await packetService.GetPacketByIdAsync(packetId);

            //Assert
            Assert.Equal(packet, result);
        }

        [Fact]
        public void Get_Exception_By_Given_Packet_Id()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            int packetId = 1;

            var packet = new Packet()
            {
                PacketId = packetId,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
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

            Packet? wrongPacket = null;

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packetId)).ReturnsAsync(wrongPacket);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.GetPacketByIdAsync(packetId));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen pakket met dit ID!");
        }

        [Fact]
        public async Task Get_List_Of_All_Packets()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = null,
                    City = Cities.Breda,
                    Canteen = new Canteen()
                    {
                        CanteenId = 1,
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
                },
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                    LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = new Student()
                    {
                        StudentId = 1,
                        Name = "Jane Doe",
                        DateOfBirth = DateTime.Now,
                        StudentNumber = "20221008",
                        EmailAddress = "janedoe@gmail.com",
                        StudyCity = Cities.DenBosch,
                        PhoneNumber = "06 12345678"
                    }
                }
            };

            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetPacketsAsync();

            //Assert
            Assert.Equal(packets, result);
            Assert.Equal(2, result.Count());
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Get_Empty_List_Of_Packets()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var packets = new List<Packet>()
            {

            };

            packetRepoMock.Setup(c => c.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetPacketsAsync();

            //Assert
            Assert.Equal(packets, result);
            Assert.Empty(packets);
        }

        [Fact]
        public async Task Get_List_Of_All_Available_Packets()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = DateTime.Now.AddHours(3),
                LatestPickUpTime = DateTime.Now.AddHours(4),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var packets = new List<Packet>()
            {
                packet,
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = new Student()
                    {
                        StudentId = 1,
                        Name = "Jane Doe",
                        DateOfBirth = DateTime.Now,
                        StudentNumber = "20221008",
                        EmailAddress = "janedoe@gmail.com",
                        StudyCity = Cities.DenBosch,
                        PhoneNumber = "06 12345678"
                    }
                }
            };

            var correctPacket = new List<Packet>()
            {
                packet
            };

            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetAllAvailablePacketsAsync();

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Single(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Get_Empty_List_Of__All_Available_Packets_Because_Of_Reserved_By_Student()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = new Student()
                    {
                        StudentId = 1,
                        Name = "Jane Doe",
                        DateOfBirth = DateTime.Now,
                        StudentNumber = "20221008",
                        EmailAddress = "janedoe@gmail.com",
                        StudyCity = Cities.DenBosch,
                        PhoneNumber = "06 12345678"
                    }
                }
            };

            var correctPacket = new List<Packet>()
            {

            };

            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetAllAvailablePacketsAsync();

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_Empty_List_Of__All_Available_Packets_Because_Of_DateTime_From_The_Past()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                    LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = null
                }
            };

            var correctPacket = new List<Packet>()
            {

            };

            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetAllAvailablePacketsAsync();

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_List_Of_My_Canteen_Packets()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddHours(3),
                LatestPickUpTime = DateTime.Now.AddHours(4),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var packets = new List<Packet>()
            {
                packet,
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = null
                }
            };

            var correctPacket = new List<Packet>()
            {
                packet
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetMyCanteenOfferedPacketsAsync(canteenEmployee.EmployeeNumber);

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Single(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Get_Empty_List_Of_My_Canteen_Packets_Because_Of_Other_Canteen_Packets()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.LA,
                OfferingHotMeals = false
            };

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = null,
                    City = Cities.Breda,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)2.99,
                    MealType = MealTypes.Bread,
                    ReservedBy = null
                },
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = null
                }
            };

            var correctPacket = new List<Packet>()
            {

            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetMyCanteenOfferedPacketsAsync(canteenEmployee.EmployeeNumber);

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_Empty_List_Of_My_Canteen_Packets_Because_Of_DateTime_From_The_Past()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.LA,
                OfferingHotMeals = false
            };

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = null,
                    City = Cities.Breda,
                    Canteen = canteen,
                    PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                    LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)2.99,
                    MealType = MealTypes.Bread,
                    ReservedBy = null
                },
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = canteen,
                    PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                    LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = null
                }
            };

            var correctPacket = new List<Packet>()
            {

            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetMyCanteenOfferedPacketsAsync(canteenEmployee.EmployeeNumber);

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_List_Of_Other_Canteen_Packets()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 2,
                Name = "Pakket2",
                Products = null,
                City = Cities.DenBosch,
                Canteen = new Canteen()
                {
                    CanteenId = 2,
                    City = Cities.DenBosch,
                    Location = Location.OA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = DateTime.Now.AddHours(3),
                LatestPickUpTime = DateTime.Now.AddHours(4),
                IsEightteenPlusPacket = false,
                Price = (decimal?)4.99,
                MealType = MealTypes.Snack,
                ReservedBy = null
            };

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = null,
                    City = Cities.Breda,
                    Canteen = canteen,
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)2.99,
                    MealType = MealTypes.Bread,
                    ReservedBy = null
                },
                packet
            };

            var correctPacket = new List<Packet>()
            {
                packet
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetOtherCanteenOfferedPacketsAsync(canteenEmployee.EmployeeNumber);

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Single(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Get_Empty_List_Of_Other_Canteen_Packets_Because_Of_CanteenEmployee_Canteen()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.LA,
                OfferingHotMeals = false
            };

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = null,
                    City = Cities.Breda,
                    Canteen = canteen,
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)2.99,
                    MealType = MealTypes.Bread,
                    ReservedBy = null
                },
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = canteen,
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = null
                }
            };

            var correctPacket = new List<Packet>()
            {

            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetOtherCanteenOfferedPacketsAsync(canteenEmployee.EmployeeNumber);

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_Empty_List_Of_Other_Canteen_Packets_Because_Of_DateTime_From_The_Past()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.LA,
                OfferingHotMeals = false
            };

            var packets = new List<Packet>()
            {
                new Packet()
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
                    MealType = MealTypes.Bread,
                    ReservedBy = null
                },
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = null,
                    City = Cities.DenBosch,
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
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = null
                }
            };

            var correctPacket = new List<Packet>()
            {

            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetOtherCanteenOfferedPacketsAsync(canteenEmployee.EmployeeNumber);

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_List_Of_My_Reserved_Packets()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 2,
                    City = Cities.DenBosch,
                    Location = Location.HA,
                    OfferingHotMeals = false
                },
                PickUpDateTime = DateTime.Now.AddHours(3),
                LatestPickUpTime = DateTime.Now.AddHours(4),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = student
            };

            var packets = new List<Packet>()
            {
                packet,
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = null
                }
            };

            var correctPacket = new List<Packet>()
            {
                packet
            };

            studentServiceMock.Setup(s => s.GetStudentByStudentNumberAsync(student.StudentNumber)).ReturnsAsync(student);
            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetMyReservedPacketsAsync(student.StudentNumber);

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Single(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Get_Empty_List_Of_My_Reserved_Packets_Because_Of_Other_Student()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = null,
                    City = Cities.Breda,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)2.99,
                    MealType = MealTypes.Bread,
                    ReservedBy = new Student()
                    {
                        StudentId = 2,
                        Name = "Jake Doe",
                        DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                        StudentNumber = "26102022",
                        EmailAddress = "jakedoe@gmail.com",
                        StudyCity = Cities.Breda,
                        PhoneNumber = "06 12345678"
                    }
                },
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = DateTime.Now.AddHours(3),
                    LatestPickUpTime = DateTime.Now.AddHours(4),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = null
                }
            };

            var correctPacket = new List<Packet>()
            {

            };

            studentServiceMock.Setup(s => s.GetStudentByStudentNumberAsync(student.StudentNumber)).ReturnsAsync(student);
            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetMyReservedPacketsAsync(student.StudentNumber);

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_Empty_List_Of_My_Reserved_Packets_Because_Of_DateTime_From_The_Past()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = null,
                    City = Cities.Breda,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                    LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)2.99,
                    MealType = MealTypes.Bread,
                    ReservedBy = student
                },
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = null,
                    City = Cities.DenBosch,
                    Canteen = new Canteen()
                    {
                        CanteenId = 2,
                        City = Cities.DenBosch,
                        Location = Location.HA,
                        OfferingHotMeals = false
                    },
                    PickUpDateTime = new DateTime(2020, 10, 10, 10, 10, 10),
                    LatestPickUpTime = new DateTime(2020, 10, 10, 11, 10, 10),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = student
                }
            };

            var correctPacket = new List<Packet>()
            {

            };

            studentServiceMock.Setup(s => s.GetStudentByStudentNumberAsync(student.StudentNumber)).ReturnsAsync(student);
            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.GetMyReservedPacketsAsync(student.StudentNumber);

            //Assert
            Assert.Equal(correctPacket, result);
            Assert.Empty(result);
        }

        [Fact]
        public void Get_No_Canteen_Employee_Exception_When_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var employeeNumber = "20221008";

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber)).ThrowsAsync(new Exception("Er bestaat geen kantine medewerker met dit personeelsnummer!"));
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.CreatePacketAsync(packet, employeeNumber, new List<string>()));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen kantine medewerker met dit personeelsnummer!");
        }

        [Fact]
        public void Get_No_Canteen_Exception_When_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ThrowsAsync(new Exception("Er bestaat geen kantine met deze locatie!"));

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.CreatePacketAsync(packet, canteenEmployee.EmployeeNumber, new List<string>()));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen kantine met deze locatie!");
        }

        [Fact]
        public void Get_No_Products_Exception_When_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.CreatePacketAsync(packet, canteenEmployee.EmployeeNumber, new List<string>()));

            //Arrange
            Assert.True(result.Result.Message == "Producten zijn verplicht!");
        }

        [Fact]
        public void Get_Not_Offering_Hotmeals_Exception_When_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.CreatePacketAsync(packet, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Je kantine biedt geen warme maaltijden aan!");
        }

        [Fact]
        public void Get_Pick_Up_DateTime_Is_In_The_Past_Exception_When_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = true
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddDays(-1),
                LatestPickUpTime = DateTime.Now,
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.CreatePacketAsync(packet, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Deze datum en/of tijd is onmogelijk!");
        }

        [Fact]
        public void Get_Pick_Up_DateTime_Is_After_Latest_Pick_Up_DateTime_Exception_When_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = true
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddDays(-1),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.CreatePacketAsync(packet, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Deze datum en/of tijd is onmogelijk!");
        }

        [Fact]
        public void Get_Pick_Up_DateTime_Is_More_Than_2_Days_After_Current_DateTime_Exception_When_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = true
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddDays(3),
                LatestPickUpTime = DateTime.Now.AddDays(4),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.CreatePacketAsync(packet, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Je mag maar maximaal 2 dagen vooruit plannen!");
        }

        [Fact]
        public void Get_Latest_Pick_Up_DateTime_Is_On_Another_Day_Then_Pick_Up_DateTime_Exception_When_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = true
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.CreatePacketAsync(packet, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "De uiterlijke afhaaltijd moet plaatsvinden op dezelfde dag als de ophaaldag!");
        }

        [Fact]
        public void Get_No_Product_Found_Exception_When_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = true
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddHours(2),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            productServiceMock.Setup(p => p.GetProductsFromStringProductsAsync(productStringNames)).ThrowsAsync(new Exception("Er bestaat geen product met deze naam!"));

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.CreatePacketAsync(packet, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen product met deze naam!");
        }

        [Fact]
        public void Get_Not_Created_Exception_After_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = true
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var containsAlchohol = true;

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddHours(2),
                IsEightteenPlusPacket = containsAlchohol,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            productServiceMock.Setup(p => p.GetProductsFromStringProductsAsync(productStringNames)).ReturnsAsync(products);
            productServiceMock.Setup(p => p.CheckAlcoholReturnBoolean(products)).Returns(containsAlchohol);
            packetRepoMock.Setup(p => p.CreatePacketAsync(packet)).ReturnsAsync(false);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.CreatePacketAsync(packet, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Het pakket is niet aangemaakt!");
        }

        [Fact]
        public async Task Get_Created_Packet_After_Creating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = true
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var containsAlchohol = true;

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddHours(2),
                IsEightteenPlusPacket = containsAlchohol,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            productServiceMock.Setup(p => p.GetProductsFromStringProductsAsync(productStringNames)).ReturnsAsync(products);
            productServiceMock.Setup(p => p.CheckAlcoholReturnBoolean(products)).Returns(containsAlchohol);
            packetRepoMock.Setup(p => p.CreatePacketAsync(packet)).ReturnsAsync(true);

            //Act
            var result = await packetService.CreatePacketAsync(packet, canteenEmployee.EmployeeNumber, productStringNames);

            //Arrange
            Assert.Equal(packet, result);
        }

        [Fact]
        public void Get_Packet_Not_Found_Exception_When_Reserving_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ThrowsAsync(new Exception("Er bestaat geen pakket met dit ID!"));
            studentServiceMock.Setup(s => s.GetStudentByStudentNumberAsync(student.StudentNumber)).ReturnsAsync(student);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.ReservePacketAsync(packet.PacketId, student.StudentNumber));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen pakket met dit ID!");
        }

        [Fact]
        public void Get_Student_Not_Found_Exception_When_Reserving_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            studentServiceMock.Setup(s => s.GetStudentByStudentNumberAsync("25102022")).ThrowsAsync(new Exception("Er bestaat geen student met dit studentennummer!"));

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.ReservePacketAsync(packet.PacketId, "25102022"));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen student met dit studentennummer!");
        }

        [Fact]
        public void Get_Already_Reserved_Exception_When_Reserving_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = student
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            studentServiceMock.Setup(s => s.GetStudentByStudentNumberAsync(student.StudentNumber)).ReturnsAsync(student);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.ReservePacketAsync(packet.PacketId, student.StudentNumber));

            //Arrange
            Assert.True(result.Result.Message == "Je kan dit pakket niet reserveren, omdat deze al gereserveerd is door een andere student!");
        }

        [Fact]
        public void Get_Already_Packet_Reserved_On_Same_Day_Exception_When_Reserving_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = new DateTime(2000, 10, 10, 10, 10, 10),
                LatestPickUpTime = new DateTime(2000, 10, 10, 10, 10, 20),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            ProductId = 1,
                            Name = "Bier",
                            IsAlcoholic = true,
                            Picture = null
                        }
                    },
                    City = Cities.Breda,
                    Canteen = new Canteen()
                    {
                        CanteenId = 1,
                        City = Cities.Breda,
                        Location = Location.HA,
                        OfferingHotMeals = true
                    },
                    PickUpDateTime = new DateTime(2000,10,10,10,10,10),
                    LatestPickUpTime = new DateTime(2000,10,10,10,10,20),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)2.99,
                    MealType = MealTypes.Bread,
                    ReservedBy = null
                },
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            ProductId = 1,
                            Name = "Bier",
                            IsAlcoholic = true,
                            Picture = null
                        }
                    },
                    City = Cities.Breda,
                    Canteen = new Canteen()
                    {
                        CanteenId = 1,
                        City = Cities.Breda,
                        Location = Location.HA,
                        OfferingHotMeals = true
                    },
                    PickUpDateTime = new DateTime(2000,10,10,10,10,10),
                    LatestPickUpTime = new DateTime(2000,10,10,10,10,20),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = student
                }
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            studentServiceMock.Setup(s => s.GetStudentByStudentNumberAsync(student.StudentNumber)).ReturnsAsync(student);
            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.ReservePacketAsync(packet.PacketId, student.StudentNumber));

            //Arrange
            Assert.True(result.Result.Message == "Je kan dit pakket niet reserveren, omdat je al meer dan 1 pakket op deze afhaaldatum hebt!");
        }

        [Fact]
        public void Get_Products_Are_18_Plus_Exception_When_Reserving_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2008, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = true,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            studentServiceMock.Setup(s => s.GetStudentByStudentNumberAsync(student.StudentNumber)).ReturnsAsync(student);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.ReservePacketAsync(packet.PacketId, student.StudentNumber));

            //Arrange
            Assert.True(result.Result.Message == "Je kan dit pakket niet reserveren, omdat dit pakket 18+ producten bevat!");
        }

        [Fact]
        public void Get_Packet_Not_Reserved_Exception_When_Reserving_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2002, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = true,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            studentServiceMock.Setup(s => s.GetStudentByStudentNumberAsync(student.StudentNumber)).ReturnsAsync(student);
            packetRepoMock.Setup(p => p.UpdatePacketAsync(packet.PacketId)).ReturnsAsync(false);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.ReservePacketAsync(packet.PacketId, student.StudentNumber));

            //Arrange
            Assert.True(result.Result.Message == "Het pakket is niet gereserveerd!");
        }

        [Fact]
        public async Task Get_No_Exceptions_When_Reserving_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2002, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = true,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            studentServiceMock.Setup(s => s.GetStudentByStudentNumberAsync(student.StudentNumber)).ReturnsAsync(student);
            packetRepoMock.Setup(p => p.UpdatePacketAsync(packet.PacketId)).ReturnsAsync(true);

            //Act
            var result = await packetService.ReservePacketAsync(packet.PacketId, student.StudentNumber);

            //Arrange
            Assert.True(result);
        }

        [Fact]
        public void Get_Packet_Not_Found_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ThrowsAsync(new Exception("Er bestaat geen pakket met dit ID!"));
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, new List<string>()));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen pakket met dit ID!");
        }

        [Fact]
        public void Get_Canteen_Employee_Is_Not_Found_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ThrowsAsync(new Exception("Er bestaat geen kantine medewerker met dit personeelsnummer!"));
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, new List<string>()));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen kantine medewerker met dit personeelsnummer!");
        }

        [Fact]
        public void Get_Canteen_Not_Found_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.LA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ThrowsAsync(new Exception("Er bestaat geen kantine met deze locatie!"));

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, new List<string>()));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen kantine met deze locatie!");
        }

        [Fact]
        public void Get_Packet_Is_Not_From_Your_Canteen_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, new List<string>()));

            //Arrange
            Assert.True(result.Result.Message == "Dit pakket is niet van jouw kantine!");
        }

        [Fact]
        public void Get_Already_Reserved_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = new Student()
                {
                    StudentId = 1,
                    Name = "Jane Doe",
                    DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                    StudentNumber = "25102022",
                    EmailAddress = "janedoe@gmail.com",
                    StudyCity = Cities.DenBosch,
                    PhoneNumber = "06 12345678"
                }
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, new List<string>()));

            //Arrange
            Assert.True(result.Result.Message == "Je kan dit pakket niet bewerken, omdat deze al gereserveerd is!");
        }

        [Fact]
        public void Get_No_Products_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = null,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, new List<string>()));

            //Arrange
            Assert.True(result.Result.Message == "Producten zijn verplicht!");
        }

        [Fact]
        public void Get_Not_Offering_Hotmeals_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Je kantine biedt geen warme maaltijden aan!");
        }

        [Fact]
        public void Get_Pick_Up_DateTime_Is_In_The_Past_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddDays(-1),
                LatestPickUpTime = DateTime.Now,
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Deze datum en/of tijd is onmogelijk!");
        }

        [Fact]
        public void Get_Pick_Up_DateTime_Is_After_Latest_Pick_Up_DateTime_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddDays(1),
                LatestPickUpTime = DateTime.Now,
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Deze datum en/of tijd is onmogelijk!");
        }

        [Fact]
        public void Get_Pick_Up_DateTime_Is_More_Than_2_Days_After_Current_DateTime_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddDays(3),
                LatestPickUpTime = DateTime.Now.AddDays(4),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Je mag maar maximaal 2 dagen vooruit plannen!");
        }

        [Fact]
        public void Get_Latest_Pick_Up_DateTime_Is_On_Another_Day_Then_Pick_Up_DateTime_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "De uiterlijke afhaaltijd moet plaatsvinden op dezelfde dag als de ophaaldag!");
        }

        [Fact]
        public void Get_Product_Name_Not_Found_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddHours(3),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            productServiceMock.Setup(p => p.GetProductsFromStringProductsAsync(productStringNames)).ThrowsAsync(new Exception("Er bestaat geen product met deze naam!"));

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen product met deze naam!");
        }

        [Fact]
        public void Get_Packet_Not_Updated_Exception_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var containsAlchohol = true;

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddHours(3),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            productServiceMock.Setup(p => p.GetProductsFromStringProductsAsync(productStringNames)).ReturnsAsync(products);
            productServiceMock.Setup(p => p.CheckAlcoholReturnBoolean(products)).Returns(containsAlchohol);
            packetRepoMock.Setup(p => p.UpdatePacketAsync(packet.PacketId)).ReturnsAsync(false);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, productStringNames));

            //Arrange
            Assert.True(result.Result.Message == "Het pakket is niet bewerkt!");
        }

        [Fact]
        public async Task Get_No_Exceptions_When_Updating_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var canteen = new Canteen()
            {
                CanteenId = 1,
                City = Cities.Breda,
                Location = Location.HA,
                OfferingHotMeals = false
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Bier",
                    IsAlcoholic = true,
                    Picture = null
                }
            };

            var productStringNames = new List<string>()
            {
                "Bier"
            };

            var containsAlchohol = true;

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now,
                LatestPickUpTime = DateTime.Now.AddMinutes(10),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            var newPacket = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1.0",
                Products = products,
                City = Cities.Breda,
                Canteen = canteen,
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddHours(3),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.Bread,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            canteenServiceMock.Setup(c => c.GetCanteenByLocationAsync((Location)canteen.Location)).ReturnsAsync(canteen);
            productServiceMock.Setup(p => p.GetProductsFromStringProductsAsync(productStringNames)).ReturnsAsync(products);
            productServiceMock.Setup(p => p.CheckAlcoholReturnBoolean(products)).Returns(containsAlchohol);
            packetRepoMock.Setup(p => p.UpdatePacketAsync(packet.PacketId)).ReturnsAsync(true);

            //Act
            var result = await packetService.UpdatePacketAsync(packet.PacketId, newPacket, canteenEmployee.EmployeeNumber, productStringNames);

            //Arrange
            Assert.True(result);
        }

        [Fact]
        public void Get_Packet_Not_Found_Exception_When_Deleting_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = true,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ThrowsAsync(new Exception("Er bestaat geen pakket met dit ID!"));
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.DeletePacketAsync(packet.PacketId, canteenEmployee.EmployeeNumber));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen pakket met dit ID!");
        }

        [Fact]
        public void Get_Canteen_Employee_Not_Found_Exception_When_Deleting_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = true,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ThrowsAsync(new Exception("Er bestaat geen kantine medewerker met dit personeelsnummer!"));

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.DeletePacketAsync(packet.PacketId, canteenEmployee.EmployeeNumber));

            //Arrange
            Assert.True(result.Result.Message == "Er bestaat geen kantine medewerker met dit personeelsnummer!");
        }

        [Fact]
        public void Get_Packet_Is_Not_From_Your_Canteen_Exception_When_Deleting_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.LA
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = true,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.DeletePacketAsync(packet.PacketId, canteenEmployee.EmployeeNumber));

            //Arrange
            Assert.True(result.Result.Message == "Dit pakket is niet van jouw kantine!");
        }

        [Fact]
        public void Get_Packet_Is_Already_Reserved_Exception_When_Deleting_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = true,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = new Student()
                {
                    StudentId = 1,
                    Name = "Jane Doe",
                    DateOfBirth = new DateTime(2008, 10, 10, 10, 10, 10),
                    StudentNumber = "25102022",
                    EmailAddress = "janedoe@gmail.com",
                    StudyCity = Cities.DenBosch,
                    PhoneNumber = "06 12345678"
                }
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);

            //Act
            var result = Record.ExceptionAsync(async() => await packetService.DeletePacketAsync(packet.PacketId, canteenEmployee.EmployeeNumber));

            //Arrange
            Assert.True(result.Result.Message == "Je kan dit pakket niet verwijderen, omdat deze al gereserveerd is!");
        }

        [Fact]
        public void Get_Packet_Not_Deleted_Exception_When_Deleting_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = true,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            packetRepoMock.Setup(p => p.DeletePacketAsync(packet.PacketId)).ReturnsAsync(false);

            //Act
            var result = Record.ExceptionAsync(async () => await packetService.DeletePacketAsync(packet.PacketId, canteenEmployee.EmployeeNumber));

            //Arrange
            Assert.True(result.Result.Message == "Het pakket is niet verwijderd!");
        }

        [Fact]
        public async Task Get_No_Exceptions_When_Deleting_Packet()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                Name = "John Doe",
                EmployeeNumber = "20221008",
                Location = Location.HA
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = DateTime.Now.AddHours(1),
                LatestPickUpTime = DateTime.Now.AddDays(1),
                IsEightteenPlusPacket = true,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            packetRepoMock.Setup(p => p.GetPacketByIdAsync(packet.PacketId)).ReturnsAsync(packet);
            canteenEmployeeServiceMock.Setup(c => c.GetCanteenEmployeeByEmployeeNumberAsync(canteenEmployee.EmployeeNumber)).ReturnsAsync(canteenEmployee);
            packetRepoMock.Setup(p => p.DeletePacketAsync(packet.PacketId)).ReturnsAsync(true);

            //Act
            var result = await packetService.DeletePacketAsync(packet.PacketId, canteenEmployee.EmployeeNumber);

            //Arrange
            Assert.True(result);
        }

        [Fact]
        public async Task Check_Reserved_Pick_Up_DateTime_Returns_True()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = new DateTime(2000, 10, 10, 10, 10, 10),
                LatestPickUpTime = new DateTime(2000, 10, 10, 10, 10, 20),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            ProductId = 1,
                            Name = "Bier",
                            IsAlcoholic = true,
                            Picture = null
                        }
                    },
                    City = Cities.Breda,
                    Canteen = new Canteen()
                    {
                        CanteenId = 1,
                        City = Cities.Breda,
                        Location = Location.HA,
                        OfferingHotMeals = true
                    },
                    PickUpDateTime = new DateTime(2000,10,10,10,10,10),
                    LatestPickUpTime = new DateTime(2000,10,10,10,10,20),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)2.99,
                    MealType = MealTypes.Bread,
                    ReservedBy = null
                },
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            ProductId = 1,
                            Name = "Bier",
                            IsAlcoholic = true,
                            Picture = null
                        }
                    },
                    City = Cities.Breda,
                    Canteen = new Canteen()
                    {
                        CanteenId = 1,
                        City = Cities.Breda,
                        Location = Location.HA,
                        OfferingHotMeals = true
                    },
                    PickUpDateTime = new DateTime(2000,10,10,10,10,10),
                    LatestPickUpTime = new DateTime(2000,10,10,10,10,20),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = student
                }
            };

            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.CheckReservedPickUpDate(student, packet);

            //Arrange
            Assert.True(result);
        }

        [Fact]
        public async Task Check_Reserved_Pick_Up_DateTime_Returns_False()
        {
            //Arrange
            var packetRepoMock = new Mock<IPacketRepository>();
            var canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var canteenServiceMock = new Mock<ICanteenService>();
            var studentServiceMock = new Mock<IStudentService>();
            var productServiceMock = new Mock<IProductService>();

            var packetService = new PacketService(packetRepoMock.Object, canteenEmployeeServiceMock.Object, canteenServiceMock.Object, studentServiceMock.Object, productServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(2000, 10, 10, 10, 10, 10),
                StudentNumber = "25102022",
                EmailAddress = "janedoe@gmail.com",
                StudyCity = Cities.DenBosch,
                PhoneNumber = "06 12345678"
            };

            var packet = new Packet()
            {
                PacketId = 1,
                Name = "Pakket1",
                Products = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = 1,
                        Name = "Bier",
                        IsAlcoholic = true,
                        Picture = null
                    }
                },
                City = Cities.Breda,
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    City = Cities.Breda,
                    Location = Location.HA,
                    OfferingHotMeals = true
                },
                PickUpDateTime = new DateTime(2001, 11, 6, 10, 10, 10),
                LatestPickUpTime = new DateTime(2001, 11, 6, 10, 10, 20),
                IsEightteenPlusPacket = false,
                Price = (decimal?)2.99,
                MealType = MealTypes.WarmDinner,
                ReservedBy = null
            };

            var packets = new List<Packet>()
            {
                new Packet()
                {
                    PacketId = 1,
                    Name = "Pakket1",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            ProductId = 1,
                            Name = "Bier",
                            IsAlcoholic = true,
                            Picture = null
                        }
                    },
                    City = Cities.Breda,
                    Canteen = new Canteen()
                    {
                        CanteenId = 1,
                        City = Cities.Breda,
                        Location = Location.HA,
                        OfferingHotMeals = true
                    },
                    PickUpDateTime = new DateTime(2000,10,10,10,10,10),
                    LatestPickUpTime = new DateTime(2000,10,10,10,10,20),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)2.99,
                    MealType = MealTypes.Bread,
                    ReservedBy = null
                },
                new Packet()
                {
                    PacketId = 2,
                    Name = "Pakket2",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            ProductId = 1,
                            Name = "Bier",
                            IsAlcoholic = true,
                            Picture = null
                        }
                    },
                    City = Cities.Breda,
                    Canteen = new Canteen()
                    {
                        CanteenId = 1,
                        City = Cities.Breda,
                        Location = Location.HA,
                        OfferingHotMeals = true
                    },
                    PickUpDateTime = new DateTime(2000,10,10,10,10,10),
                    LatestPickUpTime = new DateTime(2000,10,10,10,10,20),
                    IsEightteenPlusPacket = false,
                    Price = (decimal?)4.99,
                    MealType = MealTypes.Snack,
                    ReservedBy = student
                }
            };

            packetRepoMock.Setup(p => p.GetPacketsAsync()).ReturnsAsync(packets);

            //Act
            var result = await packetService.CheckReservedPickUpDate(student, packet);

            //Arrange
            Assert.False(result);
        }
    }
}
