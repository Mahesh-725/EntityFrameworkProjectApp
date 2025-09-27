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

        ////Get one record using Multiple (optional and/or required) parameters
        //[HttpGet("{name}")]  // retrieving the record using PK.
        //public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] string name, [FromQuery] string? description)
        //{
        //    //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).FirstAsync();  //when data present retreives the data,and throws exception if no data match.
        //    //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).SingleAsync(); //when data present retreives the data,and throws exception if no data match.
        //    //var currencies = await _appDbContext.Currencies.Where(x => x.Title == name).SingleOrDefaultAsync(); // when data present retrieves the data and the data should unique if duplicates present throwa exception, give null if no data match.
        //    var currencies = await _appDbContext.Currencies.FirstOrDefaultAsync(x => 
        //        x.Title == name 
        //        && (string.IsNullOrEmpty(description) || x.Description == description));

        //    return Ok(currencies);
        //}

        //Get multiple records using Multiple (optional and/or required) parameters
        [HttpGet("{name}")] 
        public async Task<IActionResult> GetCurrencyByNameAsync([FromRoute] string name, [FromQuery] string? description)
        {
            var currencies = await _appDbContext.Currencies.Where(x =>
                x.Title == name
                && (string.IsNullOrEmpty(description) || x.Description == description)).ToListAsync();

            return Ok(currencies);
        }

        //Get Records Based on IDs(manual)
        //[HttpGet("all")]
        //public async Task<IActionResult> GetCurrenciesByIdsAsync()
        //{
        //    var ids = new List<int> { 1, 3, 5 };
        //    var currencies = await _appDbContext.Currencies
        //        .Where(x => ids.Contains(x.Id)).ToListAsync();

        //    return Ok(currencies);
        //}

        //Get Records Based on IDs(Dynamically)
        [HttpPost("all")]
        public async Task<IActionResult> GetCurrenciesByIdsAsync([FromBody] List<int> ids)
        {
            //var ids = new List<int> { 1, 3, 5 };
            var currencies = await _appDbContext.Currencies
                .Where(x => ids.Contains(x.Id))
                .Select(x=>new Currency() //this select statement helps to retrieve only specifid coulmns here id, title values will be displayed
                {
                    Id=x.Id,
                    Title=x.Title,
                })
                .ToListAsync();

            return Ok(currencies);
        }
    }
}