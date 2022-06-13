using cw7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IDbService _dbService;

        public ClientController(IDbService dbService)
        {
            this._dbService = dbService;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveClient(int id)
        {
            try
            {
                await _dbService.RemoveClient(id);
                return Ok($"Usunięto klienta o ID {id}");
            }
            catch (Exception e)
            {
               return NotFound(e.Message);
            }
        }
    }
}