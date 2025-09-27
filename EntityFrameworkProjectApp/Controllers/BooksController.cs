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
        public async Task<IActionResult> AddNewBook([FromBody] Book model)
        {
            _appDbContext.Books.Add(model); // Add() is used to insert only one record at a time.
            await _appDbContext.SaveChangesAsync();

            return Ok(model);
        }

        [HttpGet("allBooks")]
        public async Task<IActionResult> GetBooks()
        {
            var books=await _appDbContext.Books.ToListAsync();

            return Ok(books);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AdBooks([FromBody] List<Book> model)
        {
            _appDbContext.Books.AddRange(model); // AddRange() is used to insert multiple record at a time.
            await _appDbContext.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPut("{BookId}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int BookId, [FromBody] Book model)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == BookId);

            if(book == null)
            {
                return NotFound();
            }

            book.Title = model.Title;
            book.Description = model.Description;
            book.NoOfPages = model.NoOfPages;

            await _appDbContext.SaveChangesAsync();

            return Ok(model);
        }
    }
}
