using BlankApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlankApi.Repositories
{
    public class ThingRepository
    {
        private readonly DbContextOptions<BlankApiContext> _options;

        public ThingRepository(DbContextOptions<BlankApiContext> options)
        {
            _options = options;
        }

        public List<Thing> Get()
        {
            using (var db = new BlankApiContext(_options))
            {
                return db.Thing.ToList();
            }
        }

        public Thing Get(int id)
        {
            using (var db = new BlankApiContext(_options))
            {
                return db.Thing.FirstOrDefault(x => x.Id == id);
            }
        }

        public Thing Insert(Thing thing)
        {
            if (thing == null)
            {
                throw new ArgumentNullException(nameof(thing), "Cannot supply null to Insert.");
            }

            using (var db = new BlankApiContext(_options))
            {
                db.Thing.Add(thing);
                db.SaveChanges();

                return thing;
            }
        }

        public Thing Update(Thing thing)
        {
            if (thing == null)
            {
                throw new ArgumentNullException(nameof(thing), "Cannot supply null to Update.");
            }

            using (var db = new BlankApiContext(_options))
            {
                Thing thingToModify = db.Thing.Find(thing.Id);
                if (thingToModify == null)
                {
                    throw new InvalidOperationException($"Update failed. No Thing exists by id: {thing.Id}.");
                }

                // TODO: move updating to function to do mapping
                thingToModify.Name = thing.Name;
                thingToModify.AMaBobber = thing.AMaBobber;
                db.SaveChanges();

                return thingToModify;
            }
        }

        public void Delete(int id)
        {
            using (var db = new BlankApiContext(_options))
            {
                Thing thingToDelete = db.Thing.Find(id);
                if (thingToDelete == null)
                {
                    throw new InvalidOperationException($"Delete failed. No Thing exists by id: {id}.");
                }

                db.Thing.Remove(thingToDelete);
                db.SaveChanges();
            }
        }
    }
}
