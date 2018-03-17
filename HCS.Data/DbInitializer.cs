using HCS.Core.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HCS.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HcsDbContext context)
        {
            context.Database.EnsureCreated();
            SeedUtilities(context);
            SeedLocations(context);
            SeedExemptions(context);
            SeedConsumerCategories(context);
        }

        private static void SeedLocations(HcsDbContext context)
        {
            if (!context.Locations.Any())
            {
                List<Location> locations = JsonConvert.DeserializeObject<List<Location>>(File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "locations.json"));
                context.Locations.AddRange(locations);
                context.SaveChanges();
            }
        }

        private static void SeedExemptions(HcsDbContext context)
        {
            if (!context.Exemptions.Any())
            {
                List<Exemption> exemptions = JsonConvert.DeserializeObject<List<Exemption>>(File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "exemptions.json"));
                context.Exemptions.AddRange(exemptions);
                context.SaveChanges();
            }
        }

        private static void SeedUtilities(HcsDbContext context)
        {
            if (!context.Utilities.Any())
            {
                List<Utility> utilities = JsonConvert.DeserializeObject<List<Utility>>(File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "utilities.json"));
                context.Utilities.AddRange(utilities);
                context.SaveChanges();
            }
        }

        private static void SeedConsumerCategories(HcsDbContext context)
        {
            if (!context.ConsumerTypes.Any())
            {
                List<ConsumerType> consumerTypes = JsonConvert.DeserializeObject<List<ConsumerType>>(File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "consumer_categories.json"));
                context.ConsumerTypes.AddRange(consumerTypes);
                context.SaveChanges();
            }
        }

        private static void SeedOrganizationCategories(HcsDbContext context)
        {

        }
    }
}
