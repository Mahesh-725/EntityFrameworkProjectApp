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

        //Get All Records from Database.
        [HttpGet("")]
        public async Task<IActionResult> GetCurrencies()
        {
            //var currencies = _appDbContext.Currencies.ToList();

            var currencies = await _appDbContext.Currencies.ToListAsync();

            return Ok(currencies);
        }

        // retrieving the record using PK.
        [HttpGet("{id:int}")]  
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
        {
            var currencies = await _appDbContext.Currencies.FindAsync(id);

            return Ok(currencies);
        }

        //[HttpGet("{name}/{description}")]
        //public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] string name, [FromRoute] string description)
        //{
        //    //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).FirstAsync();  //when data present retreives the data,and throws exception if no data match.
        //    //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).SingleAsync(); //when data present retreives the data,and throws exception if no data match.
        //    //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).SingleOrDefaultAsync(); // when data present retrieves the data and the data should unique if duplicates present throwa exception, give null if no data match.
        //    var currencies = await _appDbContext.Currencies.FirstOrDefaultAsync(x => x.Title == name && x.Description == description);

        //    return Ok(currencies);
        //}

        //Get one record using Multiple (optional and/or required) parameters
        [HttpGet("{name}")]  // retrieving the record using PK.
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] string name, [FromQuery] string? description)
        {
            //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).FirstAsync();  //when data present retreives the data,and throws exception if no data match.
            //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).SingleAsync(); //when data present retreives the data,and throws exception if no data match.
            //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).SingleOrDefaultAsync(); // when data present retrieves the data and the data should unique if duplicates present throwa exception, give null if no data match.
            var currencies = await _appDbContext.Currencies.FirstOrDefaultAsync(x => 
                x.Title == name 
                && (string.IsNullOrEmpty(description) || x.Description == description));

            return Ok(currencies);
        }
    }
}