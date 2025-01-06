using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController
    {
        private readonly ApplicationDbContext _context;
        public PeopleController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            return await _context.People.ToListAsync();
        }
    }
}
