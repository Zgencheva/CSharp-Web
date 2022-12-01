namespace VisitACity.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IImageService
    {
        Task<string> CreateAsync(string extension);

        //TODO:
        Task<string> UpdateAsync(string extension);
    }
}
