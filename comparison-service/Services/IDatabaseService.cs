using FaceComparisonAPI.Models;

namespace FaceComparisonAPI.Services
{
    public interface IDatabaseService
    {
        Task<AnonymizedFace> AddAnonymizedFaceAsync(AnonymizedFace anonymizedFace);
        Task<AnonymizedFace> GetAnonymizedFaceByIdAsync(Guid identifier);
    }
}
