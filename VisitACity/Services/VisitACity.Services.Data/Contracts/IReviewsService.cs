﻿namespace VisitACity.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using VisitACity.Web.ViewModels.Reviews;

    public interface IReviewsService
    {
        Task DeleteAsync(int id);

        Task AddReviewToRestaurantAsync(CreateReviewInputModel input, string userId, int id);
    }
}
