namespace FaceComparisonAPI.Utilities
{
    public static class DistanceMetrics
    {
        public static double EuclideanDistance(double[] vectorA, double[] vectorB)
        {
            if (vectorA.Length != vectorB.Length)
            {
                throw new ArgumentException("Vectors must have the same length.");
            }

            double sum = 0.0;

            for (int i = 0; i < vectorA.Length; i++)
            {
                double diff = vectorA[i] - vectorB[i];
                sum += diff * diff;
            }

            return Math.Sqrt(sum);
        }

        public static double CosineSimilarity(double[] vectorA, double[] vectorB)
        {
            if (vectorA.Length != vectorB.Length)
            {
                throw new ArgumentException("Vectors must have the same length.");
            }

            double dotProduct = 0.0;
            double normA = 0.0;
            double normB = 0.0;

            for (int i = 0; i < vectorA.Length; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
                normA += vectorA[i] * vectorA[i];
                normB += vectorB[i] * vectorB[i];
            }

            return dotProduct / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }
    }
}
