using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SuperHeroController : ControllerBase

    {
        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeros()
        {
            var heros = await _dataContext.SuperHeros.ToListAsync();
            return Ok(heros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetAllHero(int id)
        {
            var heros = await _dataContext.SuperHeros.FindAsync(id);

            if (heros == null) { return NotFound(); }
            return Ok(heros);
        }

        [HttpPost]

        public async Task<ActionResult<List<SuperHero>>> CreateHero(SuperHero superHero)
        {
            _dataContext.SuperHeros.Add(superHero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeros.ToListAsync());

        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {
            var dbHero = await _dataContext.SuperHeros.FindAsync(updatedHero.Id);
            if (dbHero is null) { return BadRequest(); }

            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Place = updatedHero.Place;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }


        [HttpDelete]

        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var toBeDeleted = await _dataContext.SuperHeros.FindAsync(id);


            if (toBeDeleted == null) { return BadRequest(); }

            _dataContext.SuperHeros.Remove(toBeDeleted);

            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeros.ToListAsync());

        }
    }
}
