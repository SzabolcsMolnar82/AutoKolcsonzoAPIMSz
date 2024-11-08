using Microsoft.AspNetCore.Mvc;
using AutoKolcsonzoAPIMSz.Context;
using AutoKolcsonzoAPIMSz.Entites;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace AutoKolcsonzoAPIMSz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleRentalContext _context;
        public VehiclesController(VehicleRentalContext context)
        {
            _context = context;
        }

        //GET Lekéri az összes járművet az adatbázisból.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        //GET Lekéri az adott azonosítóval rendelkező járművat az adatbázisból.
        [HttpGet("{id}")]

        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return vehicle;
        }
        //POST Új járművet hoz létre az adatbázisban.
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }

        //PUT Frissíti a meglévő jármű adatait az adatbázisban.
        [HttpPut]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE Törli az adott azonosítóval rendelkező járművet az adatbázisból.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicles(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
    
}
