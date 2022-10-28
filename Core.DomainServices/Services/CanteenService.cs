namespace Core.DomainServices.Services
{
    public class CanteenService : ICanteenService
    {
        private readonly ICanteenRepository _canteenRepository;

        public CanteenService(ICanteenRepository canteenRepository)
        {
            _canteenRepository = canteenRepository;
        }

        public async Task<Canteen> GetCanteenByLocationAsync(Location canteenLocation)
        {
            var canteen = await _canteenRepository.GetCanteenByLocationAsync(canteenLocation);

            if (canteen == null)
                throw new Exception("Er bestaat geen kantine met deze locatie!");

            return canteen;
        }

        public async Task<IEnumerable<Canteen>> GetCanteensAsync() => await _canteenRepository.GetCanteensAsync();
    }
}
