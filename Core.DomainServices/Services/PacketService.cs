using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.DomainServices.Interfaces.Services;
using System.Net.Sockets;

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

        public async Task<IEnumerable<Packet>> GetSpecificCanteenPacketsAsync(Canteen canteen)
        {
            var allPackets = await _packetRepository.GetPacketsAsync();

            return allPackets.Where(p => p.Canteen == canteen);
        }

        public async Task<IEnumerable<Packet>> GetMyCanteenOfferedPacketsAsync(string employeeNumber)
        {
            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            var canteen = await _canteenService.GetCanteenByLocationAsync(canteenEmployee.Location!);

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
            dynamic productList = await CheckAlcoholReturnProductList(products);

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            var canteen = await _canteenService.GetCanteenByLocationAsync(canteenEmployee.Location!);

            packet.Products = productList.productList;
            packet.IsEightteenPlusPacket = productList.containsAlchohol;
            packet.City = canteen.City;
            packet.Canteen = canteen;

            return await _packetRepository.CreatePacketAsync(packet);
        }

        public async Task<bool> ReservePacketAsync(int packetId, string studentNumber)
        {
            var packet = await GetPacketByIdAsync(packetId);

            packet.ReservedBy = await _studentService.GetStudentByStudentNumberAsync(studentNumber);

            return await _packetRepository.UpdatePacketAsync(packetId);
        }

        public async Task<bool> UpdatePacketAsync(int packetId, Packet newPacket, string employeeNumber, IList<string> products)
        {
            var packet = await _packetRepository.GetPacketByIdAsync(packetId);

            dynamic productList = await CheckAlcoholReturnProductList(products);

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            packet.Name = newPacket.Name;
            packet.Products = productList.productList;
            packet.IsEightteenPlusPacket = productList.containsAlchohol;
            packet.MealType = newPacket.MealType;
            packet.Price = newPacket.Price;
            packet.PickUpDateTime = newPacket.PickUpDateTime;
            packet.LatestPickUpTime = newPacket.LatestPickUpTime;

            if (packet.Canteen?.Location == canteenEmployee.Location)
                return await _packetRepository.UpdatePacketAsync(packetId);

            return false;
        }

        public async Task<bool> DeletePacketAsync(int packetId, string employeeNumber)
        {
            var packet = await GetPacketByIdAsync(packetId);

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            if(packet.Canteen?.Location == canteenEmployee.Location)
                return await _packetRepository.DeletePacketAsync(packetId);

            return false;
        }

        public async Task<bool> CheckReservedPickUpDate(Student student, Packet packet)
        {
            bool reservedPickUpDate = false;

            foreach (var singlePacket in await GetPacketsAsync())
                if (singlePacket.ReservedBy == student && singlePacket?.PickUpDateTime!.Value.DayOfYear == packet?.PickUpDateTime!.Value.DayOfYear)
                    reservedPickUpDate = true;

            return reservedPickUpDate;
        }

        public async Task<object> CheckAlcoholReturnProductList(IList<string> products)
        {
            var productList = new List<Product>();

            var containsAlchohol = false;

            foreach (var product in products)
            {
                var fullProduct = await _productService.GetProductByNameAsync(product);

                if ((bool)fullProduct.IsAlcoholic!)
                    containsAlchohol = true;

                productList.Add(fullProduct);
            }

            return new
            {
                containsAlchohol,
                productList
            };
        }
    }
}
