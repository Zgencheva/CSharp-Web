﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitACity.Web.ViewModels.Reviews;

namespace VisitACity.Services.Data.Contracts
{
    public interface IReviewService
    {
        Task AddReviewToAttractionAsync(CreateReviewInputModel input, string userId, int attractionId);
    }
}