using EntityFrameworkProjectApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProjectApp.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BooksController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddBooks([FromBody] Book model)
        {
            _appDbContext.Books.Add(model);
            await _appDbContext.SaveChangesAsync();

            return Ok(model);
        }

        [HttpGet("allBooks")]
        public async Task<IActionResult> GetBooks()
        {
            var books=await _appDbContext.Books.ToListAsync();

            return Ok(books);
        }
    }
}
