using EntityFrameworkProjectApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var books = await _appDbContext.Books.Include(x=>x.Author).ToListAsync();

            return Ok(books);
        }

        [HttpGet("languages")]
        public async Task<IActionResult> GetAllLanguagesAsync()
        {
            var languages = await _appDbContext.Languages.Include(x=>x.Books).ToListAsync();

            return Ok(languages);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AdBooks([FromBody] List<Book> model)
        {
            try
            {
                _appDbContext.Books.AddRange(model);
                await _appDbContext.SaveChangesAsync();

                return Ok(new
                {
                    message = "Books added successfully.",
                    data = model
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Failed to add books.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [HttpPut("{BookId:int}")]
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

        [HttpPut("bulk")]
        public async Task<IActionResult> UpdateBookInBulk()
        {
           await _appDbContext.Books
                .Where(x => x.NoOfPages == 352)
                .ExecuteUpdateAsync(x=>x
                .SetProperty(p => p.Description, "Mahesh"));

            return Ok();
        }

        [HttpDelete("{BookId}")]
        public async Task<IActionResult> DeleteBookById([FromRoute] int BookId)
        {
            var book = new Book { Id = BookId };
            _appDbContext.Entry(book).State = EntityState.Deleted;
            await _appDbContext.SaveChangesAsync();

            //var book = _appDbContext.Books.FirstOrDefault(x => x.Id == BookId);

            //if (book == null)
            //{
            //    return NotFound(new
            //    {
            //        success = false,
            //        message = "Book Record Not Found.",
            //    });
            //}


            //_appDbContext.Books.Remove(book);
            //await _appDbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Book Record Deleted successfully.",
            });
        }

        [HttpDelete("bulk/{BookId}")]
        public async Task<IActionResult> DeleteBooksInBulk([FromRoute] int BookId)
        {

            //var books = await _appDbContext.Books.Where(x => x.Id < 2).ToListAsync();
            //_appDbContext.Books.RemoveRange(books);
            //await _appDbContext.SaveChangesAsync();

            var books = await _appDbContext.Books.Where(x=>x.Id < BookId).ExecuteDeleteAsync();

            return Ok(new
            {
                success = true,
                message = "Book Record Deleted successfully.",
                data=books
            });
        }

    }
}
