namespace VisitACity.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Azure.Storage.Blobs;
    using Microsoft.AspNetCore.Mvc;
    using VisitACity.Common;
    using VisitACity.Services.Data.Contracts;
    using VisitACity.Web.ViewModels.Administration.Attractions;
    using VisitACity.Web.ViewModels.Cities;

    public class AttractionsController : AdministrationController
    {
        private readonly IAttractionsService attractionsService;
        private readonly ICitiesService citiesService;
        private readonly IImageService imageService;
        private readonly BlobServiceClient blobService;

        public AttractionsController(
            IAttractionsService attractionsService,
            ICitiesService citiesService,
            IImageService imageService,
            BlobServiceClient blobService)
        {
            this.attractionsService = attractionsService;
            this.citiesService = citiesService;
            this.imageService = imageService;
            this.blobService = blobService;
        }

        public async Task<IActionResult> Create()
        {
            var model = new AttractionFormModel();
            model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AttractionFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            string imageExtension = model.ImageToBlob.ContentType.Split('/')[1];
            if (!this.IsExtensionValid(imageExtension))
            {
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            try
            {
                var imageId = await this.imageService.CreateAsync(imageExtension);
                await this.attractionsService.CreateAsync(model, imageId, imageExtension);
            }
            catch (Exception ex)
            {

                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            this.TempData["Message"] = TempDataMessageConstants.Attraction.AttractionAdded;
            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var modelToEdit = await this.attractionsService.GetViewModelByIdAsync<AttractionFormModel>(id);
            modelToEdit.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
            return this.View(modelToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, AttractionFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            try
            {
                await this.attractionsService.UpdateAsync(id, model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Cities = await this.citiesService.GetAllAsync<CityViewModel>();
                return this.View(model);
            }

            this.TempData["Message"] = TempDataMessageConstants.Attraction.AttractionUpdated;
            return this.RedirectToAction("Details", "Attractions", new { area = string.Empty, id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.attractionsService.DeleteByIdAsync(id);

            this.TempData["Message"] = TempDataMessageConstants.Attraction.AttractionDeleted;
            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }
    }
}
