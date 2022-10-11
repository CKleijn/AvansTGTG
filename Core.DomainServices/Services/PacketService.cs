using Core.DomainServices.Interfaces.Services;

namespace Core.DomainServices.Services
{
    public class PacketService : IPacketService
    {
        private readonly IPacketRepository _packetRepository;
        private readonly ICanteenEmployeeService _canteenEmployeeService;
        private readonly ICanteenService _canteenService;
        private readonly IStudentService _studentService;
        private readonly IProductRepository _productRepository;

        public PacketService(IPacketRepository packetRepository, ICanteenEmployeeService canteenEmployeeService, ICanteenService canteenService, IStudentService studentService, IProductRepository productRepository)
        {
            _packetRepository = packetRepository;
            _canteenEmployeeService = canteenEmployeeService;
            _canteenService = canteenService;
            _studentService = studentService;
            _productRepository = productRepository;
        }

        public async Task<Packet> GetPacketByIdAsync(int packetId)
        {
            return await _packetRepository.GetPacketByIdAsync(packetId);
        }

        public async Task<IEnumerable<Packet>> GetPacketsAsync()
        {
            return await _packetRepository.GetPacketsAsync();
        }

        public async Task<IEnumerable<Packet>> GetSpecificCanteenPacketsAsync(Canteen canteen)
        {
            var allPackets = await _packetRepository.GetPacketsAsync();

            return allPackets.Where(p => p.Canteen == canteen);
        }

        public async Task<IEnumerable<Packet>> GetMyCanteenOfferedPacketsAsync(string employeeNumber)
        {
            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            var canteen = await _canteenService.GetCanteenByLocationAsync(canteenEmployee.Location);

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
            var productList = new List<Product>();

            var containsAlchohol = false;

            foreach (var product in products)
            {
                var fullProduct = await _productRepository.GetProductByNameAsync(product);

                if((bool) fullProduct.IsAlcoholic)
                {
                    containsAlchohol = true;
                }

                productList.Add(fullProduct);
            }

            var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            var canteen = await _canteenService.GetCanteenByLocationAsync(canteenEmployee.Location);

            packet.Products = productList;
            packet.IsEightteenPlusPacket = containsAlchohol;
            packet.City = canteen.City;
            packet.Canteen = canteen;

            return await _packetRepository.CreatePacketAsync(packet);
        }

        public async Task<bool> ReservePacketAsync(int packetId, string studentNumber)
        {
            var packet = await _packetRepository.GetPacketByIdAsync(packetId);

            if(packet != null)
            {
                var student = await _studentService.GetStudentByStudentNumberAsync(studentNumber);

                if(student != null)
                {
                    packet.ReservedBy = student;

                    await _packetRepository.UpdatePacketAsync(packet);

                    return true;
                }
            }

            return false;
        }

        //public async Task<bool> UpdatePacketAsync(int packetId, string employeeNumber, Packet newPacket)
        //{
        //    var packet = await _packetRepository.GetPacketByIdAsync(packetId);

        //    if (packet != null)
        //    {
        //        var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

        //        if (canteenEmployee != null)
        //        {
        //            if (packet.Canteen?.Location == canteenEmployee.Location)
        //            {
        //                await _packetRepository.UpdatePacketAsync(newPacket);

        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}

        public async Task<bool> DeletePacketAsync(int packetId, string employeeNumber)
        {
            var packet = await _packetRepository.GetPacketByIdAsync(packetId);

            if(packet != null)
            {
                var canteenEmployee = await _canteenEmployeeService.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

                if(canteenEmployee != null)
                {
                    if(packet.Canteen?.Location == canteenEmployee.Location)
                    {
                        await _packetRepository.DeletePacketAsync(packetId);

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
