using FaceComparisonAPI.Models;

namespace FaceComparisonAPI.Services
{
    public interface IComparisonService
    {
        Task<ComparisonResult> CompareAsync(AnonymizedFace faceA, AnonymizedFace faceB, double threshold);
    }
}
