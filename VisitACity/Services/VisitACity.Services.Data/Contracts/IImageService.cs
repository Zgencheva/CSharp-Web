namespace VisitACity.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IImagesService
    {
        Task<string> CreateAsync(string extension);
    }
}
