namespace VisitACity.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VisitACity.Common;
    using VisitACity.Data.Common.Repositories;
    using VisitACity.Data.Models;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Reviews;

    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public ReviewsService(
            IDeletableEntityRepository<Review> reviewsRepository,
            IDeletableEntityRepository<Restaurant> restaurantRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.reviewsRepository = reviewsRepository;
            this.restaurantRepository = restaurantRepository;
            this.userRepository = userRepository;
        }

        public async Task AddReviewToRestaurantAsync(CreateReviewInputModel input, string userId, int id)
        {
            var restarant = await this.restaurantRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (restarant == null)
            {
                throw new NullReferenceException(ExceptionMessages.Restaurant.InvalidRestaurant);
            }

            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NullReferenceException(ExceptionMessages.NotExistingUser);
            }

            var review = new Review
            {
                UserId = userId,
                Content = input.Content,
                Rating = input.Rating,
                Restaurant = restarant,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await this.reviewsRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (review == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidComment);
            }

            review.IsDeleted = true;
            this.reviewsRepository.Update(review);
            await this.reviewsRepository.SaveChangesAsync();
        }
    }
}
