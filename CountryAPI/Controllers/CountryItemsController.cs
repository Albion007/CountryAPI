#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountryAPI.models;

namespace CountryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryItemsController : ControllerBase
    {
        private readonly CountryContext _context;

        public CountryItemsController(CountryContext context)
        {
            _context = context;
        }

        // GET: api/CountryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryItem>>> GetCountryItems()
        {
            return await _context.CountryItems.ToListAsync();
        }

        // GET: api/CountryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryItem>> GetCountryItem(long id)
        {
            var countryItem = await _context.CountryItems.FindAsync(id);

            if (countryItem == null)
            {
                return NotFound();
            }

            return countryItem;
        }

        // PUT: api/CountryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryItem(long id, CountryItem countryItem)
        {
            if (id != countryItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(countryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CountryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryItem>> PostCountryItem(CountryItem countryItem)
        {
            _context.CountryItems.Add(countryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryItem", new { id = countryItem.Id }, countryItem);
        }

        // DELETE: api/CountryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryItem(long id)
        {
            var countryItem = await _context.CountryItems.FindAsync(id);
            if (countryItem == null)
            {
                return NotFound();
            }

            _context.CountryItems.Remove(countryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryItemExists(long id)
        {
            return _context.CountryItems.Any(e => e.Id == id);
        }
    }
}
