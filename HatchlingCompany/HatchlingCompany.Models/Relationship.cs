using System.ComponentModel.DataAnnotations;

namespace HatchlingCompany.Models
{
    public class Relationship
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public virtual Employee Author { get; set; }

        public int RecipientId { get; set; }
        public virtual Employee Recipient { get; set; }
        
        [MinLength(2)]
        public string Comment { get; set; }

        public int RelationshipStrength { get; set; }
    }
}
