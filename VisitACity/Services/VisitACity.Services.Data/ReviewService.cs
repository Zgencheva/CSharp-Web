using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitACity.Data.Common.Repositories;
using VisitACity.Data.Models;
using VisitACity.Services.Data.Contracts;
using VisitACity.Web.ViewModels.Reviews;

namespace VisitACity.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Attraction> attractionRespository;
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;

        public ReviewService(
            IDeletableEntityRepository<Review> reviewsRepository,
            IDeletableEntityRepository<Attraction> attractionRespository,
            IDeletableEntityRepository<Restaurant> restaurantRepository)
        {
            this.reviewsRepository = reviewsRepository;
            this.attractionRespository = attractionRespository;
            this.restaurantRepository = restaurantRepository;
        }

        public Task AddReviewToAttractionAsync(ReviewServiceModel inpu)
        {
            throw new NotImplementedException();
        }

        public double GetAttractionAverageRaiting(int id)
        {
            throw new NotImplementedException();
        }
    }
}
