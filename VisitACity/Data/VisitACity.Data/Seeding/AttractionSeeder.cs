namespace VisitACity.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VisitACity.Data.Models;
    using VisitACity.Data.Models.Enums;

    public class AttractionSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Attractions.Any())
            {
                return;
            }

            var citySofia = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Sofia");
            var cityVarna = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Varna");
            var cityPlovdiv = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Plovdiv");
            var cityAsenovgrad = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Asenovgrad");

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Muzeiko",
                Type = (AttractionType)30,
                AttractionUrl = "https://www.muzeiko.bg/bg",
                Price = 10,
                Address = "3 Professor Boyan Kamenov, 1756 Studentski",
                City = citySofia,
                Image = new Image { Id = "02c5467a-4c9f-4708-86c7-20ca782d8d92", Extension = ".jpg" },
                Description = "Children's museum featuring interactive science-related exhibits, a cafe & a gift shop.",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Nacional History Museum",
                Type = (AttractionType)30,
                AttractionUrl = "https://historymuseum.org/",
                Price = 10,
                Address = "16 Vitoshko lale",
                City = citySofia,
                Image = new Image { Id = "0a4c0be2-e549-49e8-9d4e-d9881080009f", Extension = ".jpg" },
                Description = "The National Historical Museum in Sofia is Bulgaria's largest museum. It was founded on 5 May 1973. A new representative exhibition was opened in the building of the Court of Justice on 2 March 1984, to commemorate the 13th centenary of the Bulgarian state",
            });
            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Sait Sofia",
                Type = (AttractionType)30,
                AttractionUrl = "https://stolica.bg/sofia-tur/hramove/tsarkva-sveta-sofiya",
                Price = 0,
                Address = "2 Paris",
                City = citySofia,
                Image = new Image { Id = "0b38c0d5-5a00-4aff-80dc-cfbb692e9db1", Extension = ".jpg" },
                Description = "The church was built on the site of several earlier churches from the 4th century, and places of worship dating back to the days when it was the necropolis of the Roman town of Serdica.",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Boyana Waterfall",
                Type = (AttractionType)0,
                AttractionUrl = "",
                Price = 0,
                Address = "In the woods, near Sofia",
                City = citySofia,
                Image = new Image { Id = "58686ce8-3ebb-4aa3-8480-65900630643f", Extension = ".jpg" },
                Description = "Boyana Waterfall is the largest waterfall in the Bulgarian Vitosha mountain, with a height of 25 meters.",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Museum of illusions",
                Type = (AttractionType)30,
                AttractionUrl = "https://museumofillusions.bg/",
                Price = 20,
                Address = "16 Maria Luiza",
                City = citySofia,
                Image = new Image { Id = "626a4614-f668-48ea-b646-1fe70f1b325d", Extension = ".jpg" },
                Description = "It's fascinating. It's fun. It's sure to be surprising. 400 m2 full of riveting illusions, wonderful atmosphere and helpful staff.",
            });
            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Dolphinarium",
                Type = (AttractionType)0,
                AttractionUrl = "https://dolphinariumvarna.bg/",
                Price = 25,
                Address = "Saltanat primorski park",
                City = cityVarna,
                Image = new Image { Id = "62cde487-6b82-468f-bdbf-fcfe2d7c779b", Extension = ".jpg" },
                Description = "Yes, you can swim with dolphins. Dolphins can have fun.",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Sofia Zoo",
                Type = (AttractionType)10,
                AttractionUrl = "http://zoosofia.eu/",
                Price = 15,
                Address = "37 Chavdar Mutafov",
                City = citySofia,
                Image = new Image { Id = "65bb5bae-3402-48e8-870a-2524efff998c", Extension = ".jpg" },
                Description = "Sofia Zoo in Sofia, the capital of Bulgaria, was founded by royal decree on 1 May 1888, and is the oldest and largest zoological garden in southeastern Europe. It covers 36 hectares and, in March 2006, housed 4,850 animals representing 840 species.",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Rila Manastry and 7 Rila lakes",
                Type = (AttractionType)10,
                AttractionUrl = "https://rilskiezera.bg/",
                Price = 60,
                Address = "In the mountain Rila",
                City = citySofia,
                Image = new Image { Id = "8c2658a9-bae8-4943-ae08-01755ad64129", Extension = ".jpg" },
                Description = "The Seven Rila Lakes are a group of glacial lakes, situated in the northwestern Rila Mountain in Bulgaria. They are the most visited group of lakes in Bulgaria. The lakes are situated between 2,100 and 2,500 metres elevation above sea level. Each lake carries a name associated with its most characteristic feature.,",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Boyana church",
                Type = (AttractionType)40,
                AttractionUrl = "http://www.boyanachurch.org/",
                Price = 10,
                Address = "3 Boyansko ezero",
                City = citySofia,
                Image = new Image { Id = "9141ebf6-40ad-46f4-8a83-b928a6ec36cc", Extension = ".jpg" },
                Description = "The Boyana Church is a medieval Bulgarian Orthodox church situated on the outskirts of Sofia, the capital of Bulgaria, in the Boyana quarter. In 1979, the building was added to the UNESCO World Heritage List.",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Kapana",
                Type = (AttractionType)10,
                AttractionUrl = "https://visitkapana.bg/",
                Price = 10,
                Address = "In the center of Plovdiv",
                City = cityPlovdiv,
                Image = new Image { Id = "a0f8a448-9e9a-475c-a303-7969f60dee6d", Extension = ".jpg" },
                Description = "As Bulgaria’s first dedicated creative district, Kapana is a charming arts and cultural destination where cobblestone streets give way to restored buildings with art galleries and specialty shops. Patrons flock to sidewalk patios at an array of bakeries, diverse eateries and gastropubs that serve everything from classic regional eats to French cuisine. Kapana Fest showcases performers and artists each August.",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Rome stadium",
                Type = (AttractionType)40,
                AttractionUrl = "",
                Price = 0,
                Address = "Knqz Aleksandyr 1",
                City = cityPlovdiv,
                Image = new Image { Id = "d3033433-143d-4bff-b465-fa68aaf0af35", Extension = ".jpg" },
                Description = "Rome old stadium",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Paleontological Museum",
                Type = (AttractionType)40,
                AttractionUrl = "https://bulgariatravel.org/paleontological-museum-town-of-asenovgrad/",
                Price = 10,
                Address = "35 Slivnica",
                City = cityAsenovgrad,
                Image = new Image { Id = "f4656919-2330-4488-9f8d-2b5f15665010", Extension = ".jpg" },
                Description = "Rome old stadium",
            });
        }
    }
}
