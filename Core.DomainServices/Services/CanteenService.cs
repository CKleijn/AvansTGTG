namespace Core.DomainServices.Services
{
    public class CanteenService : ICanteenService
    {
        private readonly ICanteenRepository _canteenRepository;

        public CanteenService(ICanteenRepository canteenRepository)
        {
            _canteenRepository = canteenRepository;
        }

        public async Task<Canteen> GetCanteenByLocationAsync(Location canteenLocation) => await _canteenRepository.GetCanteenByLocationAsync(canteenLocation);

        public async Task<IEnumerable<Canteen>> GetCanteensAsync() => await _canteenRepository.GetCanteensAsync();
    }
}
