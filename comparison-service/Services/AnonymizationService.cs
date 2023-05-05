using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FaceComparisonAPI.Services
{
    public class EmbeddingData
    {
        public double[] Embedding { get; set; }
        public FacialArea FacialArea { get; set; }
    }

    public class FacialArea
    {
        public int H { get; set; }
        public int W { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
    
    public class AnonymizationService : IAnonymizationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _anonymizeApiUrl;

        public AnonymizationService(HttpClient httpClient, string anonymizeApiUrl)
        {
            _httpClient = httpClient;
            _anonymizeApiUrl = anonymizeApiUrl;
        }

        public async Task<double[]> AnonymizeAsync(IFormFile image)
        {
            using var content = new MultipartFormDataContent();
            using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var fileContent = new ByteArrayContent(memoryStream.ToArray());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(image.ContentType);
            content.Add(fileContent, "image", image.FileName);

            var response = await _httpClient.PostAsync(_anonymizeApiUrl, content);
            response.EnsureSuccessStatusCode();

            
            var jsonString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<EmbeddingData>>(jsonString);

            var embedding = data[0].Embedding;

            return embedding;
        }
    }
}
