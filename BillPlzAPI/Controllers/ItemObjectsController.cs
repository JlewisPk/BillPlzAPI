using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BillPlzAPI.Models;

namespace BillPlzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemObjectsController : ControllerBase
    {
        private readonly BillPlzAPIContext _context;

        public ItemObjectsController(BillPlzAPIContext context)
        {
            _context = context;
        }

        // GET: api/ItemObjects
        [HttpGet]
        public IEnumerable<ItemObject> GetItemObject()
        {
            return _context.ItemObject;
        }

        // GET: api/ItemObjects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemObject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemObject = await _context.ItemObject.FindAsync(id);

            if (itemObject == null)
            {
                return NotFound();
            }

            return Ok(itemObject);
        }

        // PUT: api/ItemObjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemObject([FromRoute] int id, [FromBody] ItemObject itemObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemObject.itemId)
            {
                return BadRequest();
            }

            _context.Entry(itemObject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemObjectExists(id))
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

        // POST: api/ItemObjects
        [HttpPost]
        public async Task<IActionResult> PostItemObject([FromBody] ItemObject itemObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ItemObject.Add(itemObject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemObject", new { id = itemObject.itemId }, itemObject);
        }

        // DELETE: api/ItemObjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemObject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemObject = await _context.ItemObject.FindAsync(id);
            if (itemObject == null)
            {
                return NotFound();
            }

            _context.ItemObject.Remove(itemObject);
            await _context.SaveChangesAsync();

            return Ok(itemObject);
        }

        private bool ItemObjectExists(int id)
        {
            return _context.ItemObject.Any(e => e.itemId == id);
        }

        // additional controller here

        // GET: api/ItemObjects/ItemNameList
        [Route("itemNameList")]
        [HttpGet]
        public async Task<List<string>> GetMatchingNames()
        {
            var items = (from m in _context.ItemObject
                         select m.itemName).Distinct();

            var returned = await items.ToListAsync();

            return returned;
        }

    }
}