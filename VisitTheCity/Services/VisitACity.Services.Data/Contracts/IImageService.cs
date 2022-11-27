using System.Threading.Tasks;

namespace VisitACity.Services.Data.Contracts
{
    public interface IImageService
    {
        Task<string> CreateAsync(string extension);

    }
}
