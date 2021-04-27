using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TowTruckUberAPI.Models;

namespace TowTruckUberAPI.Infrastructure.Database
{
    public class SeedData
    {


        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                AppDbContext context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                //context.Database.Migrate();
                if (context.Database.CanConnect())
                {
                    if (!context.MapGrids.Any())
                    {

                        context.MapGrids.AddRange(
                            new MapGrid()
                            {
                                Latitude = "38.8951",
                                Longitude = "-77.0364"
                            },
                            new MapGrid()
                            {
                                Latitude = "-11.5350",
                                Longitude = "99.8888"
                            },
                            new MapGrid()
                            {
                                Latitude = "20.2137",
                                Longitude = "-05.0404"
                            },
                            new MapGrid()
                            {
                                Latitude = "33.4051",
                                Longitude = "-58,9531"
                            });
                        context.SaveChanges();
                    }

                    if (!context.Addresses.Any())
                    {

                        context.Addresses.AddRange(new Address()
                        {
                            City = "Wrocław",
                            Country = "Polska",
                            HouseNum = "4",
                            Street = "grunwaldzka",
                            MapGrid = context.MapGrids.Find(1),
                            ZipCode = "27-100"
                        });
                        context.SaveChanges();
                    }

                    //if (!context.Users.Any())
                    //{

                    //    context.Users.AddRange(new User()
                    //    {
                    //        Name = "Zdzisław",
                    //        Surname = "Kręcina",
                    //        PhoneNumber = "403918329",
                    //        Email = "zdzislaw@example.com",
                    //        IsContractor = false,
                    //    },
                    //        new User()
                    //        {
                    //            Name = "Michał",
                    //            Surname = "Anioł",
                    //            PhoneNumber = "997998999",
                    //            Email = "michal@example.com",
                    //            IsContractor = true,
                    //        },
                    //        new User()
                    //        {
                    //            Name = "123",
                    //            Surname = "456",
                    //            PhoneNumber = "506508510",
                    //            Email = "123@example.com",
                    //            IsContractor = true,
                    //        });
                    //    context.SaveChanges();
                    //}

                    //if (!context.Trips.Any())
                    //{

                    //    context.Trips.AddRange(
                    //        new Trip()
                    //        {
                    //            Contractor = context.Users.Find(1),
                    //            Customer = context.Users.Find(2),
                    //            CreatedAt = DateTime.Now,
                    //            Distance = 40,
                    //            EndLocation = context.Addresses.Find(1),
                    //            EstimatedTime = DateTime.Now,
                    //            StartLocation = context.Addresses.Find(2),

                    //        });
                    //    context.SaveChanges();
                    //}

                    //if (!context.Payments.Any())
                    //{

                    //    context.Payments.AddRange(new Payment()
                    //    {
                    //        Description = "test",
                    //        Name = "payment test",
                    //        Payer = context.Users.Find(1),
                    //        StartDate = "111",
                    //        State = StateEnum.Pending,
                    //        Plan = 3,

                    //    });
                    //    context.SaveChanges();
                    //}
                }
            }
        }
    }
}
