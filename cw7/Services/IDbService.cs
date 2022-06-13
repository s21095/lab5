using cw7.Models.DTO;

namespace cw7.Services
{
    public interface IDbService
    {
        public Task<IEnumerable<SomeSortOfTrip>> GetTrips();
        public Task RemoveTrip(int id);
        public Task AddClientToTrip(int id, SomeSortOfClientTrip clientTrip);
        public Task RemoveClient(int id);
      
    }
}
