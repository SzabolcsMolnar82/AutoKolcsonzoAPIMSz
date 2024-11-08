using Microsoft.AspNetCore.Mvc;
using AutoKolcsonzoAPIMSz.Context;
using AutoKolcsonzoAPIMSz.Entites;
using Microsoft.EntityFrameworkCore;

namespace AutoKolcsonzoAPIMSz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly VehicleRentalContext _context;

        public RentalsController(VehicleRentalContext context)
        {
            _context = context;
        }

        //GET lekéri az összes kölcsönzést az adatbázisból.
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            return await _context.Rentals.ToListAsync();
        }

        //GET(ID) lekéri az adott azonosítóval rendelkező kölcsönzést az adatbázisból.
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }
            return rental;
        }

        //POST új kölcsönzést hoz létre az adatbázisban.
        [HttpPost]
        public async Task<ActionResult<Rental>> PostRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRental), new { id = rental.Id }, rental);
        }


        //PUT Frissíti a meglévő kölcsönzés adatait az adatbázisban.
        [HttpPut]
        public async Task<IActionResult> PutRental(int id, Rental rental)
        {
            if (id != rental.Id)
            {
                return BadRequest();
            }

            _context.Entry(rental).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //DELETE Törli az adott azonosítóval rendelkező kölcsönzést az adatbázisból.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
