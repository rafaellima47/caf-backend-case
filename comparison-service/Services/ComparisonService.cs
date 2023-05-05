using FaceComparisonAPI.Models;

namespace FaceComparisonAPI.Services
{
    public class ComparisonService : IComparisonService
    {
        private double CalculateEuclideanDistance(AnonymizedFace face1, AnonymizedFace face2)
        {
            var embedding1 = face1.Embedding;
            var embedding2 = face2.Embedding;
            if (embedding1.Length != embedding2.Length)
            {
                throw new ArgumentException("Os arrays de embeddings devem ter o mesmo tamanho");
            }

            double distance = 0;
            for (int i = 0; i < embedding1.Length; i++)
            {
                distance += (embedding1[i] - embedding2[i]) * (embedding1[i] - embedding2[i]);
            }

            return (double)Math.Sqrt(distance);
        }

        public Task<ComparisonResult> CompareAsync(AnonymizedFace faceA, AnonymizedFace faceB, double threshold)
        {
            double distance = CalculateEuclideanDistance(faceA, faceB);
            bool isMatch = distance < threshold;

            return Task.FromResult(new ComparisonResult
            {
                Distance = distance,
                IsMatch = isMatch
            });
        }
    }
}
