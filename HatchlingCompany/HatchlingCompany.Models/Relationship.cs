using System.ComponentModel.DataAnnotations;

namespace HatchlingCompany.Models
{
    public class Relationship
    {
        public int FirstEmployeeId { get; set; }
        public virtual Employee FirstEmployee { get; set; }

        public int SecondEmployeeId { get; set; }
        public virtual Employee SecondEmployee { get; set; }
        
        [MinLength(2)]
        public string Comment { get; set; }

        public int RelationshipStrength { get; set; }
    }
}
