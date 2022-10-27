namespace VisitACity.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public async Task AddReviewToAttractionAsync(CreateReviewInputModel input, string userId, int attractionId)
        {
            var attraction = await this.attractionRespository.All().FirstOrDefaultAsync(x => x.Id == attractionId);
            if (attraction == null)
            {
                throw new Exception("Invalid attraction");
            }

            var review = new Review
            {
                UserId = userId,
                Content = input.Content,
                Rating = input.Rating,
                Attraction = attraction,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await this.reviewsRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (review == null)
            {
                throw new NullReferenceException("Invalid comment");
            }
            review.IsDeleted = true;
            this.reviewsRepository.Update(review);
            await this.reviewsRepository.SaveChangesAsync();
        }
    }
}
