using FaceComparisonAPI.Database;
using FaceComparisonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FaceComparisonAPI.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly FaceComparisonDbContext _dbContext;

        public DatabaseService(FaceComparisonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AnonymizedFace> AddAnonymizedFaceAsync(AnonymizedFace anonymizedFace)
        {
            _dbContext.AnonymizedFaces.Add(anonymizedFace);
            await _dbContext.SaveChangesAsync();
            return anonymizedFace;
        }

        public async Task<AnonymizedFace> GetAnonymizedFaceByIdAsync(Guid identifier)
        {
            return await _dbContext.AnonymizedFaces.FirstOrDefaultAsync(face => face.Identifier == identifier);
        }
    }
}
