using FaceComparisonAPI.Database;
using FaceComparisonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FaceComparisonAPI.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly FaceComparisonDbContext _dbContext;
        private readonly ILogger<DatabaseService> _logger;

        public DatabaseService(FaceComparisonDbContext dbContext, ILogger<DatabaseService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<AnonymizedFace> AddAnonymizedFaceAsync(AnonymizedFace anonymizedFace)
        {
            var existingFace = await _dbContext.AnonymizedFaces.FirstOrDefaultAsync(face => face.Identifier == anonymizedFace.Identifier);

            if (existingFace != null)
            {
                _logger.LogInformation("Anonymized face with identifier {Identifier} already in database", anonymizedFace.Identifier);
                return existingFace;
            }

            _dbContext.AnonymizedFaces.Add(anonymizedFace);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Anonymized face with identifier {Identifier} added to the database", anonymizedFace.Identifier);
            return anonymizedFace;
        }

        public async Task<AnonymizedFace> GetAnonymizedFaceByIdAsync(Guid identifier)
        {
            return await _dbContext.AnonymizedFaces.FirstOrDefaultAsync(face => face.Identifier == identifier);
        }
    }
}
