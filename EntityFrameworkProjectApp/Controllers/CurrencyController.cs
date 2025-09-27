using EntityFrameworkProjectApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProjectApp.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetCurrencies()
        {
            //var currencies = _appDbContext.Currencies.ToList();

            var currencies = await _appDbContext.Currencies.ToListAsync();

            return Ok(currencies);
        }

        [HttpGet("{id}")]  // retrieving the record using PK.
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
        {
            var currencies = await _appDbContext.Currencies.FindAsync(id);

            return Ok(currencies);
        }
    }
}
