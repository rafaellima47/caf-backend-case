namespace FaceComparisonAPI.Models 
{
    public class Face
    {
        public Guid Identifier { get; set; }
        public byte[] Image { get; set; }
    }
}

