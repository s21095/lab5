using cw7.Models;
using cw7.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace cw7.Services
{
    public class DbService : IDbService
    {
        private readonly modelContext _dbContext;

        public DbService(modelContext dbContext)
        {
            _dbContext = dbContext;
        }
       // SomeSortOfTrip
       // SomeSortOfClientTrip
     //   SomeSortOfCountry
      //  SomeSortOfClient
        public async Task<IEnumerable<SomeSortOfTrip>> GetTrips()
        {
            return await _dbContext.Trips
                .Include(e => e.CountryTrips)
                .Include(e => e.ClientTrips)
                .Select(e => new SomeSortOfTrip
                {
                    Name = e.Name,
                    Description = e.Description,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    MaxPeople = e.MaxPeople,
                    Countries = e.CountryTrips.Select(e => new SomeSortOfCountry { Name = e.IdCountryNavigation.Name }).ToList(),
                    Clients = e.ClientTrips.Select(e => new SomeSortOfClient { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName }).ToList()
                })
                .OrderByDescending(e => e.DateFrom)
                .ToListAsync();
        }

        public async Task RemoveTrip(int id)
        {
            Trip trip = new()
            {
                IdTrip = id
            };
            _dbContext.Attach(trip);
            _dbContext.Remove(trip);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddClientToTrip(int id, SomeSortOfClientTrip clientTrip)
        {
            Client client = await _dbContext.Clients.Include(e => e.ClientTrips).Where(e => e.Pesel.Equals(clientTrip.Pesel)).FirstOrDefaultAsync();
            if (client == null)
            {
                client = new Client
                {
                    FirstName = clientTrip.FirstName,
                    LastName = clientTrip.LastName,
                    Email = clientTrip.Email,
                    Pesel = clientTrip.Pesel,
                    Telephone = clientTrip.Telephone
                };
                _dbContext.Clients.Add(client);
                await _dbContext.SaveChangesAsync();
            }
 
            Trip trip = await _dbContext.Trips.Where(e => e.IdTrip == clientTrip.IdTrip).FirstOrDefaultAsync();
            if (trip == null)
            {
                throw new Exception("Wycieczka nie istnieje");
            }
            DateTime now = DateTime.Now;
            ClientTrip clientTrip1 = new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = clientTrip.IdTrip,
                PaymentDate = clientTrip.PaymentDate,
                RegisteredAt = now
            };
            _dbContext.ClientTrips.Add(clientTrip1);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveClient(int id)
        {
            Client client = await _dbContext.Clients.Include(e => e.ClientTrips).Where(e => e.IdClient == id).FirstOrDefaultAsync();
            if (client == null)
            {
                throw new Exception($"Nie znaleziono klienta o ID {id}");
            }
            _dbContext.Remove(client);
            await _dbContext.SaveChangesAsync();
        }

    }
}
