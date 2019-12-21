using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using overapp.janus.Infrastructure;
using overapp.janus.Models.Domain;

namespace overapp.janus
{
    /// <summary>
    /// Class to auto generate data in inMemory DB. NOT FOR PROD
    /// </summary>
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new JanusContext(serviceProvider.GetRequiredService<DbContextOptions<JanusContext>>()))
            {
                if (context.Merchants.Any())
                {
                    // Data is already present
                    return;
                }

                // Add fake merchants
                context.Merchants.Add(new Merchant
                {
                    ClientId = "123456",
                    ClientSecret = "psss-it-s-a-secret",
                    Id = 1,
                    Name = "Merchant1"
                });


                context.Merchants.Add(new Merchant
                {
                    ClientId = "1",
                    ClientSecret = "psss-it-s-a-secret2",
                    Id = 2,
                    Name = "Merchant2"
                });

                context.SaveChanges();
            }
        }
    }
}