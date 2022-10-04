using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitACity.Data.Models;

namespace VisitACity.Data.Seeding
{
    internal class ImageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Images.Any())
            {
                return;
            }

            await dbContext.Images.AddAsync(new Image { Extension = "jpg", AttractionId = 1 });
            await dbContext.Images.AddAsync(new Image { Extension = "jpg", AttractionId = 2 });
            await dbContext.Images.AddAsync(new Image { Extension = "jpg", AttractionId = 3 });
            await dbContext.Images.AddAsync(new Image { Extension = "jpg", AttractionId = 4 });
            await dbContext.Images.AddAsync(new Image { Extension = "jpg", AttractionId = 5 });
            await dbContext.Images.AddAsync(new Image { Extension = "jpg", AttractionId = 6 });
            await dbContext.Images.AddAsync(new Image { Extension = "jpg", AttractionId = 7 });
            await dbContext.Images.AddAsync(new Image { Extension = "jpg", AttractionId = 8 });
        }
    }
}
