﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VisitACity.Data.Models;
using VisitACity.Web.ViewModels.Attractions;

namespace VisitACity.Services.Data.Contracts
{
    public interface IAttractionsService
    {
        int GetCount();

        IEnumerable<AttractionViewModel> GetBestAttractions(int page, int itemsPerPage);

        IEnumerable<AttractionViewModel> GetAttractionsByCity(string cityName, int page, int itemsPerPage);

        Task<AttractionViewModel> GetAttractionById(int id);
    }
}
