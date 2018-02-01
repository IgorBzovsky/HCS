using HCS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HCS.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HcsDbContext context)
        {
            context.Database.EnsureCreated();
            SeedUtilities(context);
            SeedLocations(context);
        }

        private static void SeedLocations(HcsDbContext context)
        {
            if (!context.Locations.Any())
            {
                context.Locations.Add(new Location
                {
                    Name = "Вінницька область",
                    Children = new List<Location>
                    {
                        new Location
                        {
                            Name = "Вінницький район",
                            Children = new List<Location>
                            {
                                new Location
                                {
                                    Name = "Вінниця"
                                }
                            }
                        },
                        new Location
                        {
                            Name = "Жмеринський район",
                            Children = new List<Location>
                            {
                                new Location
                                {
                                    Name = "Жмеринка"
                                }
                            }
                        }
                    }
                });

                context.Locations.Add(new Location
                {
                    Name = "Одеська область",
                    Children = new List<Location>
                    {
                        new Location
                        {
                            Name = "Одеський район",
                            Children = new List<Location>
                            {
                                new Location
                                {
                                    Name = "Одеса"
                                }
                            }
                        },
                        new Location
                        {
                            Name = "Білгород-Дністровський район",
                            Children = new List<Location>
                            {
                                new Location
                                {
                                    Name = "Білгород-Дністровський"
                                }
                            }
                        }
                    }
                });
                context.SaveChanges();
            }
        }

        private static void SeedUtilities(HcsDbContext context)
        {
            if (!context.Utilities.Any())
            {
                MeasureUnit electricityUnit = new MeasureUnit { Name = "кВт" };
                MeasureUnit heatingUnit = new MeasureUnit { Name = "Гкал" };
                MeasureUnit cubicMeter = new MeasureUnit { Name = "м\u00B3" };

                context.Utilities.Add(new Utility
                {
                    Name = "Електроенергія",
                    MeasureUnit = electricityUnit
                });
                context.Utilities.Add(new Utility
                {
                    Name = "Газ",
                    MeasureUnit = cubicMeter
                });
                context.Utilities.Add(new Utility
                {
                    Name = "Тепло",
                    MeasureUnit = heatingUnit
                });
                context.Utilities.Add(new Utility
                {
                    Name = "Гаряча вода",
                    MeasureUnit = cubicMeter
                });
                context.Utilities.Add(new Utility
                {
                    Name = "Холодна вода",
                    MeasureUnit = cubicMeter
                });
                context.Utilities.Add(new Utility
                {
                    Name = "Водовідведення",
                    MeasureUnit = cubicMeter
                });
                context.SaveChanges();
            }
        }
    }
}
