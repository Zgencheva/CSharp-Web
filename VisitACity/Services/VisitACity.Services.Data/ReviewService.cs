namespace VisitACity.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Reviews;

    public class ReviewService : IReviewService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Attraction> attractionRespository;
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public ReviewService(
            IDeletableEntityRepository<Review> reviewsRepository,
            IDeletableEntityRepository<Attraction> attractionRespository,
            IDeletableEntityRepository<Restaurant> restaurantRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.reviewsRepository = reviewsRepository;
            this.attractionRespository = attractionRespository;
            this.restaurantRepository = restaurantRepository;
            this.userRepository = userRepository;
        }

        public Task AddReviewToAttractionAsync(CreateReviewInputModel input, string userId, int attractionId)
        {
            throw new NotImplementedException();
        }
    }
}
