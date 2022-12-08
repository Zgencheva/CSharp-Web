namespace VisitACity.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        Task<string> CreateAsync(IFormFile imageModel);

        Task<string> UpdateAsync(IFormFile imageModel, string imageId);
    }
}
