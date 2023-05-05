using System.ComponentModel.DataAnnotations.Schema;

namespace FaceComparisonAPI.Models
{  
    [Table("anonymized_faces")]  
    public class AnonymizedFace
    {
        [Column("identifier")]
        public Guid Identifier { get; set; }
        [Column("embedding")]
        public double[] Embedding { get; set; }
    }
}