using cw7.Models.DTO;
using cw7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public TripsController(IDbService dbService)
        {
            this._dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            await _dbService.RemoveTrip(id);
            return Ok($"Usunieto wyczieczke o ID {id}");
        }

        [HttpPost]
        [Route("{id}/clients")]
        public async Task<IActionResult> AddClientToTrip(int id, SomeSortOfClientTrip clientTrip)
        {
            try
            {
                await _dbService.AddClientToTrip(id, clientTrip);
                return Ok($"Dodano klienta do wycieczki o ID {id}");
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
    }
}
