using EntityFrameworkProjectApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProjectApp.Controllers
{
    [Route("api/languages")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public LanguageController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> GetLanguages()
        {
            //var languages = _appDbContext.Languages.ToList();
            var languages = await _appDbContext.Languages.ToListAsync();
            return Ok(languages);
        }
    }
}
