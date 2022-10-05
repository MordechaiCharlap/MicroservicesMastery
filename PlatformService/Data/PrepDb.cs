using PlatformService.Models;
using Microsoft.EntityFrameworkCore;
namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }
        private static void SeedData(AppDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("--> Attempting to apply migration...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migration {ex.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("-->Creating Mock data...");
                Platform p1 = new Platform() { Name = "Test 1", Publisher = "Microsoft", Cost = "A lot" };
                Platform p2 = new Platform() { Name = "Test 2", Publisher = "Java", Cost = "A lot" };
                Platform p3 = new Platform() { Name = "Test 3", Publisher = "Kubernitis", Cost = "A lot" };

                context.Platforms.AddRange(p1, p2, p3);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("-->We already have data");
            }

        }
    }
}