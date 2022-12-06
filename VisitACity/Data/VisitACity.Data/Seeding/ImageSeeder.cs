namespace VisitACity.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VisitACity.Data.Models;

    internal class ImageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Images.Any())
            {
                return;
            }

            await dbContext.Images.AddAsync(new Image { Id = "02c5467a-4c9f-4708-86c7-20ca782d8d92", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "0a4c0be2-e549-49e8-9d4e-d9881080009f", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "0b38c0d5-5a00-4aff-80dc-cfbb692e9db1", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "58686ce8-3ebb-4aa3-8480-65900630643f", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "626a4614-f668-48ea-b646-1fe70f1b325d", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "62cde487-6b82-468f-bdbf-fcfe2d7c779b", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "65bb5bae-3402-48e8-870a-2524efff998c", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "8c2658a9-bae8-4943-ae08-01755ad64129", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "9141ebf6-40ad-46f4-8a83-b928a6ec36cc", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "a0f8a448-9e9a-475c-a303-7969f60dee6d", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "d3033433-143d-4bff-b465-fa68aaf0af35", Extension = "jpg" });
            await dbContext.Images.AddAsync(new Image { Id = "f4656919-2330-4488-9f8d-2b5f15665010", Extension = "jpg" });

            await dbContext.SaveChangesAsync();
        }
    }
}
