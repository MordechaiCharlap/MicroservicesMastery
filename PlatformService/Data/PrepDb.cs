using PlatformService.Models;
namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }
        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("-->Creating Mock data...");

                Platform p1 = new Platform() { Id = 1, Name = "Test 1", Publisher = "Microsoft", Cost = "A lot" };
                Platform p2 = new Platform() { Id = 2, Name = "Test 2", Publisher = "Java", Cost = "A lot" };
                Platform p3 = new Platform() { Id = 3, Name = "Test 3", Publisher = "Kubernitis", Cost = "A lot" };

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