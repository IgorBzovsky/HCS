using HCS.Core.Domain;
using System.Collections.Generic;
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
                                    Name = "Вінниця",
                                    Children = new List<Location>
                                    {
                                        new Location
                                        {
                                            Name = "Шевченка"
                                        },
                                        new Location
                                        {
                                            Name = "Л.Ратушної"
                                        },
                                        new Location
                                        {
                                            Name = "Київська"
                                        },
                                        new Location
                                        {
                                            Name = "600-річчя"
                                        }
                                    }
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
                                    Name = "Жмеринка",
                                    Children = new List<Location>
                                    {
                                        new Location
                                        {
                                            Name = "Крилова"
                                        },
                                        new Location
                                        {
                                            Name = "Залізнична"
                                        }
                                    }
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
                                    Name = "Одеса",
                                    Children = new List<Location>
                                    {
                                        new Location
                                        {
                                            Name = "Пушкінська"
                                        },
                                        new Location
                                        {
                                            Name = "Маловського"
                                        },
                                        new Location
                                        {
                                            Name = "Балківська"
                                        }
                                    }
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
                                    Name = "Білгород-Дністровський",
                                    Children = new List<Location>
                                    {
                                        new Location
                                        {
                                            Name = "Франко"
                                        },
                                        new Location
                                        {
                                            Name = "Київська"
                                        }
                                    }
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
