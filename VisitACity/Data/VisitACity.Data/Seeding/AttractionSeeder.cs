﻿namespace VisitACity.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Sait Sofia",
                Type = (AttractionType)30,
                AttractionUrl = "https://stolica.bg/sofia-tur/hramove/tsarkva-sveta-sofiya",
                Price = 0,
                Address = "2 Paris",
                City = citySofia,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/51/Basilica_of_Hagia_Sofia%2C_Bulgaria.jpg",
                Description = "The church was built on the site of several earlier churches from the 4th century, and places of worship dating back to the days when it was the necropolis of the Roman town of Serdica. In the 2nd century, it was the location of a Roman theatre. Over the next few centuries, several other churches were constructed, only to be destroyed by invading forces such as the Goths and the Huns. The basic cross design of the present basilica, with its two east towers and one tower-cupola, is believed to be the fifth structure to be constructed on the site and was built during the reign of Byzantine Emperor Justinian I in the middle of the 6th century (527–565). It is thus a contemporary of the better-known Hagia Sophia church in Constantinople.[1]",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Museum of illusions",
                Type = (AttractionType)30,
                AttractionUrl = "https://museumofillusions.bg/",
                Price = 20,
                Address = "16 Maria Luiza",
                City = citySofia,
                ImageUrl = "https://museumofillusions.bg/uploads/2022/03/04.jpg",
                Description = "It's fascinating. It's fun. It's sure to be surprising. 400 m2 full of riveting illusions, wonderful atmosphere and helpful staff.",
            });

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Sofia Zoo",
                Type = (AttractionType)10,
                AttractionUrl = "http://zoosofia.eu/",
                Price = 15,
                Address = "37 Chavdar Mutafov",
                City = citySofia,
                ImageUrl = "http://zoosofia.eu/wp-content/uploads/2020/05/70470268_418593165457994_2397962378311368704_n.jpg",
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
                ImageUrl = "https://freesofiatour.com/wp-content/uploads/2018/05/seven-rila-lakes-how-to-get-to-1200x675.jpeg",
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
                ImageUrl = "https://static.bnr.bg/gallery/cr/medium/c664e2ac09d19888c4690dc31d0ff917.jpg",
                Description = "The Boyana Church is a medieval Bulgarian Orthodox church situated on the outskirts of Sofia, the capital of Bulgaria, in the Boyana quarter. In 1979, the building was added to the UNESCO World Heritage List.",
            });

            var cityPlovdiv = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Plovdiv");

            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Kapana",
                Type = (AttractionType)10,
                AttractionUrl = "https://visitkapana.bg/",
                Price = 10,
                Address = "In the center of Plovdiv",
                City = cityPlovdiv,
                ImageUrl = "https://freeplovdivtour.com/wp-content/uploads/2018/02/kapana-tour.jpg",
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
                ImageUrl = "http://www.romanplovdiv.org/wp-content/uploads/2020/01/rimski-stadion-den-1024x576.jpg",
                Description = "Rome old stadium",
            });

            var cityAsenovgrad = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Asenovgrad");
            await dbContext.Attractions.AddAsync(new Attraction
            {
                Name = "Paleontological Museum",
                Type = (AttractionType)40,
                AttractionUrl = "https://bulgariatravel.org/paleontological-museum-town-of-asenovgrad/",
                Price = 10,
                Address = "35 Slivnica",
                City = cityAsenovgrad,
                ImageUrl = "https://www.nmnhs.com/images/main/exhibitions/asenovgrad/deinotherium.jpg",
                Description = "Rome old stadium",
            });
        }
    }
}