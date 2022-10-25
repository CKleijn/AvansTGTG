namespace Core.DomainServices.Services
{
    public class PacketService : IPacketService
    {
        private readonly IPacketRepository _packetRepository;
        private readonly ICanteenEmployeeService _canteenEmployeeService;
        private readonly ICanteenService _canteenService;
        private readonly IStudentService _studentService;
        private readonly IProductService _productService;

        public PacketService(IPacketRepository packetRepository, ICanteenEmployeeService canteenEmployeeService, ICanteenService canteenService, IStudentService studentService, IProductService productService)
        {
            _packetRepository = packetRepository;
            _canteenEmployeeService = canteenEmployeeService;
            _canteenService = canteenService;
            _studentService = studentService;
            _productService = productService;
        }

        public async Task<Packet> GetPacketByIdAsync(int packetId) => await _packetRepository.GetPacketByIdAsync(packetId);

        public async Task<IEnumerable<Packet>> GetPacketsAsync() => await _packetRepository.GetPacketsAsync();

        public async Task<IEnumerable<Packet>> GetAllAvailablePacketsAsync()
        {
            var allPackets = await _packetRepository.GetPacketsAsync();

            return allPackets.Where(p => p.ReservedBy == null);
        }

        public async Task<IEnumerable<Packet>> GetMyCanteenOfferedPacketsAsync(string employeeNumber)
        {
            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            var canteen = await _canteenService.GetCanteenByLocationAsync((Location) canteenEmployee.Location!);

            var allPackets = await _packetRepository.GetPacketsAsync();

            return allPackets.Where(p => p.Canteen == canteen);
        }

        public async Task<IEnumerable<Packet>> GetMyReservedPacketsAsync(string studentNumber)
        {
            var student = await _studentService.GetStudentByStudentNumberAsync(studentNumber);

            var allPackets = await _packetRepository.GetPacketsAsync();

            return allPackets.Where(p => p.ReservedBy == student);
        }

        public async Task<Packet> CreatePacketAsync(Packet packet, string employeeNumber, IList<string> products)
        {
            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            var canteen = await _canteenService.GetCanteenByLocationAsync((Location) canteenEmployee.Location!);

            if (products.Count == 0)
                throw new Exception("Producten zijn verplicht!");

            if (packet.MealType != null)
                if ((int)packet.MealType! == 4 && canteen.OfferingHotMeals == false)
                    throw new Exception("Je kantine biedt geen warme maaltijden aan!");

            if (packet.PickUpDateTime < DateTime.Now || packet.PickUpDateTime > packet.LatestPickUpTime)
                throw new Exception("Deze datum en/of tijd is onmogelijk!");

            if (packet.PickUpDateTime > DateTime.Now.AddDays(2))
                throw new Exception("Je mag maar maximaal 2 dagen vooruit plannen!");

            dynamic productList = await _productService.CheckAlcoholReturnProductList(products);

            packet.Products = productList.productList;
            packet.IsEightteenPlusPacket = productList.containsAlchohol;
            packet.City = canteen.City;
            packet.Canteen = canteen;

            return await _packetRepository.CreatePacketAsync(packet);
        }

        public async Task<bool> ReservePacketAsync(int packetId, string studentNumber)
        {
            var packet = await GetPacketByIdAsync(packetId);

            var student = await _studentService.GetStudentByStudentNumberAsync(studentNumber);

            if (packet.ReservedBy != null)
                throw new Exception("Je kan dit pakket niet reserveren, omdat deze al gereserveerd is door een andere student!");

            if (await CheckReservedPickUpDate(student, packet))
                throw new Exception("Je kan dit pakket niet reserveren, omdat je al meer dan 1 pakket op deze afhaaldatum hebt!");

            if ((bool)packet.IsEightteenPlusPacket! && student.DateOfBirth!.Value.AddYears(18) > packet.PickUpDateTime)
                throw new Exception("Je kan dit pakket niet reserveren, omdat dit pakket 18+ producten bevat!");

            packet!.ReservedBy = student;

            return await _packetRepository.UpdatePacketAsync(packetId);
        }

        public async Task<bool> UpdatePacketAsync(int packetId, Packet newPacket, string employeeNumber, IList<string> products)
        {
            var packet = await GetPacketByIdAsync(packetId);

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            var canteen = await _canteenService.GetCanteenByLocationAsync((Location)canteenEmployee.Location!);

            if (packet.Canteen?.Location != canteenEmployee.Location)
                throw new Exception("Dit pakket is niet van jouw kantine!");

            if (packet.ReservedBy != null)
                throw new Exception("Je kan dit pakket niet bewerken, omdat deze al gereserveerd is!");

            if (products.Count == 0)
                throw new Exception("Producten zijn verplicht!");

            if (newPacket.MealType != null)
                if ((int)newPacket.MealType! == 4 && canteen.OfferingHotMeals == false)
                    throw new Exception("Je kantine biedt geen warme maaltijden aan!");

            if (newPacket.PickUpDateTime < DateTime.Now || newPacket.PickUpDateTime > newPacket.LatestPickUpTime)
                throw new Exception("Deze datum en/of tijd is onmogelijk!");

            if (newPacket.PickUpDateTime > DateTime.Now.AddDays(2))
                throw new Exception("Je mag maar maximaal 2 dagen vooruit plannen!");

            dynamic productList = await _productService.CheckAlcoholReturnProductList(products);

            packet.Name = newPacket.Name;
            packet.Products = productList.productList;
            packet.IsEightteenPlusPacket = productList.containsAlchohol;
            packet.MealType = newPacket.MealType;
            packet.Price = newPacket.Price;
            packet.PickUpDateTime = newPacket.PickUpDateTime;
            packet.LatestPickUpTime = newPacket.LatestPickUpTime;

            return await _packetRepository.UpdatePacketAsync(packetId);
        }

        public async Task<bool> DeletePacketAsync(int packetId, string employeeNumber)
        {
            var packet = await GetPacketByIdAsync(packetId);

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            if (packet.Canteen?.Location != canteenEmployee.Location)
                throw new Exception("Dit pakket is niet van jouw kantine!");

            if (packet.ReservedBy != null)
                throw new Exception("Je kan dit pakket niet verwijderen, omdat deze al gereserveerd is!");
                
            return await _packetRepository.DeletePacketAsync(packetId);
        }

        public async Task<bool> CheckReservedPickUpDate(Student student, Packet packet)
        {
            bool reservedPickUpDate = false;

            foreach (var singlePacket in await GetPacketsAsync())
                if (singlePacket.ReservedBy == student && singlePacket?.PickUpDateTime!.Value.DayOfYear == packet?.PickUpDateTime!.Value.DayOfYear)
                    reservedPickUpDate = true;

            return reservedPickUpDate;
        }
    }
}
