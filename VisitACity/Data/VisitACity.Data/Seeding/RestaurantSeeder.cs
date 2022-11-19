namespace VisitACity.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VisitACity.Data.Models;

    public class RestaurantSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Restaurants.Any())
            {
                return;
            }

            var citySofia = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Sofia");
            var cityVarna = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Varna");
            var cityPlovdiv = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Plovdiv");
            var cityAsenovgrad = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Asenovgrad");
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "ChasovnikA",
                Url = "https://chasovnikavarna.com/",
                Address = "4 Knjaz N. Nikolaevich",
                City = cityVarna,
                ImageUrl = "https://zavedenia.com/zimages/varna/big/1831/1831lTCxEeMFBeksgU8XJbyhdYnWr22FvK7RDJp.jpg",
                PhoneNumber = "+35979541589",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Staria chinar",
                Url = "http://www.stariachinar.com/cherno-more/",
                Address = "Port Varna",
                City = cityVarna,
                ImageUrl = "https://static.pochivka.bg/restaurants.bgstay.com/images/restaurants/01/1279/320x230/5617902937b23.JPG",
                PhoneNumber = "+35979549494",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Hebros",
                Url = "https://hebros-hotel.com/restorant/",
                Address = "51 Konstantin Stoilov",
                City = cityPlovdiv,
                ImageUrl = "https://image9003.dineout.bg/isxKn_YSeTTq1vbQpQ2GRd-m6p4=/800x/places/f4e9e6b2b90317c6bcf870333323b2e0/thumb_3624600e2c556972043123f939140ba0.jpeg",
                PhoneNumber = "+359886511459",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Hebros",
                Url = "https://hebros-hotel.com/restorant/",
                Address = "51 Konstantin Stoilov",
                City = cityPlovdiv,
                ImageUrl = "https://image9003.dineout.bg/isxKn_YSeTTq1vbQpQ2GRd-m6p4=/800x/places/f4e9e6b2b90317c6bcf870333323b2e0/thumb_3624600e2c556972043123f939140ba0.jpeg",
                PhoneNumber = "+359886511459",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Raffy",
                Url = "https://raffyplovdiv.bg/",
                Address = "53 Saedinenie",
                City = cityPlovdiv,
                ImageUrl = "https://media-cdn.tripadvisor.com/media/photo-s/14/19/87/fd/5-patriarch-evtimii-str.jpg",
                PhoneNumber = "+359885855166",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Jagerhof",
                Url = "https://jagerhof.bg/hausbrauerei/",
                Address = "4 Saedinenie",
                City = cityPlovdiv,
                ImageUrl = "https://jagerhof.bg/wp-content/uploads/2020/06/Untitled-1-2.png",
                PhoneNumber = "+359885206666",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Shtastliveca",
                Url = "https://www.shtastliveca.com/en/restaurants/shtastliveca-vitosha-sofia.html",
                Address = "22 San Stefano",
                City = citySofia,
                ImageUrl = "http://www.shtastliveca.com/img/tour/shtastliveca-vitosha-sofia-1.jpg",
                PhoneNumber = "+35988254333",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "101",
                Url = "https://101bardinner.com/bg",
                Address = "1 Nikola Gabrovski",
                City = citySofia,
                ImageUrl = "https://101bardinner.com/uploads/slider/_D7A0701.jpg",
                PhoneNumber = "+35988444333",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Neighbors",
                Url = "https://neighbors.bg/",
                Address = "Slav Karaslavov 10",
                City = citySofia,
                ImageUrl = "https://lh3.googleusercontent.com/p/AF1QipNQlXQj1XdN9pVMvodeNKLkQ2F5mDGAfi2nAT_F=s680-w680-h510",
                PhoneNumber = "+359899995534",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Mr. Pizza",
                Url = "https://www.mrpizza.bg/bg",
                Address = "6 Nikola Vapcarov",
                City = citySofia,
                ImageUrl = "https://www.mrpizza.bg/img/kids-gallery/DSC03112.jpg",
                PhoneNumber = "+359888297457",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Nacional",
                Url = "https://www.restorant-nacional-friends-kitchen.bg/",
                Address = "3 Professor Boyan Kamenov, 1756 Studentski",
                City = citySofia,
                ImageUrl = "https://www.restorant-nacional-friends-kitchen.bg/image/icon.png",
                PhoneNumber = "+359898556623",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Pizzerito",
                Url = "https://www.pizzerito.com/",
                Address = "10 Vasil Kalchev",
                City = citySofia,
                ImageUrl = "https://lh5.googleusercontent.com/p/AF1QipPsUDetdAVrwOwHANjLa3UzJURGuoxGiYDBDQna=w408-h306-k-no",
                PhoneNumber = "+359878657667",
            });
            await dbContext.Restaurants.AddAsync(new Restaurant
            {
                Name = "Victoria",
                Url = "https://www.victoria.bg/",
                Address = "75 Cherkovna",
                City = citySofia,
                ImageUrl = "https://www.victoria.bg/files/images/restaurants/restaurants/gallery/IMG_1260_1657548619_7_1657619882_10.jpg",
                PhoneNumber = "+359887911000",
            });
        }
    }
}
