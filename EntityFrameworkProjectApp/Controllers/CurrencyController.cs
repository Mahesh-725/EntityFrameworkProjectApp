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

        [HttpGet("{id:int}")]  // retrieving the record using PK.
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
        {
            var currencies = await _appDbContext.Currencies.FindAsync(id);

            return Ok(currencies);
        }

        [HttpGet("{name}")]  // retrieving the record using PK.
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] string name)
        {
            //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).FirstAsync();  //when data present retreives the data,and throws exception if no data match.
            //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).SingleAsync(); //when data present retreives the data,and throws exception if no data match.
            //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).SingleOrDefaultAsync(); // when data present retrieves the data and the data should unique if duplicates present throwa exception, give null if no data match.
            var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).FirstOrDefaultAsync();

            return Ok(currencies);
        }
    }
}
c