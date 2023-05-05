namespace FaceComparisonAPI.Services
{
    public interface IAnonymizationService
    {
        Task<double[]> AnonymizeAsync(IFormFile image);
    }
}
