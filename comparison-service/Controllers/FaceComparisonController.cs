using System.Threading.Tasks;
using FaceComparisonAPI.Models;
using FaceComparisonAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FaceComparisonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FaceComparisonController : ControllerBase
    {
        private readonly IAnonymizationService _anonymizationService;
        private readonly IDatabaseService _databaseService;
        private readonly IComparisonService _comparisonService;
        private readonly double _comparisonThreshold;
        private readonly ILogger<FaceComparisonController> _logger;

        public FaceComparisonController(IAnonymizationService anonymizationService,
                                        IDatabaseService databaseService,
                                        IComparisonService comparisonService,
                                        ComparisonConfiguration comparisonConfiguration,
                                        ILogger<FaceComparisonController> logger)
        {
            _anonymizationService = anonymizationService;
            _databaseService = databaseService;
            _comparisonService = comparisonService;
            _comparisonThreshold = comparisonConfiguration.Threshold;
            _logger = logger;
        }

        [HttpPost("anonymize")]
        public async Task<IActionResult> Anonymize([FromForm] Guid identifier, [FromForm] IFormFile image)
        {
            if (identifier == Guid.Empty)
            {
                _logger.LogWarning("Anonymize request with empty identifier");
                return BadRequest("Anonymize request with empty identifier");
            }

            if (image == null || image.Length == 0)
            {if (identifier == Guid.Empty)
                _logger.LogWarning("Anonymize request with empty image");
                return BadRequest("Anonymize request with empty image");
            }

            var embedding = await _anonymizationService.AnonymizeAsync(image);
            var anonymizedFace = new AnonymizedFace { Identifier = identifier, Embedding = embedding };
            await _databaseService.AddAnonymizedFaceAsync(anonymizedFace);

            return Ok(anonymizedFace);
        }


        [HttpPost("compare")]
        public async Task<IActionResult> Compare([FromForm] Guid identifier, [FromForm] IFormFile image)
        {
            var embedding = await _anonymizationService.AnonymizeAsync(image);
            var anonymizedFaceA = new AnonymizedFace { Identifier = identifier, Embedding = embedding };
            var anonymizedFaceB = await _databaseService.GetAnonymizedFaceByIdAsync(identifier);        

            if (anonymizedFaceB == null)
            {
                _logger.LogWarning("Face with identifier {Identifier} not found in the database", identifier);
                return NotFound("Face not found in the database.");
            }

            var comparisonResult = await _comparisonService.CompareAsync(anonymizedFaceA, anonymizedFaceB, _comparisonThreshold);

            _logger.LogInformation(
                "Comparison result: {ComparisonResult}, FaceIdentifier: {Face1Identifier}",
                comparisonResult.IsMatch, anonymizedFaceA.Identifier
            );

            return Ok(comparisonResult);
        }
    }
}
