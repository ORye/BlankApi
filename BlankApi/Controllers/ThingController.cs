using BlankApi.Data;
using BlankApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlankApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ThingController : ControllerBase
    {
        private readonly ThingRepository _thingRepository;

        public ThingController(DbContextOptions<BlankApiContext> dbContextoptions)
        {
            _thingRepository = new ThingRepository(dbContextoptions);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Thing>> Index()
        {
            return _thingRepository.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Thing> GetThing(int id)
        {
            return _thingRepository.Get(id);
        }

        [HttpPost]
        public ActionResult<Thing> AddThing(Thing thing)
        {
            Thing addedThing = _thingRepository.Insert(thing);
            return addedThing;
        }

        [HttpPut]
        public ActionResult<Thing> EditThing(Thing thing)
        {
            try
            {
                Thing editedThing = _thingRepository.Update(thing);
                return editedThing;
            }
            catch (ObjectNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteThing(int id)
        {
            try
            {
                _thingRepository.Delete(id);
            }
            catch (ObjectNotFoundException)
            {
                return new NotFoundResult();
            }

            return new OkResult();
        }
    }
}